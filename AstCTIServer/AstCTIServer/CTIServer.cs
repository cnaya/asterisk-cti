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
using System.Collections.Generic;
using System.Threading;

namespace AstCTIServer
{
    /// <summary>
    /// This class is the front end to AstCTIClients connections.
    /// It's the server part: for each connection, this class starts
    /// and take track of a ClientManager object.
    /// </summary>
    class CTIServer
    {
        #region Private Variables
        private TcpListener listener;			// Socket Server
        #endregion

        #region Constructor
        public CTIServer()
        {
            Thread main = new Thread(new ThreadStart(this.StartListening));
            main.Start();
        }
        #endregion

        #region Server Socket Management

        /// <summary>
        /// Start the Server Listening Loop Thread
        /// </summary>
        public void StartListening()
        {
            // Starts a server socket listening on IP:PORT from configuration file
            string toLog = "";
            IPAddress ipaddr = IPAddress.Parse(Server.cfg.CTI_SERVER_LISTEN_IP);
            IPEndPoint endpoint = new IPEndPoint(ipaddr, Server.cfg.CTI_SERVER_LISTEN_PORT);
            listener = new TcpListener(endpoint);
            try
            {
                listener.Start();

                int ClientNbr = 0;

                // Start listening for connections.
                toLog = string.Format("CTIServer Listening on {0}:{1}",endpoint.Address.ToString(), endpoint.Port);
                Server.logger.WriteLine(LogType.Info, toLog);
                if (Server.debug) Console.WriteLine(toLog); 
                while (Server.shared.ContinueProcess)
                {
                    try
                    {
                        // Let's accept a new connection
                        Socket handler = listener.AcceptSocket();
                        if (handler != null)
                        {
                            // Here's a new connection!
                            toLog = string.Format("New Connection (#{0}) from {1} accepted", ++ClientNbr, handler.RemoteEndPoint.ToString());
                            Server.logger.WriteLine(LogType.Info,toLog);

                            // Increment no. of clients by one
                            Interlocked.Increment(ref Server.shared.NumberOfClients);

                            // Handling the new connection
                            ClientManager client = new ClientManager(handler);
                            // let's take track of client events
                            client.ClientStopped +=new ClientManager.OnClientStopped(client_ClientStopped);
                            client.ServerStopped +=new ClientManager.OnServerStopped(client_ServerStopped);
                            // Starting the client management thread
                            client.Start();
                        }
                        else
                            break;
                    }
                    catch (Exception ex)
                    {
                        if (Server.debug) Console.WriteLine(ex.ToString());
                    }
                }

            }
            catch (SocketException ex)
            {
                Server.logger.WriteLine(LogType.Fatal, string.Format("Listener ended: {0}" , ex.NativeErrorCode.ToString()));
            }
            catch (Exception ex)
            {
                Server.logger.WriteLine(LogType.Fatal, string.Format("Listener ended: {0}" , ex.Message));
            }

        }

        /// <summary>
        /// This event is fired when the server gets stopped... not used yet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="addr"></param>
        void client_ServerStopped(object sender, RemoteAddress addr)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// This event is fired when the client gets stopped... not used yet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="addr"></param>
        void client_ClientStopped(object sender, RemoteAddress addr)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion
    }
}
