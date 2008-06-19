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

namespace AstCTIServer
{
	/// <summary>
	/// Protocol Commands Parsing
	/// </summary>
	public class ProtocolParser
	{		
		/// <summary>
		/// This function parse strings with commands
		/// </summary>
		/// <param name="in_data">Client data</param>
		/// <param name="out_data">Data without command</param>
		/// <returns>The parsed command if found; otherwise null</returns>
		public static string Parse(string in_data, ref string out_data) 
		{
			string command = "";
			string data = "";
			
			if (in_data.Length < 1) return null;

			string []tokens = in_data.Split(' ');
			if (tokens.Length < 1) 
				return in_data;
			
			try 
			{
				command = tokens[0];
				if (tokens.Length > 1) 
				{
					data = in_data.Remove(0, command.Length+1);
					out_data = data;
				}
				return command;
			} 
			catch(Exception ex) 
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}
	}
}
