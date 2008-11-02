// Copyright (C) 2007-2008 Bruno Salzano
// http://centralino-voip.brunosalzano.com
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
//
// As a special exception, you may use this file as part of a free software
// library without restriction.  Specifically, if other files instantiate
// templates or use macros or inline functions from this file, or you compile
// this file and link it with other files to produce an executable, this
// file does not by itself cause the resulting executable to be covered by
// the GNU General Public License.  This exception does not however
// invalidate any other reasons why the executable file might be covered by
// the GNU General Public License.
//
// This exception applies only to the code released under the name GNU
// AstCTIServer.  If you copy code from other releases into a copy of GNU
// AstCTIServer, as the General Public License permits, the exception does
// not apply to the code that you add in this way.  To avoid misleading
// anyone as to the status of such modified files, you must delete
// this exception notice from them.
//
// If you write modifications of your own for AstCTIServer, it is your choice
// whether to permit this exception to apply to your modifications.
// If you do not wish that, delete this exception notice.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.Sockets;
using System.IO;

namespace AstCTIServer
{
    enum ServerStatus
    {
        STATUS_UNDEFINED = 0,
        STATUS_LOGGING_IN = 1,
        STATUS_LOGGEDIN = 2,
        STATUS_REQUESTING_EXTENSIONS = 3,
        STATUS_REQUESTING_SIP = 4,
        STATUS_REQUESTING_QUEUES = 5
    }

    class Server
    {
        public static FileLogger logger;
        public static ConfigurationReader cfg;
        public static SharedState shared;
        public static MySqlConnection cn;
        public static bool debug = false;
        public static bool dumpconf = false;
        public static MainServer AstCTI;
    
        

        public static bool IsMono()
        {
            Type t = Type.GetType("Mono.Runtime");
            return (t != null);
        }

        public static bool IsUnix()
        {
            int p = (int)Environment.OSVersion.Platform;
            return ((p == 4) || (p == 128));
        }

        static void Main(string[] args)
        {
            Server.cfg = new ConfigurationReader();

            // Check for log directory
            DirectoryInfo dinfo = new DirectoryInfo(Server.cfg.LOG_DIRECTORY);
            if (!dinfo.Exists)
            {
                dinfo.Create();
            }
            dinfo = null;

            Server.shared = new SharedState();
            Server.logger = new FileLogger();

            foreach (string arg in args)
            {
                if (arg.ToLower().Equals("--help"))
                {
                    string usage = "\n";
                    usage       += "Usage:\n";
                    usage       += "-----------------------------------------------------------------\n";
                    usage       += "AstCtiServer.exe [--debug] [--dumpconfig]\n";
                    usage       += "-----------------------------------------------------------------\n";
                    usage       += "--debug      : enables extensive debug on console                \n";
                    usage       += "--dumpconfig : enables dump of main asterisk configuration       \n";
                    usage       += "               (extensions.conf, sip.conf and queues.conf) files \n";
                    Console.WriteLine(usage);
                    return;
                }
                if (arg.ToLower().Equals("--debug"))
                {
                    Server.debug = true;
                }

                if (arg.ToLower().Equals("--dumpconfig"))
                {
                    Server.dumpconf = true;
                }

            }

            Server.logger.WriteLine(LogType.Info, "Testing database connection");
            try
            {
                Server.logger.WriteLine(LogType.Debug, "Connecting to database on: " + cfg.MYSQL_HOST);
                Server.cn = new MySqlConnection(cfg.MYSQL_CONNSTR);
                Server.cn.Open();

                // La connessione al database è ok
                Server.logger.WriteLine(LogType.Debug, "Database connection successful");
                if (Server.debug) Console.WriteLine("Database connection successful");
                Server.logger.WriteLine(LogType.Debug, "Server can start");


            }
            catch (Exception ex)
            {
                // Il test non è riuscito. Chiudiamo il programma
                if (Server.debug) Console.WriteLine(ex.ToString());
                Server.logger.WriteLine(LogType.Error, "Database connection is not avaiable. Exiting");
                Environment.Exit(0);
            }

            Server.logger.WriteLine(LogType.Notice, "Starting MainServer");
            AstCTI = new MainServer();

        }
    }

    class MainServer
    {
        private SocketManager sock = null;
        
