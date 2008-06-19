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
// AstCTIClient.  If you copy code from other releases into a copy of GNU
// AstCTIClient, as the General Public License permits, the exception does
// not apply to the code that you add in this way.  To avoid misleading
// anyone as to the status of such modified files, you must delete
// this exception notice from them.
//
// If you write modifications of your own for AstCTIClient, it is your choice
// whether to permit this exception to apply to your modifications.
// If you do not wish that, delete this exception notice.
//
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Runtime.InteropServices;
using System.Management;

namespace AstCTIClient
{
	/// <summary>
	/// Description of WMI.
	/// </summary>
	public class WMI
	{
		public static string[] MacAddresses() {
			ArrayList lst = new ArrayList();
			string pcname = WMI.ComputerName();
			ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection moc = mc.GetInstances();
			foreach(ManagementObject mo in moc)
			{
				if((bool)mo["IPEnabled"] == true) {
					string []addresses = mo["IPAddress"] as string[];
					if (addresses.Length > 0) {
						if (!addresses[0].Equals("0.0.0.0")) {
							lst.Add(pcname + "," + addresses[0] + "," +  mo["MacAddress"].ToString());
						}
					}
					
				}
				mo.Dispose();
			}
			moc.Dispose();
			mc.Dispose();
			return lst.ToArray(typeof(String)) as string[];

		}

        public static string[] IPAddresses()
        {
            ArrayList lst = new ArrayList();
            string pcname = WMI.ComputerName();
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    string[] addresses = mo["IPAddress"] as string[];
                    if (addresses.Length > 0)
                    {
                        if (!addresses[0].Equals("0.0.0.0"))
                        {
                            lst.Add(addresses[0]);
                        }
                    }

                }
                mo.Dispose();
            }
            moc.Dispose();
            mc.Dispose();
            return lst.ToArray(typeof(String)) as string[];

        }
		
		public static string ComputerName() {
			string computerName = "";
			ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
			ManagementObjectCollection moc = mc.GetInstances();
			if (moc.Count>0) {
				foreach(ManagementObject mo in moc) {
					computerName = (string)mo["Name"].ToString();
					mo.Dispose();
				}
				
			}
			moc.Dispose();
			moc.Dispose();
			mc.Dispose();
			return computerName;
		}
	}
}
