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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AstCTIClient
{

    /// <summary>
    /// determines the meaning of the transparencyKey argument to the Convert method
    /// </summary>
    public enum TransparencyMode
    {
        /// <summary>
        /// the color key is used to define the transparent region of the bitmap
        /// </summary>
        ColorKeyTransparent,
        /// <summary>
        /// the color key is used to define the area that should _not_ be transparent
        /// </summary>
        ColorKeyOpaque
    }

	/// <summary>
	/// a class to convert a color-keyed bitmap into a region
	/// </summary>
	public class BitmapToRegion
	{
        /// <summary>
        /// ctor made private to avoid instantiation
        /// </summary>
        private BitmapToRegion()
        {}


        /// <summary>
        /// the meat of this class
        /// converts the bitmap to a region by scanning each line one by one
        /// this method will not affect the original bitmap in any way
        /// </summary>
        /// <param name="bitmap">The bitmap to convert</param>
        /// <param name="transparencyKey">The color which will indicate either transparency or opacity</param>
        /// <param name="mode">Whether the transparency key should indicate the transparent or the opaque region</param>
        public unsafe static Region Convert( Bitmap bitmap, Color transparencyKey,
            TransparencyMode mode )
        {
            //sanity check
            if ( bitmap == null )
                throw new ArgumentNullException( "Bitmap", "Bitmap cannot be null!" );

            //flag = true means the color key represents the opaque color
            bool modeFlag = ( mode == TransparencyMode.ColorKeyOpaque );
            
            GraphicsUnit unit = GraphicsUnit.Pixel;
            RectangleF boundsF = bitmap.GetBounds( ref unit );
            Rectangle bounds = new Rectangle( (int)boundsF.Left, (int)boundsF.Top, 
                (int)boundsF.Width, (int)boundsF.Height );

            uint key = (uint)((transparencyKey.A << 24) | (transparencyKey.R << 16) | 
                (transparencyKey.G << 8) | (transparencyKey.B << 0));


            //get access to the raw bits of the image
            BitmapData bitmapData = bitmap.LockBits( bounds, ImageLockMode.ReadOnly, 
                PixelFormat.Format32bppArgb );
            uint* pixelPtr = (uint*)bitmapData.Scan0.ToPointer();

            //avoid property accessors in the for
            int yMax = (int)boundsF.Height;
            int xMax = (int)boundsF.Width;

            //to store all the little rectangles in
            GraphicsPath path = new GraphicsPath();

            for ( int y = 0; y < yMax; y++ )
            {
                //store the pointer so we can offset the stride directly from it later
                //to get to the next line
                byte* basePos = (byte*)pixelPtr;

                for ( int x = 0; x < xMax; x++, pixelPtr++  )
                {      
                    //is this transparent? if yes, just go on with the loop
                    if ( modeFlag ^ ( *pixelPtr == key ) )
                        continue;

                    //store where the scan starts
                    int x0 = x;

                    //not transparent - scan until we find the next transparent byte
                    while( x < xMax && !( modeFlag ^ ( *pixelPtr == key ) ) )
                    {
                        ++x;
                        pixelPtr++;
                    }

                    //add the rectangle we have found to the path
                    path.AddRectangle( new Rectangle( x0, y, x-x0, 1 ) );
                }
                //jump to the next line
                pixelPtr = (uint*)(basePos + bitmapData.Stride);
            }

            //now create the region from all the rectangles
            Region region = new Region( path );

            //clean up
            path.Dispose();
            bitmap.UnlockBits( bitmapData );

            return region;
        } 

	}
  
}