        private string dataBuffer = "";
        private CTIServer ctiserver = null;
        private Hashtable activeCalls = null;
        public ServerStatus status;

        public MainServer()
        {
            this.status = ServerStatus.STATUS_UNDEFINED;

            this.activeCalls = new Hashtable();
            this.ctiserver = new CTIServer();
            // ctiserver.StartListening();
        
            sock = new SocketManager(Server.cfg.MANAGER_HOST, Server.cfg.MANAGER_PORT);
            sock.Connected += new SocketManager.OnConnected(sock_Connected);
            sock.DataArrival += new SocketManager.OnDataArrival(sock_DataArrival);
            sock.Disconnected += new SocketManager.OnDisconnected(sock_Disconnected);
            sock.SocketError += new SocketManager.OnSocketError(sock_SocketError);
            Thread t = new Thread(new ThreadStart(this.StartServer));
            t.Start();
            sock.Connect();


        }

        void StartServer()
        {

            while (true)
            {
                Thread.Sleep(500);
            }
        }

        void sock_SocketError(object sender, string data, int errorCode)
        {
            if (Server.debug) Console.WriteLine("Asterisk disconnected. Trying again in 10 seconds");
            Server.logger.WriteLine(LogType.Debug, "Asterisk disconnected. Trying again in 10 seconds");
            this.status = ServerStatus.STATUS_UNDEFINED;
            Thread.Sleep(10 * 1000);


            sock.Connect();
        }

        void sock_Disconnected(object sender)
        {
            if (Server.debug) Console.WriteLine("Asterisk disconnected. Trying again in 10 seconds");
            Server.logger.WriteLine(LogType.Debug, "Asterisk disconnected. Trying again in 10 seconds");
            this.status = ServerStatus.STATUS_UNDEFINED;
            Thread.Sleep(10 * 1000);


            sock.Connect();

        }

        void sock_DataArrival(object sender, string data)
        {
            dataBuffer += data;

            if (dataBuffer.Contains("\r\n\r\n"))
            {
                string preparsed = dataBuffer;
                dataBuffer = "";
                ParseData(preparsed);
            }

        }

        void sock_Connected(object sender)
        {
            if (Server.debug) Console.WriteLine("Asterisk Connection in progress");
            string login = "Action: Login\r\n" +
                                     "Username: " + Server.cfg.MANAGER_USER + "\r\n" +
                                     "Secret: " + Server.cfg.MANAGER_PASS + "\r\n\r\n";
            this.status = ServerStatus.STATUS_LOGGING_IN;
            this.sock.SendData(login);
        }


        void ParseData(string data)
        {
            switch(this.status)
            {
                case ServerStatus.STATUS_LOGGING_IN:            
                    if (data.Contains("Authentication accepted"))
                    {
                        if (Server.debug) Console.WriteLine("Authentication successful");
                        // It was asked to dump asterisk configurations
                        if (Server.dumpconf)
                        {
                            this.status = ServerStatus.STATUS_REQUESTING_EXTENSIONS;
                            RequestConfigFile("extensions.conf");
                        }
                        else
                        {
                            this.status = ServerStatus.STATUS_LOGGEDIN;
                        }
                        return;
                    }
                    else
                    {
                        this.status = ServerStatus.STATUS_UNDEFINED;
                        if (Server.debug) Console.WriteLine("Authentication failure");
                    }
                    break;
                case ServerStatus.STATUS_REQUESTING_EXTENSIONS:
                    Server.logger.WriteLine(data);
                    this.status = ServerStatus.STATUS_REQUESTING_SIP;
                    RequestConfigFile("sip.conf");
                    break;
                case ServerStatus.STATUS_REQUESTING_SIP:
                    Server.logger.WriteLine(data);
                    this.status = ServerStatus.STATUS_REQUESTING_QUEUES;
                    RequestConfigFile("queues.conf");
                    break;
                case ServerStatus.STATUS_REQUESTING_QUEUES:
                    Server.logger.WriteLine(data);
                    this.status = ServerStatus.STATUS_LOGGEDIN;
                    break;
                case ServerStatus.STATUS_LOGGEDIN:
                    Hashtable evt = HashFromMessage(data);
                    this.EvaluateEvent(evt);
                    break;
            }
        }

        private void CheckConfigurationFiles()
        {

            
            this.status = ServerStatus.STATUS_LOGGEDIN;
        }

