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
using Microsoft.Win32;      
using System.Windows.Forms; 

namespace AstCTIClient
{
	/// <summary>
	/// An useful class to read/write/delete/count registry keys
	/// </summary>
	public class RegistryManager
	{
		#region Private vars
		private bool showError = false;
		private string subKey = "";
		private RegistryKey baseRegistryKey = Registry.LocalMachine;
		
		#endregion

		#region Costruttore
		public RegistryManager() 
		{
			this.subKey = "SOFTWARE\\" + Application.ProductName.ToUpper();
			this.baseRegistryKey =  Registry.LocalMachine;
			this.showError = false;
		}

		public RegistryManager(bool bShowErrors)
		{
			this.showError = bShowErrors;
		}

		public RegistryManager(bool bShowErrors, string subKey)
		{
			this.showError = bShowErrors;
			this.subKey = subKey;
		}

		public RegistryManager(bool bShowErrors, RegistryKey regKey, string subKey) 
		{
			this.showError = bShowErrors;
			this.baseRegistryKey = regKey;
			this.subKey = subKey;
			
		}
		#endregion

		#region Public properties
		public bool ShowError
		{
			get { return showError; }
			set	{ showError = value; }
		}

		
		public string SubKey
		{
			get { return subKey; }
			set	{ subKey = value; }
		}

		public RegistryKey BaseRegistryKey
		{
			get { return baseRegistryKey; }
			set	{ baseRegistryKey = value; }
		}
		#endregion
		
		

		public string Read(string KeyName)
		{
			RegistryKey rk = baseRegistryKey ;
			RegistryKey sk1 = rk.OpenSubKey(subKey);
			
			if ( sk1 == null ) return null;
			
			try 
			{
				// If the RegistryKey exists I get its value
				// or null is returned.
				return (string)sk1.GetValue(KeyName.ToUpper());
			}
			catch (Exception e)
			{
				ErrMsg(e, "Reading registry " + KeyName.ToUpper());
				return null;
			}
		}
		
		public object ReadObject(string KeyName)
		{
			RegistryKey rk = baseRegistryKey ;
			RegistryKey sk1 = rk.OpenSubKey(subKey);
			
			if ( sk1 == null ) return null;
			
			try 
			{
				// If the RegistryKey exists I get its value
				// or null is returned.
				return sk1.GetValue(KeyName.ToUpper());
			}
			catch (Exception e)
			{
				ErrMsg(e, "Reading registry " + KeyName.ToUpper());
				return null;
			}
		}

		public bool Write(string KeyName, object Value)
		{
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.CreateSubKey(subKey);
				sk1.SetValue(KeyName.ToUpper(), Value);
				return true;
			}
			catch (Exception e)
			{
				ErrMsg(e, "Writing registry " + KeyName.ToUpper());
				return false;
			}
		}

		public bool DeleteKey(string KeyName)
		{
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.CreateSubKey(subKey);
				if ( sk1 == null )
					return true;
				else
					sk1.DeleteValue(KeyName);

				return true;
			}
			catch (Exception e)
			{
				ErrMsg(e, "Deleting SubKey " + subKey);
				return false;
			}
		}

		public bool DeleteSubKeyTree()
		{
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
				
				if ( sk1 != null ) rk.DeleteSubKeyTree(subKey);
				return true;
			}
			catch (Exception e)
			{
				ErrMsg(e, "Deleting SubKey " + subKey);
				return false;
			}
		}

		public int SubKeyCount()
		{
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
				if ( sk1 != null ) return sk1.SubKeyCount;
				
				return 0; 
			}
			catch (Exception e)
			{
				ErrMsg(e, "Retriving subkeys of " + subKey);
				return 0;
			}
		}

		public int ValueCount()
		{
			try
			{
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
				
				if ( sk1 != null ) return sk1.ValueCount;
				
				return 0; 
			}
			catch (Exception e)
			{
				ErrMsg(e, "Retriving keys of " + subKey);
				return 0;
			}
		}

		private void ErrMsg(Exception e, string Title) 
		{
			if (this.showError == true) 
				MessageBox.Show(e.Message,Title,MessageBoxButtons.OK,MessageBoxIcon.Error);
			else
				throw e;
		}
	}
}
