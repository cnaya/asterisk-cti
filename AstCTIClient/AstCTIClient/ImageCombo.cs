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

	public class ImageCombo : ComboBox
	{
		private ImageList imgs = new ImageList();

		// constructor
		public ImageCombo()
		{
			// set draw mode to owner draw
			this.DrawMode = DrawMode.OwnerDrawFixed;	
		}

		// ImageList property
		public ImageList ImageList 
		{
			get 
			{
				return imgs;
			}
			set 
			{
				imgs = value;
			}
		}

		// customized drawing process
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			// draw background & focus rect
			e.DrawBackground();
			e.DrawFocusRectangle();

			// check if it is an item from the Items collection
			if (e.Index < 0)

				// not an item, draw the text (indented)
				e.Graphics.DrawString(this.Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + imgs.ImageSize.Width, e.Bounds.Top);

			else
			{
				
				// check if item is an ImageComboItem
				if (this.Items[e.Index].GetType() == typeof(ImageComboItem)) 
				{															

					// get item to draw
					ImageComboItem item = (ImageComboItem) this.Items[e.Index];

					// get forecolor & font
					Color forecolor = (item.ForeColor != Color.FromKnownColor(KnownColor.Transparent)) ? item.ForeColor : e.ForeColor;
					Font font = item.Mark ? new Font(e.Font, FontStyle.Bold) : e.Font;

					// -1: no image
					if (item.ImageIndex != -1) 
					{
						// draw image, then draw text next to it
						this.ImageList.Draw(e.Graphics, e.Bounds.Left, e.Bounds.Top, item.ImageIndex);
						e.Graphics.DrawString(item.Text, font, new SolidBrush(forecolor), e.Bounds.Left + imgs.ImageSize.Width, e.Bounds.Top);
					}
					else
						// draw text (indented)
						e.Graphics.DrawString(item.Text, font, new SolidBrush(forecolor), e.Bounds.Left + imgs.ImageSize.Width, e.Bounds.Top);

				}
				else
				
					// it is not an ImageComboItem, draw it
					e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + imgs.ImageSize.Width, e.Bounds.Top);
				
			}

			base.OnDrawItem (e);
		}
		
	}

}