        private void RequestConfigFile(string confFile)
        {
            if (Server.debug) Console.WriteLine("Requesting file " + confFile);
            string request = "Action: GetConfig\r\n" +
                                     "Filename: " + confFile + "\r\n\r\n";
            this.sock.SendData(request);
        }

        private void EvaluateEvent(Hashtable evt)
        {
            string szEvent = (string)evt["Event"];
            string Uniqueid = null;
            string Uniqueid2 = null;
            string Context = null;
            string Destination = null;
            AsteriskCall call = null;
            AsteriskCall call1 = null;
            AsteriskEvent astevt = null;
            switch (szEvent)
            {
                /*
                 * This event always happens when a new channel is created
                 * So is generated everytime a call bengins
                 */
                case "Newchannel":
                    Uniqueid = (string)evt["Uniqueid"];
                    Context = (string)evt["Context"];
                    call = new AsteriskCall();
                    call.Channel = (string)evt["Channel"];
                    call.ParseDestination();
                    call.Uniqueid = Uniqueid;
                    if (Context != null) call.Context = Context;

                    if (Server.debug) Console.WriteLine("Newchannel - " + call.ToString());
                    Server.logger.WriteLine(LogType.Debug, "Newchannel - " + call.ToString());
                    AddActiveCall(Uniqueid, call);
                    break;
                
                /*
                 * Ringing: some phone is ringing? This should be trigger callerid
                 */
                case "Dial":
                    Uniqueid = (string)evt["SrcUniqueID"]; // Caller
                    Uniqueid2 = (string)evt["DestUniqueID"]; // Called

                    call = GetActiveCall(Uniqueid); // caller extension..
                    call1 = GetActiveCall(Uniqueid2); // called extension..
                    
                    if ( (call != null) && (call1 != null) )
                    {

                        string callerid = (string)evt["CallerID"];
                        string calleridname = (string)evt["CallerIDName"];

                        // Here we set called call as dialed ;)
                        call1.isDialed = true;
                        call1.CallerIDNum = callerid;
                        call1.CallerIDName = calleridname;
                        astevt = new AsteriskEvent();
                        astevt.Call = call1;
                        astevt.Event = "Newcallerid";
                        if (call1.ParsedChannel != null) //BI (Beppe Innamorato) 16/10/08 inserted if
                        {
                            SendMessage(call1.ParsedChannel, astevt.ToXML());
                        }
                    }
                    break;
                /*
                 * Example
                 * Event: Newstate
                 * Privilege: call,all
                 * Channel: SIP/201-0080e6e0
                 * State: Ringing
                 * CallerID: 202
                 * CallerIDName: Test 202
                 * Uniqueid: asterisk-5559-1224799548.57

                 */
                case "Newstate":
                    Destination = AsteriskCall.ParseChannel((string)evt["Channel"]);
                    if (Destination != null)
                    {
                        string state = (string)evt["State"];
                        if ((state != null) & (state.Equals("Ringing")))
                        {
                            
                            Uniqueid = (string)evt["Uniqueid"]; // Caller
                            call = GetActiveCall(Uniqueid);

                            // If the call is dialed then we can't 
                            // send callerid event on Newstate..
                            if (!call.isDialed)
                            {

                                string callerid = (string)evt["CallerID"];
                                string calleridname = (string)evt["CallerIDName"];

                                call.CallerIDNum = callerid;
                                call.CallerIDName = calleridname;
                                astevt = new AsteriskEvent();
                                astevt.Call = call;
                                astevt.Event = "Newcallerid";

                                SendMessage(Destination, astevt.ToXML());
                            }
                            
                        }
                        else
                        {
                            AsteriskEvent tmpEvt = new AsteriskEvent();
                            tmpEvt.Event = "Newstate";

                            AsteriskCall tmpCall = new AsteriskCall();
                            tmpCall.Uniqueid = (string)evt["Uniqueid"];
                            tmpCall.CallerIDNum = (string)evt["CallerID"];
                            tmpCall.CallerIDName = (string)evt["CallerIDName"];
                            tmpCall.Channel = (string)evt["Channel"];
                            tmpCall.State = (string)evt["State"];

                            tmpEvt.Call = tmpCall;
                            SendMessage(Destination, tmpEvt.ToXML());

                            tmpCall = null;
                            tmpEvt = null;
                        }

                    }
                    break;
                /*
                 * Event: OriginateResponse
                 * Privilege: call,all
                 * Response: Success
                 * Channel: SIP/201-082141f8
                 * Context: astctidemo
                 * Exten: 200
                 * Reason: 4
                 * Uniqueid: 1193986538.26
                 * CallerID: <unknown>
                 * CallerIDNum: <unknown>
                 * CallerIDName: SIP/201
                 */
                case "OriginateResponse":
                    string originateResponse = (string)evt["Response"];
                    if (originateResponse == null) return;
                    if (originateResponse.Equals("Success"))
                    {
                        Uniqueid = (string)evt["Uniqueid"];
                        Context = (string)evt["Context"];

                        call = GetActiveCall(Uniqueid);
                        if (call == null)
                        {
                            call = new AsteriskCall();
                        }
                        call.Context = Context;
                        call.Uniqueid = Uniqueid;
                    }
                    break;

                /*
                 * This event is generated everytime a new step in the dialplan
                 * is done: a new extension of type set may contains calldata
                 */
                case "Newexten":
                    Uniqueid = (string)evt["Uniqueid"];
                    Context = (string)evt["Context"];



                    call = GetActiveCall(Uniqueid);
                    if (call == null)
                    {
                        call = new AsteriskCall();
                    }
                    if (Context != null) call.Context = Context;
                    if (Uniqueid != null) call.Uniqueid = Uniqueid;
                    AddActiveCall(Uniqueid, call);

                    string Application = (string)evt["Application"];
                    if (Application == null) break;
                    switch (Application)
                    {
                        case "Set":
                            string AppData = ParseCallData((string)evt["AppData"]);
                            if (AppData != null)
                            {
                                call = GetActiveCall(Uniqueid);
                                if (call != null)
                                {
                                    if (Context != null) call.Context = Context;
                                    call.AppData = AppData;
                                    if (Server.debug) Console.WriteLine("AppData - " + call.ToString());
                                    Server.logger.WriteLine(LogType.Debug, "AppData - " + call.ToString());
                                }
                            }
                            break;
                        
                    }
                    break;
                /*
                 * Here we should notify the client..
                 * 
                 * >> Example -> SIP/202 calls SIP/201
                 * Event: Link
                 * Privilege: call,all
                 * Channel1: SIP/202-082173d0
                 * Channel2: SIP/201-0821bb20
                 * Uniqueid1: 1194000842.70
                 * Uniqueid2: 1194000842.71
                 * CallerID1: 202
                 * CallerID2: 201
                 * 
                 * >> Example -> SIP/202 calls Extension 100
                 * Event: Link
                 * Privilege: call,all
                 * Channel1: SIP/202-082173d0
                 * Channel2: SIP/201-0821bb20
                 * Uniqueid1: 1194001092.74
                 * Uniqueid2: 1194001092.75
                 * CallerID1: 202
                 * CallerID2: 100
                 */
                case "Link":
                    Uniqueid = (string)evt["Uniqueid1"];
                    Uniqueid2 = (string)evt["Uniqueid2"];
                    string callerid1 = (string)evt["CallerID1"];
                    string callerid2 = (string)evt["CallerID2"];
                    call = GetActiveCall(Uniqueid); // caller
                    call1 = GetActiveCall(Uniqueid2); // called

                    if (call1 != null)
                    {
                        if (Server.debug) Console.WriteLine("Link - " + call1.ToString());
                        Server.logger.WriteLine(LogType.Debug, "Link - " + call1.ToString());

                        if (call1.AppData == null)
                        {
                            if (call.AppData != null) call1.AppData = call.AppData;
                        }
                        if (call1.Context == null)
                        {
                            if (call.Context != null) call1.Context = call.Context;
                        }
                        // call1.CallerIDNum = callerid2;
                    }
                    if ((call != null) & (call1 != null))
                    {
                        if (Server.debug) Console.WriteLine("Link");
                        astevt = new AsteriskEvent();
                        astevt.Call = call1;
                        astevt.Event = "Link";

                        Destination = AsteriskCall.ParseChannel((string)evt["Channel2"]);
                        if (Destination != null)
                        {
                            Server.logger.WriteLine(LogType.Debug, string.Format("Sending Link notification to {0}", Destination));
                            SendMessage(Destination, astevt.ToXML());
                        }
                    }
                    break;

                /*
                 * This event happens after a Dial command and usually before a Ringing event
                 * and it's unique id is of the dialed extension. So we can notify when this happens
                 */
                case "Newcallerid":
                    //Uniqueid = (string)evt["Uniqueid"];
                    //call = GetActiveCall(Uniqueid); // called extension..

                    //if (call != null)
                    //{
                    //    string callerid = (string)evt["CallerID"];
                    //    string calleridname = (string)evt["CallerIDName"];
                    //    call.CallerIDNum = callerid;
                    //    call.CallerIDName = calleridname;
                    //    AddActiveCall(Uniqueid, call);   // Update the call info..
                    //    if (Server.debug) Console.WriteLine("Newcallerid - " + call.ToString());

                    //    astevt = new AsteriskEvent();
                    //    astevt.Call = call;
                    //    astevt.Event = "Newcallerid";
                    //    if (call.ParsedChannel != null) //BI (Beppe Innamorato) 16/10/08 inserted if
                    //    {
                    //        SendMessage(call.ParsedChannel, astevt.ToXML());
                    //    }
                    //}

                    break;
                /*
                 * When an hangup event is fired, we've to remove
                 * the call with the uniqueid matched
                 */
                case "Hangup":
                    Uniqueid = (string)evt["Uniqueid"];
                    call = GetActiveCall(Uniqueid);
                    if (call != null)
                    {
                        astevt = new AsteriskEvent();
                        astevt.Call = call;
                        astevt.Event = "Hangup";
                        if (call.ParsedChannel != null) //BI (Beppe Innamorato) 16/10/08 inserted if
                        {
                            SendMessage(call.ParsedChannel, astevt.ToXML());
                        }
                        if (Server.debug) Console.WriteLine("Hangup - " + call.ToString());
                        Server.logger.WriteLine(LogType.Debug, "Hangup - " + call.ToString());
                        RemoveActiveCall(Uniqueid);
                    }

                    break;
                default:

                    break;
            }
        }

