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
using System.Net;
using System.Net.Sockets;

namespace AstCTIServer
{
	public class SocketManager
	{
		private Socket pClientSocket;
		private string pHost;
		private int pPort;
		private IAsyncResult m_result;
		public AsyncCallback m_pfnCallBack ;
		
		public delegate void OnConnected(object sender);
		public event OnConnected Connected;

		public delegate void OnDisconnected(object sender);
		public event OnDisconnected Disconnected;

		public delegate void OnDataArrival(object sender, string data);
		public event OnDataArrival DataArrival;
		
		public delegate void OnSocketError(object sender, string data, int errorCode);
		public event OnSocketError SocketError;
		
		private string buffer ="";
		
		
		public SocketManager() 
		{

		}

		public SocketManager(string host, int port)
		{
			this.pHost = host;
			this.pPort = port;
		}
		
		
		
		public string Host 
		{
			get 
			{
				return this.pHost;
			}
			set
			{
				this.pHost = value;
			}
		}

		public int Port
		{
			get 
			{
				return this.pPort;
			}
			set
			{
				this.pPort = value;
			}
		}
		
		public bool IsConnected 
		{
			get 
			{
				if (this.pClientSocket != null) 
				{
					return this.pClientSocket.Connected; 
				}
				return false;
			}
		}
			

		public bool Connect() 
		{
            Server.logger.WriteLine(LogType.Debug, "In SocketManager::Connect()");
            if ((this.pHost.Length < 1) && (this.pPort < 0))
            {
                Server.logger.WriteLine(LogType.Error, "SocketManager::Connect(): No Host or no port");
                return false;
            }
			try 
			{
				this.pClientSocket = new Socket(AddressFamily.InterNetwork,
					SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress remoteAddress = null;

                if (RemoteAddress.IsIP(this.pHost))
                {
                    remoteAddress = IPAddress.Parse(this.pHost);
                }
                else
                {
                    Server.logger.WriteLine(LogType.DebugSocket, "SocketManager::Connect(): Try to resolve " + this.pHost);

                    IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(this.pHost);

                    if (hostEntry.AddressList.Length < 1)
                    {
                        Server.logger.WriteLine(LogType.DebugSocket, "SocketManager::Connect(): GetHostEntry returned -1");
                        return false;
                    }
                    remoteAddress = hostEntry.AddressList[0];

                    Server.logger.WriteLine(LogType.DebugSocket, "SocketManager::Connect(): Resolved " + remoteAddress.ToString());
                }
                System.Net.IPEndPoint remoteEndPoint = new IPEndPoint(remoteAddress, this.pPort);
                
                Server.logger.WriteLine(LogType.Debug, "SocketManager::Connect(): Connecting Socket");
                this.pClientSocket.Connect(remoteEndPoint);
				if(this.pClientSocket.Connected) 
				{
                    Server.logger.WriteLine(LogType.Debug, "SocketManager::Connect(): Connect Successful");
					if (this.Connected != null) this.Connected(this);
                    Server.logger.WriteLine(LogType.DebugSocket, "SocketManager::Connect(): Wait for data");
					this.WaitForData();
					return true;	
				}

                Server.logger.WriteLine(LogType.Error, "SocketManager::Connect(): Connect Failed");
                return false;
				

			} 
			catch (SocketException ex) 
			{
				
				if (this.SocketError != null) this.SocketError(this, ex.Message, ex.ErrorCode);
				if (this.Disconnected != null) this.Disconnected(this);
				return false;
			}
			
		}

		public void Disconnect() 
		{
			if ( this.pClientSocket != null )
			{
				try 
				{
					this.pClientSocket.Close ();
					this.pClientSocket = null;
					
				}
				catch(Exception) {}
			}
			if (this.Disconnected != null) this.Disconnected(this);
		}

		public bool SendData(string data) 
		{
			try
			{
				Object objData = data;
				byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString ());
				if(this.pClientSocket != null)
				{
                    Server.logger.WriteLine(LogType.DebugSocket, "SocketManager::SendData(): " + data);
					pClientSocket.Send (byData);
					return true;
				}
				if (this.Disconnected != null) this.Disconnected(this);
				return false;
			}
			catch(SocketException se)
			{
				if (this.SocketError != null) this.SocketError(this, se.Message,se.ErrorCode);
				if (this.Disconnected != null) this.Disconnected(this);
                Server.logger.WriteLine(LogType.Debug, "SocketManager::SendData(): Error: " +se.Message );
				return false;
			}	
		}

		public void WaitForData()
		{
			try
			{
                if  ( m_pfnCallBack == null ) 
				{
					m_pfnCallBack = new AsyncCallback (OnDataReceived);
				}
				
				if (!this.pClientSocket.Connected) 
				{
                    Server.logger.WriteLine(LogType.Debug, "SocketManager::WaitForData(): Server is not connected");
					this.Disconnect();
				}
				
				if (this.pClientSocket == null) return;
				SocketPacket theSocPkt = new SocketPacket ();
				theSocPkt.thisSocket = this.pClientSocket;
				// Start listening to the data asynchronously
				m_result = this.pClientSocket.BeginReceive (theSocPkt.dataBuffer,
					0, theSocPkt.dataBuffer.Length,
					SocketFlags.None, 
					m_pfnCallBack, 
					theSocPkt);
			}
			catch(SocketException se)
			{
                Server.logger.WriteLine(LogType.Debug, "SocketManager::WaitForData(): SocketException: " + se.Message);
				this.Disconnect();
				

			}
			catch(Exception ex) 
			{
                Server.logger.WriteLine(LogType.Debug, "SocketManager::WaitForData(): Exception: " + ex.Message);
				Console.WriteLine(ex.ToString());
			}

		}

		public void OnDataReceived(IAsyncResult asyn)
		{
			try
			{
                SocketPacket theSockId = (SocketPacket)asyn.AsyncState ;
				int iRx  = theSockId.thisSocket.EndReceive (asyn);
				if (iRx == 0) 
				{
					this.Disconnect();
					return;
				}
				char[] chars = new char[iRx +  1];
				System.Text.Decoder d = System.Text.Encoding.ASCII.GetDecoder();
				int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
				System.String szData = new System.String(chars);
				szData = szData.Replace("\0","");
				buffer += szData;
				
				if (buffer.EndsWith("\n")) 
				{

					if (this.DataArrival != null) this.DataArrival(this, buffer);
                    buffer = buffer.Replace("\n", "");
                    Server.logger.WriteLine(LogType.DebugSocket, "SocketManager::OnDataReceived(): " + buffer);
					buffer = "";
				}
				this.WaitForData();

			}
			catch (ObjectDisposedException )
			{
				Server.logger.WriteLine(LogType.Debug ,"\nOnDataReceived: Socket has been closed\n");
				this.Disconnect();
			}
			catch(SocketException se)
			{

				this.Disconnect();
                Server.logger.WriteLine(LogType.Debug, "\nOnDataReceived: " + se.Message);
			}
		}
	
		public class SocketPacket
		{
			public System.Net.Sockets.Socket thisSocket;
			public byte[] dataBuffer = new byte[1];
		}
	}
}
