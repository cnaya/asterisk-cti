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
using System.Data;
using System.Reflection;
using System.IO;
	
namespace AstCTIServer
{	
		
	public enum LogType {
		
		Info = 0x1,
		Notice = 0x2,
		Warning = 0x4,
		Error = 0x8,
		Fatal = 0x10,
        Debug = 0x20,
        DebugSocket = 0x40
	}
	
    /// <summary>
    /// This class is responsible of logging on external txt files
    /// </summary>
	public class FileLogger
    {

        #region Private Variables
        private bool isReady=false;
		private StreamWriter swLog;
		private string strLogFile;
        
		private LogType pLogLevel = LogType.Error | LogType.Info;
        #endregion

        #region Public Properties
        public LogType LogLevel
        {
            get { return this.pLogLevel; }
            set
            {
                if ((object)value != null)
                {
                    this.pLogLevel = value;
                }
            }
        }
        #endregion

        #region Constructor
        public FileLogger() {
            this.pLogLevel = Server.cfg.LOG_LEVEL;
        }
        #endregion

        #region File Open & Close
        private bool OpenFile() {
			try {
                FileStream fs = File.Open(strLogFile, FileMode.Append, FileAccess.Write, FileShare.Write);

				swLog = new StreamWriter(fs);
				isReady = true;
			} catch(Exception ex) {
                Console.WriteLine(ex.ToString());
				isReady = false;
				return false;
			}
			return true;
		}
		
		private void CloseFile() {
			
			if(isReady) {
				try {
					swLog.Close();
				} catch {
					
				}
			}
        }
        #endregion

        #region Log Filename Building
        private void GetLogFilename() {

            this.strLogFile = Server.cfg.LOG_DIRECTORY + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
        }
        #endregion

        #region WriteLine

        public void WriteLine(string message) {
			this.WriteLine(LogType.Info,message);
		}

		public void WriteLine(LogType logtype,string message) {
			try {
				string stub = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss - ") ;
				if ((this.pLogLevel & logtype) == logtype ) {
					switch(logtype) {
						case LogType.Info:
							stub += "INFO  : ";
							break;
						case LogType.Notice:
							stub += "NOTICE: ";
							break;
						case LogType.Warning:
							stub += "WARN  : ";
							break;
						case LogType.Error:
							stub += "ERROR: ";
							break;
						case LogType.Fatal:
							stub += "FATAL : ";
							break;
                        case LogType.Debug:
                            stub += "DEBUG : ";
                            break;
                        case LogType.DebugSocket:
                            stub += "DSOCK : ";
                            break;
					}
					stub += message;
					this.GetLogFilename();
					if (!this.OpenFile()) {
						Console.WriteLine("ERROR IN FileLogger.WriteLine: cannot open log file");
						return;
					} 
					swLog.WriteLine(stub);
					this.CloseFile();
				}
			} catch(Exception ex) {
				Console.WriteLine("ERROR IN FileLogger.WriteLine: " + ex.ToString());
			}
        }
        #endregion

    }
}
