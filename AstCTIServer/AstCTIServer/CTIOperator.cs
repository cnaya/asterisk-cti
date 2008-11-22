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
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace AstCTIServer
{
    /// <summary>
    /// This Class represent a CTI Operator. It Helps to authenticate
    /// CTI Users from credential, reading the MySQL Database
    /// </summary>
    class CTIOperator
    {
        #region Public Variables
        public string USERNAME; // Username
        public string SECRET;   // Password
        public string HOST;     // Remote Host
        public string EXT;      // CTI Operator Extension
        #endregion  

        #region Constructor
        public CTIOperator()
        {
            this.Initialize();
        }
        #endregion

        #region Init and Load
        /// <summary>
        /// Reset the public variables to default values
        /// </summary>
        public void Initialize()
        {
            this.USERNAME = "";
            this.SECRET = "";
            this.HOST = "";
            this.EXT = "";

        }

        /// <summary>
        /// Load a CTI Operator from credentials
        /// </summary>
        /// <param name="username">Operator' Username</param>
        /// <param name="password">Operator' Secret</param>
        /// <returns>True if loaded; Otherwise false</returns>
        public bool LoadFromCredential(string username, string password)
        {
            MySqlCommand cmd = null;
            MySqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                if (Server.cn.State != ConnectionState.Open)
                {
                    return false;
                }

                string sql = "SELECT * FROM cti WHERE USERNAME=?user AND SECRET=?secret";
                cmd = new MySqlCommand(sql, Server.cn);
                cmd.Parameters.Add(new MySqlParameter("?user", username));
                cmd.Parameters.Add(new MySqlParameter("?secret", password));
                cmd.Prepare();

                da = new MySqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "cti");
                dt = ds.Tables["cti"];
                if (dt.Rows.Count < 1)
                {
                    if (dt != null) dt.Dispose();
                    if (da != null) da.Dispose();
                    if (ds != null) ds.Dispose();
                    if (cmd != null) cmd.Dispose();
                    return false;
                }
                else
                {
                    if (dt.Rows[0]["USERNAME"] != null) this.USERNAME = (string)dt.Rows[0]["USERNAME"];
                    if (dt.Rows[0]["SECRET"] != null) this.SECRET = (string)dt.Rows[0]["SECRET"];
                    if (dt.Rows[0]["HOST"] != null) this.HOST = (string)dt.Rows[0]["HOST"];
                    if (dt.Rows[0]["EXT"] != null) this.EXT = (string)dt.Rows[0]["EXT"];
                    if (dt != null) dt.Dispose();
                    if (da != null) da.Dispose();
                    if (ds != null) ds.Dispose();
                    if (cmd != null) cmd.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Server.logger.WriteLine(LogType.Error, string.Format("Exception in LoadFromCredential: {0}", ex.Message));
                if (dt != null) dt.Dispose();
                if (dt != null) da.Dispose();
                if (dt != null) ds.Dispose();
                if (dt != null) cmd.Dispose();
                return false;
            }
        }
        #endregion

        #region Static Helper Functions

        /// <summary>
        /// Calculate the MD5 from a string
        /// </summary>
        /// <param name="str">The string to evaluate</param>
        /// <returns>MD5 String Value</returns>
        public static string MD5(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(str);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        #endregion

    }
}