        public void SendDataToAsterisk(string data)
        {
            if (data.Length > 0)
            {
                this.sock.SendData(data);
            }
        }

        private string ParseCallData(string calldata)
        {
            if (calldata == null) return null;
            if (calldata.Contains("calldata="))
            {
                return calldata.Replace("calldata=", "");
            }
            else
            {
                return null;
            }
        }

        private AsteriskCall GetActiveCall(string uniqueid)
        {
            if (uniqueid == null) return null;
            return (this.activeCalls.ContainsKey(uniqueid)) ? (AsteriskCall)this.activeCalls[uniqueid] : null;

        }

        private void AddActiveCall(string uniqueid, AsteriskCall dest)
        {
            if (!this.activeCalls.ContainsKey(uniqueid))
            {
                this.activeCalls.Add(uniqueid, dest);
            }
            else
            {
                this.activeCalls[uniqueid] = dest;
            }
            if (Server.debug) Console.WriteLine("Active channels: " + activeCalls.Count.ToString());
        }

        private void RemoveActiveCall(string uniqueid)
        {
            if (this.activeCalls.ContainsKey(uniqueid))
            {
                this.activeCalls.Remove(uniqueid);
            }
            if (Server.debug) Console.WriteLine("Active channels: " + activeCalls.Count.ToString());
        }

        private void SendMessage(string channel, string message)
        {
            ClientManager cl = (ClientManager)Server.shared.clients[channel];
            if (cl != null)
            {
                cl.SendData(message, "\n");
            }
        }

        private void SendUdpMessage(IPEndPoint ep, string message)
        {
            try
            {
                if (ep != null)
                {
                    string msg = message + "\0";
                    UdpClient client = new UdpClient();
                    client.Connect(ep);
                    byte[] dat = Encoding.ASCII.GetBytes(msg);
                    client.Send(dat, dat.Length);
                    client.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        private Hashtable HashFromMessage(string data)
        {
            Hashtable t = new Hashtable();
            string[] lines = data.Split('\n');
            foreach (string line in lines)
            {
                if (line.Contains(":"))
                {
                    string[] kv = line.Split(':');
                    if (kv.Length == 2)
                    {
                        t.Add(kv[0].Trim(), kv[1].Trim().Replace("\r", ""));
                    }
                }

            }
            return t;
        }


    }

}
