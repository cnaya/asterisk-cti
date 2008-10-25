using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AstCTIClient
{

    class UDPServer
    {
        private int _udpport = 0;
        private Thread udpThread = null;
        private bool bIsRunning = false;
        private IPEndPoint ipep;
        private UdpClient newsock;

        public delegate void OnStarted(Object sender);
        public event OnStarted Started;

        public delegate void OnStopped(Object sender);
        public event OnStopped Stopped;

        public delegate void OnError(Object sender, string err);
        public event OnError Error;

        public delegate void OnDataArrival(Object sender, string data);
        public event OnDataArrival DataArrival;

        public UDPServer(int port)
        {
            this._udpport = port;
            
        }

        public void StartReceive()
        {
            
            this.udpThread = new Thread(new ThreadStart(this.ReceiveData));
            this.udpThread.Start();
        }

        public void StopReceive()
        {
            this.bIsRunning = false;

            try
            {
                if (this.udpThread != null)
                {
                    if (this.udpThread.IsAlive)
                    {
                        this.udpThread.Join(300);
                        this.udpThread.Abort();
                    }
                }
                if (this.newsock != null)
                {
                    this.newsock.Close();
                }
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Thread cleaned");
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Thread cleaned");
            }
            catch (Exception ex)
            {
                if (this.Error != null) this.Error(this, ex.Message);
            }           
            
            if (this.Stopped != null) this.Stopped(this);
            this.udpThread = null;
        }

        private void ReceiveData()
        {
            byte[] udpData = new byte[1024];
            string strUdpData = "";
            ipep = new IPEndPoint(IPAddress.Any, this._udpport);            
            newsock = new UdpClient(ipep);
            
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            this.bIsRunning = true;
            if (this.Started != null) this.Started(this);
            while (this.bIsRunning)
            {
                try
                {
                    udpData = newsock.Receive(ref sender);
                    strUdpData = Encoding.ASCII.GetString(udpData, 0, udpData.Length);
                    strUdpData = strUdpData.Replace("\0", "");
                    Console.WriteLine(strUdpData);
                    if (this.DataArrival != null) this.DataArrival(this, strUdpData);
                }
                catch (Exception ex)
                {
                    if (this.Error != null) this.Error(this, ex.Message);
                }
            }
            newsock.Close();
        }


    }
}
