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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AstCTIClient
{

	/// <summary>
	/// A Form that fades into view at creation, and fades out of view at destruction.
	/// </summary>
	public class FadingForm : System.Windows.Forms.Form
	{

		private bool m_fadeInFlag = false;

		private System.Windows.Forms.Timer m_fadeInOutTimer;

        private int pSpeed = 100;

		public FadingForm() : base()
		{
            this.m_fadeInOutTimer = new System.Windows.Forms.Timer();
            this.m_fadeInOutTimer.Tick += new System.EventHandler(this.m_fadeInOutTimer_Tick);           
		}

        public int Speed
        {
            get
            {
                return this.pSpeed;
            }
            set
            {
                this.pSpeed = value;
                this.m_fadeInOutTimer.Interval = this.pSpeed;
            }
        }
		protected override void OnLoad(EventArgs e)
		{

			base.OnLoad(e);

			// Should we start fading?
			if (!DesignMode)
			{
				
				m_fadeInFlag = true;
				Opacity = 0;
				
				m_fadeInOutTimer.Enabled = true;

			}						
		} 

		protected override void OnClosing(CancelEventArgs e)
		{
			
			base.OnClosing(e);

			// If the user canceled then don't fade anything.
			if (e.Cancel == true)
				return;

			// Should we fade instead of closing?
			if (Opacity > 0)
			{
				m_fadeInFlag = false;
				m_fadeInOutTimer.Enabled = true;
				e.Cancel = true;
			} // End if we should fade instead of closing.

		} 

		
		private void m_fadeInOutTimer_Tick(object sender, System.EventArgs e)
		{
            try
            {
                // How should we fade?
                if (m_fadeInFlag == false)
                {

                    Opacity -= (m_fadeInOutTimer.Interval / 100.0);

                    // Should we continue to fade?
                    if (this.Opacity > 0)
                        m_fadeInOutTimer.Enabled = true;
                    else
                    {

                        m_fadeInOutTimer.Enabled = false;
                        Close();

                    } // End else we should close the form.

                } // End if we should fade in.
                else
                {

                    Opacity += (m_fadeInOutTimer.Interval / 100.0);
                    m_fadeInOutTimer.Enabled = (Opacity < 1.0);
                    m_fadeInFlag = (Opacity < 1.0);

                } // End else we should fade out.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
		
		} 

		
		private void _DoNothing() { }

	} 

} 
