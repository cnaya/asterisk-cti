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

// Many Thanks to: Niels Penneman
// http://www.codeproject.com/KB/combobox/ImageCombo_NET.aspx

using System;
using System.Drawing;

namespace System.Windows.Forms
{

	public class ImageComboItem : object
	{
		// forecolor: transparent = inherit
		private Color forecolor = Color.FromKnownColor(KnownColor.Transparent);
		private bool mark = false;
		private int imageindex = -1;
		private object tag = null;
		private string text = null;		
		
		// constructors
		public ImageComboItem()
		{
		}

		public ImageComboItem(string Text) 
		{
			text = Text;	
		}

		public ImageComboItem(string Text, int ImageIndex)
		{
			text = Text;
			imageindex = ImageIndex;
		}

		public ImageComboItem(string Text, int ImageIndex, bool Mark)
		{
			text = Text;
			imageindex = ImageIndex;
			mark = Mark;
		}

		public ImageComboItem(string Text, int ImageIndex, bool Mark, Color ForeColor)
		{
			text = Text;
			imageindex = ImageIndex;
			mark = Mark;
			forecolor = ForeColor;
		}

		public ImageComboItem(string Text, int ImageIndex, bool Mark, Color ForeColor, object Tag)
		{
			text = Text;
			imageindex = ImageIndex;
			mark = Mark;
			forecolor = ForeColor;
			tag = Tag;
		}

		// forecolor
		public Color ForeColor 
		{
			get 
			{
				return forecolor;
			}
			set
			{
				forecolor = value;
			}
		}

		// image index
		public int ImageIndex 
		{
			get 
			{
				return imageindex;
			}
			set 
			{
				imageindex = value;
			}
		}

		// mark (bold)
		public bool Mark
		{
			get
			{
				return mark;
			}
			set
			{
				mark = value;
			}
		}

		// tag
		public object Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		// item text
		public string Text 
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}
		
		// ToString() should return item text
		public override string ToString() 
		{
			return text;
		}

	}

}
