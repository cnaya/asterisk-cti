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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Threading;
using System.Data;


namespace AstCTIServer
{
    class ClientManager
    {
        #region Private Variables
        private Socket ClientSocket;    // The client socket
        private int ReceiveBufferSize = 4;  // Size of the receive buffer
        private RemoteAddress remoteAddress;    // Remote address
        private NetworkStream networkStream;    // Network stream
        private Thread wThread; // The local thread
        public CTIOperator activeOper = null; // Active CTI Operator        
        #endregion

        #region Public variables
        public bool bRunning = false; // Thread exit condition        
        #endregion

        #region Public events
        public delegate void OnClientStarted(Object sender, RemoteAddress addr);
        public event OnClientStarted ClientStarted;

        public delegate void OnClientStopped(Object sender, RemoteAddress addr);
        public event OnClientStopped ClientStopped;

        public delegate void OnServerStopped(Object sender, RemoteAddress addr);
        public event OnServerStopped ServerStopped;
        #endregion

        #region Constructor
        public ClientManager(Socket clsock)
        {
            this.ClientSocket = clsock;
            this.remoteAddress = RemoteAddress.Parse(clsock.RemoteEndPoint.ToString());
            LingerOption lingeroption = new LingerOption(false, 0);
            this.ClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, (Server.cfg.CTI_CLIENT_KEEPALIVE  * 1000));
            this.ClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, lingeroption);
            this.ClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, (Server.cfg.CTI_CLIENT_TIMEOUT * 1000));
             
            Server.logger.WriteLine(LogType.Notice, string.Format("ClientManager initialized for {0}",clsock.RemoteEndPoint.ToString()));

        }
        #endregion

        #region Client Thread Management
        /// <summary>
        /// Start the client thread
        /// </summary>
        public void Start()
        {
            if (this.wThread != null) this.Stop();           

            this.wThread = new Thread(new ThreadStart(this.Manager));
            this.wThread.Name = "Thread-" + this.remoteAddress.Address;
            this.wThread.Start();
        }


        /// <summary>
        /// Stops the client thread
        /// </summary>
        public void Stop()
        {
            try
            {
                if (this.wThread.IsAlive)
                {
                    this.bRunning = false;
                    this.wThread.Join(200);
                }
                if (this.wThread.IsAlive)
                {
                    this.wThread.Abort();
                }
                if (this.ClientSocket.Connected)
                {
                    this.CloseSockets("");
                }
                
                if (this.activeOper != null)
                {
                    this.activeOper = null;
                    // this.sessione = null;
                }

            }
            catch (ThreadAbortException)
            {
                // Do thread deinit here..
            }

            this.wThread = null;
            if (this.ClientStopped != null) this.ClientStopped(this, this.remoteAddress);
        }

        /// <summary>
        /// Main ClientManager loop
        /// </summary>
        private void Manager()
        {
            string reason = "";
            bRunning = true;
            

            char[] splitparms = { '\n' };
            byte[] bytes;		// Incoming data from the client.

            networkStream = new NetworkStream(this.ClientSocket);
            ReceiveBufferSize = (int)this.ClientSocket.GetSocketOption(
                                    SocketOptionLevel.Socket,
                                    SocketOptionName.ReceiveBuffer);

            Server.logger.WriteLine(LogType.Notice, string.Format("ClientManager started for {0}", this.ClientSocket.RemoteEndPoint.ToString()));
            if (this.ClientStarted != null) this.ClientStarted(this, this.remoteAddress);
            this.SendData("100 CTISERVER WELCOME", "\n");

            bytes = new byte[ReceiveBufferSize];
            while (bRunning)
            {
                string data = null;
                string command = "";
                string parameters = "";

                try
                {
                    if (!this.IsSocketConnected(this.ClientSocket))
                    {
                        reason = "Remote client has closed the socket";
                        this.bRunning = false;
                    }
                    int BytesRead = networkStream.Read(bytes, 0, ReceiveBufferSize);
                    data = Encoding.ASCII.GetString(bytes, 0, BytesRead);


                    string[] datas = data.Split(splitparms);
                    if (datas.Length > 0)
                    {
                        for (int pos = 0; pos < datas.Length; pos++)
                        {
                            parameters = "";
                            command = "";
                            command = ProtocolParser.Parse(datas[pos], ref parameters);
                            if (command != null)
                            {
                                bool bPresent = false;
                                if (this.activeOper != null)
                                {
                                    bPresent = true;
                                    
                                }
                                if (!bPresent) 
                                {
                                    if ((!command.ToUpper().Equals("USER")) &
                                         (!command.ToUpper().Equals("PASS")) &
                                         (!command.ToUpper().Equals("HELO")) &
                                         (!command.ToUpper().Equals("NOOP")) &
                                         (!command.ToUpper().Equals("ORIG")) &
                                         (!command.ToUpper().Equals("QUIT")))
                                    {
                                        command = "";

                                    }
                                }

                                switch (command.ToUpper().Trim())
                                {
                                    #region USER
                                    case "USER":
                                        if (!checkParameters(parameters))
                                        {
                                            this.SendData("101 ERROR", "\n");
                                            break;
                                        }
                                        bool bAlreadyConnected = false;
                                        string ipAlreadyConnected = "";
                                        lock (Server.shared.clients)
                                        {
                                            if (Server.shared.clients.Count > 0)
                                            {
                                                for (int i = 0; i < Server.shared.clients.Count; i++)
                                                {
                                                    IEnumerator cls = Server.shared.clients.Values.GetEnumerator();
                                                    while(cls.MoveNext())
                                                    {
                                                        ClientManager cl = (ClientManager)cls.Current;
                                                        if (cl.activeOper != null)
                                                        {
                                                            if (cl.activeOper.USERNAME.Equals(parameters))
                                                            {
                                                                bAlreadyConnected = true;
                                                            }
                                                        }
                                                    }                                                   
                                                }
                                            }
                                        }
                                        if (bAlreadyConnected)
                                        {
                                            this.SendData("101 KO", "\n");
                                            Server.logger.WriteLine(LogType.Notice, String.Format("Client already connected from {0} with username: {1}", ipAlreadyConnected, parameters));
                                        }
                                        else
                                        {
                                            this.activeOper = new CTIOperator();
                                            this.activeOper.USERNAME = parameters;
                                            this.SendData("100 OK", "\n");                                            
                                        }
                                        break;
                                    #endregion

                                    #region PASS
                                    case "PASS":
                                        if (this.activeOper == null)
                                        {
                                            this.SendData("101 KO", "\n");
                                            break;
                                        }
                                        if (!checkParameters(parameters))
                                        {
                                            this.SendData("101 ERROR", "\n");
                                            break;
                                        }

                                        if (this.activeOper.LoadFromCredential(this.activeOper.USERNAME, parameters))
                                        {
                                            Server.shared.clients.Add(this.activeOper.EXT, this); 
                                            this.SendData("102 OK", "\n");
                                        }
                                        else
                                        {
                                            this.SendData("101 KO", "\n");
                                            this.activeOper = null;
                                        }
                                        break;
                                    #endregion

                                    #region QUIT
                                    case "QUIT":
                                        if (this.activeOper != null)
                                        {
                                            Server.shared.clients.Remove(this.activeOper.EXT); 
                                            this.activeOper = null;
                                        }
                                        this.SendData("900 BYE", "\n");
                                        reason = "Remote client closed connection";
                                        this.bRunning = false;
                                        break;
                                    #endregion

                                    #region ORIGINATE
                                    case "ORIG":
                                        // exten,context,prio
                                        if (!checkParameters(parameters))
                                        {
                                            this.SendData("101 ERROR", "\n");
                                            break;
                                        }
                                        string[] parms = parameters.Split('|');
                                        if (parms.Length < 3)
                                        {
                                            this.SendData("101 ERROR", "\n");
                                            break;
                                        }
                                        string orig_extn = parms[0];
                                        string orig_cntx = parms[1];
                                        string orig_prio = parms[2];
                                        string ast_cmd = this.BuildOriginateCommand(orig_extn, orig_cntx, orig_prio);
                                        Server.AstCTI.SendDataToAsterisk(ast_cmd);
                                        this.SendData("100 OK", "\n");
                                        break;
                                    #endregion

                                    #region HELO and NOOP
                                    case "NOOP":
                                        this.SendData("100 OK", "\n");
                                        break;
                                    case "HELO":
                                        this.SendData("100 OK", "\n");
                                        break;
                                    #endregion
                                }
                            }
                        }
                    }
                }
                catch (IOException)
                {
                    
                    reason = "Client Socket timeout";
                    Server.logger.WriteLine(LogType.Notice, reason);
                    bRunning = false;
                    break;

                } // Timeout
                catch (SocketException)
                {
                    reason = "Socket is broken";
                    Server.logger.WriteLine(LogType.Notice, reason);
                    bRunning = false;
                    break;

                }
                catch (ObjectDisposedException ex)
                {
                    Server.logger.WriteLine(LogType.Debug, string.Format("ObjectDisposedException: {0}",ex.Source));
                }
                catch (Exception ex)
                {
                    Server.logger.WriteLine(LogType.Error, ex.ToString());
                }
			}		
			
			this.CloseSockets(reason);
            if (this.activeOper != null) 
                Server.shared.clients.Remove(this.activeOper.EXT);
			
			Interlocked.Decrement(ref Server.shared.NumberOfClients);
			Server.logger.WriteLine(LogType.Notice, String.Format("Number of clients connected: {0}",Server.shared.NumberOfClients));

        }

        /// <summary>
        /// This helper function builds an Originate AMI Command
        /// </summary>
        /// <param name="exten">The exten to dial</param>
        /// <param name="context">The originating dialing context</param>
        /// <param name="prio">Priority of the exten to dial</param>
        /// <returns>The string with the command</returns>
        private string BuildOriginateCommand(string exten, string context, string prio)
        {
            /*
             * Action: Originate
             * Channel: Calling channel
             * Context: Context on which generate the call
             * Exten: Exten to dial
             * Priority: In which priority generate the call
             * Callerid: Callerid for the generated call
             */

            if (context.Length < 1) context = "default";
            if (prio.Length < 1) prio = "1";
            

            string command = "Action: Originate\r\n";
            command += "Channel: " + this.activeOper.EXT + "\r\n";
            command += "Context: " + context + "\r\n";
            command += "Exten: " + exten + "\r\n";
            command += "Priority: " + prio + "\r\n";
            command += "Callerid: " + this.activeOper.EXT + "\r\n";
            command += "Async: true\r\n";
            command += "\r\n";

            return command;
        }

        /// <summary>
        /// This function checks polls a socket to see if it's connected
        /// </summary>
        /// <param name="target">The Socket to check</param>
        /// <returns>True if connected; otherwise, false</returns>
        private bool IsSocketConnected(System.Net.Sockets.Socket target) {
			if (!target.Connected) return false;
            try
            {
                bool bstate = target.Poll(1, System.Net.Sockets.SelectMode.SelectRead);
                if (bstate & (target.Available == 0)) return false;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
		}
        /// <summary>
        /// Check if parameters are present
        /// </summary>
        /// <param name="parameters">The string to check</param>
        /// <returns>True if parameters string is greater than 0</returns>
        private bool checkParameters(string parameters)
        {
            return (parameters.Length > 0);
        }
		#endregion

        #region Socket Management
        /// <summary>
        /// Sends data through the socket
        /// </summary>
        /// <param name="data">String to send</param>
        /// <returns>true if the send is successful; otherwise, false</returns>
        public bool SendData(string data)
        {
            try
            {
                byte[] sendBytes = Encoding.ASCII.GetBytes(data);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                return true;
            }
            catch (ObjectDisposedException ex)
            {
                Server.logger.WriteLine(LogType.Notice, "The stream has been closed (" + ex.Source + ")");
                return false;
            }
            catch (Exception ex)
            {
                Server.logger.WriteLine(LogType.Error, ex.ToString());
                return false;
            }
        }


        /// <summary>
        /// Sends data terminated by a term string through the socket
        /// </summary>
        /// <param name="data">String to send</param>
        /// <param name="term">Stringa to add to data string</param>
        /// <returns>true if the send is successful; otherwise, false</returns>
        public bool SendData(string data, string term)
        {
            return this.SendData(data + term);
        }


        /// <summary>
        /// Close the stream and the socket
        /// </summary>
        /// <param name="reason">The reason for which the close is invoked</param>
        public void CloseSockets(string reason)
        {
            try
            {
                networkStream.Close();
                ClientSocket.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            Server.logger.WriteLine(LogType.Info, String.Format("Client from {0} disconnected: {1}", remoteAddress.Address, reason));


        }

        #endregion

		#region Overrides
		
		public override string ToString()
		{
			return this.remoteAddress.Address + ":" + this.remoteAddress.Port.ToString();
		}
		
		public override int GetHashCode()
		{
			return this.remoteAddress.ToString().GetHashCode();
		}

		#endregion

		#region IDisposable

			public void Dispose()
			{
				// Deduct no. of clients by one
				Interlocked.Decrement(ref Server.shared.NumberOfClients );
                Console.WriteLine("Number of active connections is {0}", Server.shared.NumberOfClients);
				this.wThread = null;
				this.remoteAddress = null;
				this.ClientSocket = null;
				this.networkStream = null;
				// this.sessione = null;
				
			}

		#endregion
	}
}
