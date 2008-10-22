// Copyright (C) 2007 Bruno Salzano
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
using System.Text.RegularExpressions;

namespace AstCTIServer
{
    /// <summary>
    /// This is an helper class for managing IPAddress Object
    /// </summary>
    public class RemoteAddress
    {
        #region Private Variables
        private string pAddress = "";
        private int pPort = 0;
        #endregion

        #region Constructors
        public RemoteAddress()
        {
        }

        public RemoteAddress(string remAddress)
        {
            this.ParseAddress(remAddress);
        }
        #endregion

        #region Read-Only properties

        /// <summary>
        /// The IP Address
        /// </summary>
        public string Address
        {
            get { return this.pAddress; }
        }

        /// <summary>
        /// Ip Address as System.Net.IPAddress object
        /// </summary>
        public System.Net.IPAddress IPAddress
        {
            get
            {
                try
                {
                    return System.Net.IPAddress.Parse(this.pAddress);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Port
        /// </summary>
        public int Port
        {
            get { return this.pPort; }
        }
        #endregion

        #region Funzioni private di supporto
        /// <summary>
        /// Parse a string and fills private
        /// variables
        /// </summary>
        /// <param name="data">String to parse</param>
        private void ParseAddress(string data)
        {
            if (data.IndexOf(":") < 1) return;

            try
            {
                string[] tmp = data.Split(':');
                if (tmp == null) return;
                if (tmp.Length < 2) return;

                this.pAddress = tmp[0];
                this.pPort = int.Parse(tmp[1]);

            }
            catch (Exception) { }
        }
        #endregion

        #region Static methods
        public static RemoteAddress Parse(string data)
        {
            return new RemoteAddress(data);
        }

        public static bool IsIP(string addr)
        {
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            Regex check = new Regex(pattern);
            if (addr == "") return false;

            return check.IsMatch(addr, 0);

        } 
        #endregion
    }
}
