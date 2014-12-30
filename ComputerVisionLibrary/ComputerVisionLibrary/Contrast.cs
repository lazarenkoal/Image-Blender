using System;


namespace ComputerVisionLibrary
{
    /// <summary>
    /// This class contains methods
    /// for setting and editing image's
    /// contrast level
    /// </summary>
    public class Contrast
    {
        /// <summary>
        /// Changes contrast of image
        /// </summary>
        /// <param name="originalImage">Original image (in bytes)</param>
        /// <param name="coef">Value for changing the level of contrast</param>
        /// <returns>Image in bytearray with changed contrast level</returns>
        public static byte[] SetContrast(byte[] originalImage, int coef)
        {
            //contrastLevel = ((100.0 + coef)/100.0)^2
            // newPixel = (((oldPixel/255.0 - 0.5) * contastLevel) + 0.5) * 255.0
            //bgra is default image format

            double contrastLevel = Math.Pow(((100.0 + coef) / 100.0), 2);
            byte[] contrastedImage = new byte[originalImage.Length];

            for (int i = 0; i < contrastedImage.Length; )
            {
                contrastedImage[i] = PixelProcessing.CheckPixelValue((int)((((originalImage[i] / 255.0 - 0.5) * contrastLevel) + 0.5) * 255.0));
                contrastedImage[i + 1] = PixelProcessing.CheckPixelValue((int)((((originalImage[i + 2] / 255.0 - 0.5) * contrastLevel) + 0.5) * 255.0));
                contrastedImage[i + 2] = PixelProcessing.CheckPixelValue((int)((((originalImage[i + 2] / 255.0 - 0.5) * contrastLevel) + 0.5) * 255.0));
                contrastedImage[i + 3] = originalImage[i + 3];
                i += 4;
            }//for
            return contrastedImage;
        }//setContrast()
    }
}
