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
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;

namespace AstCTIServer
{
	/// <summary>
	/// This class reads configuration from XML App.config
	/// </summary>
	public class ConfigurationReader
	{
        #region Private Variables
        private string CURRENT_PATH = "";
        #endregion

        #region public Variables
		public string	MYSQL_CONNSTR	= "";
		public string	MYSQL_HOST		= "";
		public string	MYSQL_USER		= "";
		public string	MYSQL_PASS		= "";
		public string	MYSQL_DBMS		= "";
        public string   MYSQL_TIMEOUT   = "30";
		public bool		LOG_ACTIVE		= false;
		public string	LOG_DIRECTORY	= "";
		public int		SOCKET_TIMEOUT	= 10;
		public string	MANAGER_HOST	= "";
		public int		MANAGER_PORT	= 0;
        public string   MANAGER_USER = "";
        public string   MANAGER_PASS = "";
        public LogType LOG_LEVEL = (LogType)0x63;

        public int      CTI_CLIENT_TIMEOUT = 60;
        public int      CTI_CLIENT_KEEPALIVE = 30;
		public string   CTI_SERVER_LISTEN_IP = "0.0.0.0";
	    public int      CTI_SERVER_LISTEN_PORT = 9000;
        #endregion

        #region Constructor
        public ConfigurationReader()
		{
            FileInfo f = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.CURRENT_PATH = f.DirectoryName;
            this.readConfiguration();
        }
        #endregion

        #region Configuration reading

        /// <summary>
        /// Read the XML configuration and map all the fields to 
        /// public variables
        /// </summary>
        private void readConfiguration() 
		{
            try
            {
                System.Collections.Specialized.NameValueCollection database = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("globalsettings/database");
                MYSQL_HOST = database["MYSQL_HOST"];
                MYSQL_DBMS = database["MYSQL_DBMS"];
                MYSQL_USER = database["MYSQL_USER"];
                MYSQL_PASS = database["MYSQL_PASS"];
                MYSQL_TIMEOUT = database["MYSQL_TIMEOUT"];
                MYSQL_CONNSTR = getConnectionString(MYSQL_HOST, MYSQL_USER, MYSQL_PASS, MYSQL_DBMS, MYSQL_TIMEOUT);

                System.Collections.Specialized.NameValueCollection logging = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("globalsettings/logging");
                this.LOG_DIRECTORY = ParsePath(logging["DIRECTORY"]);
                this.LOG_ACTIVE = ParseBoolean(logging["ACTIVE"]);
                this.LOG_LEVEL = (LogType)ParseInteger(logging["LOG_LEVEL"]);

                System.Collections.Specialized.NameValueCollection intcfg = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("globalsettings/manager");
                this.SOCKET_TIMEOUT = ParseInteger(intcfg["SOCKET_TIMEOUT"]);
                this.MANAGER_HOST = intcfg["MANAGER_HOST"];
                this.MANAGER_PORT = ParseInteger(intcfg["MANAGER_PORT"]);
                this.MANAGER_USER = intcfg["MANAGER_USER"];
                this.MANAGER_PASS = intcfg["MANAGER_PASS"];

                System.Collections.Specialized.NameValueCollection cticfg = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("globalsettings/ctiserver");
                this.CTI_CLIENT_TIMEOUT = ParseInteger(cticfg["CTI_CLIENT_TIMEOUT"]);
                this.CTI_CLIENT_KEEPALIVE = ParseInteger(cticfg["CTI_CLIENT_KEEPALIVE"]);
                this.CTI_SERVER_LISTEN_IP = cticfg["CTI_SERVER_LISTEN_IP"];
                this.CTI_SERVER_LISTEN_PORT = ParseInteger(cticfg["CTI_SERVER_LISTEN_PORT"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                
            }
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Retrieve a MySQL Connection string
        /// </summary>
        /// <param name="szHost">Database Host/Ip</param>
        /// <param name="szUser">Database Username</param>
        /// <param name="szPass">Database Password</param>
        /// <param name="szDBMS">Instance to open</param>
        /// <returns></returns>
        private String getConnectionString(String szHost , String szUser ,String szPass , String szDBMS,string timeOut ) 
		{
            
			String connStr = "";
            if (Server.IsMono() | Server.IsUnix())
            {
                connStr = "Server=" + szHost + ";" +
                        "Database=" + szDBMS + ";" +
                        "User ID=" + szUser + ";" +
                        "Password=" + szPass + ";" +
                        "Pooling=false";
                        
            }
            else
            {
                connStr = "Persist Security Info=False;" + 
                           "Server=" + szHost + ";" +
                        "Database=" + szDBMS + ";" +
                        "User ID=" + szUser + ";" +
                        "Password=" + szPass + ";" +
                        "Connect Timeout=" + timeOut;
                //connStr = "Persist Security Info=False;" +
                //        "database=" + szDBMS + ";" +
                //        "server=" + szHost + ";" +
                //        "user id=" + szUser + ";" +
                //        "Password=" + szPass + ";" +
                //        "Connect Timeout=" + timeOut;
            }
			return connStr;
		}


        /// <summary>
        /// Parse a path replacing some strings with current path
        /// or file separator
        /// </summary>
        /// <param name="path">The path to parse</param>
        /// <returns>The parsed path</returns>
        private string ParsePath(string path)
        {
            string strPath = "";
            if (path.IndexOf("{APPLICATION_DIR}") > -1)
            {
                strPath = path.Replace("{APPLICATION_DIR}", this.CURRENT_PATH);
                strPath = strPath.Replace("{FILE_SEPARATOR}", Path.DirectorySeparatorChar.ToString());
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(strPath);
                return dir.ToString();
            }
            return path;
        }

        /// <summary>
        /// Convert a string to a boolean value
        /// </summary>
        /// <param name="boolval">The string to convert</param>
        /// <returns>True or False</returns>
		private bool ParseBoolean(string boolval) 
		{
			return boolval.ToLower().Equals("true");
		}

        /// <summary>
        /// Try to cast an string value to an int
        /// </summary>
        /// <param name="intval">The string to convert</param>
        /// <returns>Converted value or zero</returns>
		public int ParseInteger(string intval) 
		{
			try 
			{
				return int.Parse(intval);
			} 
			catch(Exception) 
			{
				return 0;
			}
        }
        #endregion

    }
	
}
