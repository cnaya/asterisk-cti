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

namespace AstCTIClient
{
    #region Enums
    public enum PROTOCOL_STATES
    {
        STATUS_UNKNOWN = 0,
        OPENED_CONNECTION = 1,
        SENDED_USERNAME = 2,
        SENDED_PASSWORD = 3,
        LOGGED_IN = 4
    }
    #endregion
	/// <summary>
	/// Descrizione di riepilogo per ProtocolParser.
	/// </summary>
	public class ProtocolParser
	{
		
		public delegate void OnParsed(int message, string data);
		public event OnParsed Parsed;

        private static string buffer = "";

		public ProtocolParser()
		{
		}

		public int Parse(string data) 
		{
            int messageNum = 0;
			if (data.Length < 1) return 0;
            int.TryParse(data.Substring(0, 3), out messageNum);

            if (messageNum > 0)
            {
                string[] tokens = data.Split(' ');
                if (tokens.Length < 2) return 0;

                try
                {
                    int message = int.Parse(tokens[0]);
                    string tmpdata = data.Remove(0, tokens[0].Length + 1);

                    if (this.Parsed != null)
                    {
                        this.Parsed(message, tmpdata);

                    }
                    return message;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return 0;
                }
            }
            else
            {
                if (data.Contains("<event>"))
                {
                    buffer = "";
                }
                buffer += data;
                if (data.Contains("</event>")) {
                    this.Parsed(0, buffer);
                    buffer = "";
                }
                
            }
            return 0;
		}

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
	}
}
