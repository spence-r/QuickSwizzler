/* Spence! 2020 */
/* commonspence.com */

using System;
using System.Drawing;

namespace QuickSwizzler
{
    /// <summary>
    /// A set of R/G/B channels to be swizzled
    /// </summary>
    public class SwizzleChannels
    {
        // R/G/B bitmaps
        protected Bitmap Red;
        protected Bitmap Green;
        protected Bitmap Blue;

        /// <summary>
        /// Constructs a SwizzleChannels using the given image's R/G/B channels
        /// </summary>
        /// <param name="img">The image to read bitmap data from</param>
        public SwizzleChannels(Image img)
        {
            // initialize R, G, B by extracting the full-color bitmap from img
            // note - this extracted image data won't be used at all, we only treat it as a template for the bitmap size/format
            Red = new Bitmap(img);
            Green = new Bitmap(img);
            Blue = new Bitmap(img);

            // update each R, G, B to zero out the non-R/G/B component for each channel
            ExtractChannelBitmaps(img);
        }

        /// <summary>
        /// Gets value (0-255) of the specified R/G/B channel of the pixel at [x,y]. 
        /// </summary>
        /// <param name="ch">Color channel to evaluate</param>
        /// <param name="x">pixel's x-coord</param>
        /// <param name="y">pixel's x-coord</param>
        /// <returns>The value of color channel ch for the pixel at [x,y]</returns>
        public int GetPixelChannelValue(Channel ch, int x, int y)
        {
            int pixelColorValue = 0;
            Color c;

            switch (ch)
            {
                case Channel.Red:
                    c = Red.GetPixel(x, y);
                    pixelColorValue = c.R;
                    break;
                case Channel.Green:
                    c = Green.GetPixel(x, y);
                    pixelColorValue = c.G;
                    break;
                case Channel.Blue:
                    c = Blue.GetPixel(x, y);
                    pixelColorValue = c.B;
                    break;
            }

            return pixelColorValue;
        }

        /// <summary>
        /// Initialize the R/G/B Bitmaps by stripping the R/G/B channel components as necessary
        /// </summary>
        /// <param name="img">The image to read bitmap data from</param>
        protected void ExtractChannelBitmaps(Image img)
        {
            // load reference Bitmap from the Image
            Bitmap bmp = new Bitmap(img);

            // iterate through each pixel of the bitmap
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    // get the pixel and its A/R/G/B values
                    Color px = bmp.GetPixel(x, y);
                    int a = px.A;
                    int r = px.R;
                    int g = px.G;
                    int b = px.B;

                    // set the pixel's channel values from the extracted A/R/G/B values
                    Red.SetPixel(x, y, Color.FromArgb(a, r, 0, 0)); // aroo!
                    Green.SetPixel(x, y, Color.FromArgb(a, 0, g, 0));
                    Blue.SetPixel(x, y, Color.FromArgb(a, 0, 0, b));
                }
            }

        }

    }

}
