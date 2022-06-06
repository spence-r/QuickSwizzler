/* Spence! 2020 */
/* commonspence.com */

using System;
using System.Drawing;
using System.IO;
using System.Windows;

namespace QuickSwizzler
{
    /// <summary>
    /// QuickSwizzle tool behaviours
    /// </summary>
    public static class QuickSwizzle
    {
        public static MessageBoxResult ConfirmSwizzle()
        {
            // confirm swizzling if not running a batch operation.
            return MessageBox.Show( "About to swizzle and save the currently selected file " +
                "with the current R/G/B channel settings. This will ALWAYS OVERWRITE any files " +
                "in the destination directory and could be destructive (loss of channel data or original files). Proceed?", "",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        }

        public static MessageBoxResult ConfirmBatchSwizzle()
        {
            // confirm swizzling if not running a batch operation.
            return MessageBox.Show("About to swizzle and save all of the files in the list " +
                "with the current R/G/B channel settings. This will ALWAYS OVERWRITE any files " +
                "in the destination directory and could be destructive (loss of channel data or original files). Proceed?", "",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        }

        /// <summary>
        /// Validate the export path
        /// </summary>
        /// <returns>True if the path is valid (not empty or null)</returns>
        public static bool ValidateExportPath()
        {
            if (SwizzleSettings.Files.ExportPath == null || SwizzleSettings.Files.ExportPath == "")
            {
                MessageBox.Show("Select an export path first.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates a swizzled image, then saves it to the output directory
        /// </summary>
        /// <param name="imgPath">Path to the source image to be swizzled</param>
        /// <param name="asCopy">Save image as a copy?</param>
        public static void SwizzleAndSaveImage(string imgPath, bool asCopy = false)
        {        
            if (imgPath == null || imgPath == "")
            {
                // return early if the image path is null
                System.Media.SystemSounds.Asterisk.Play();
                return;
            }
            else
            {
                try
                {
                    Image srcImage = Image.FromFile(imgPath);

                    // extract the SwizzleChannels (R/G/B separated bitmaps) from the source image
                    SwizzleChannels swzCh = new SwizzleChannels(srcImage);

                    // swizzle the image based on the selected output settings
                    Bitmap swizzledImage = ConstructSwizzledImage(swzCh, srcImage);

                    // setup graphics surface & properties
                    Graphics g = Graphics.FromImage(swizzledImage);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    // render and save image
                    g.DrawImage(swizzledImage, new Rectangle(0, 0, swizzledImage.Width, swizzledImage.Height));

                    // set the path string with a _swz suffix depending on whether or not we wanted to save it as copy
                    string path = asCopy ?
                        SwizzleSettings.Files.ExportPath + "\\" + 
                            Path.GetFileNameWithoutExtension(imgPath) + "_swz.tiff" 
                            : 
                        SwizzleSettings.Files.ExportPath + "\\" + 
                            Path.GetFileNameWithoutExtension(imgPath) + ".tiff";

                    // save to [export dir/filename.tiff]
                    swizzledImage.Save(path, System.Drawing.Imaging.ImageFormat.Tiff);

                    return;
                }
                catch (Exception e)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    return;
                }
            }
        }

        /// <summary>
        /// Make a swizzled bitmap where each pixel's color is set from the target channels 
        ///         specified in <<see cref="SwizzleSettings"/>
        /// </summary>
        /// <param name="swizzleChan">the <see cref="SwizzleChannels"/></param>
        /// <param name="width">Width of source image</param>
        /// <param name="height">Height of source image</param>
        /// <returns>A swizzled bitmap based on <see cref="SwizzleSettings"/></returns>
        public static Bitmap ConstructSwizzledImage(SwizzleChannels swizzleChan, Image srcImage)
        {                                                   
            Bitmap bmp = new Bitmap(srcImage);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    // pack the bitmap
                    bmp.SetPixel(x, y, Color.FromArgb(255,
                        swizzleChan.GetPixelChannelValue(SwizzleSettings.RedChannelTarget, x, y),
                        swizzleChan.GetPixelChannelValue(SwizzleSettings.GreenChannelTarget, x, y),
                        swizzleChan.GetPixelChannelValue(SwizzleSettings.BlueChannelTarget, x, y)
                        ));
                }
            }

            return bmp;
        }
    }
}
