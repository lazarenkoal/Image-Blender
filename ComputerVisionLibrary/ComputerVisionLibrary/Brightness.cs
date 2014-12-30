using System;

namespace ComputerVisionLibrary
{
    /// <summary>
    /// Class contains methods for
    /// changing and editing Brightness
    /// </summary>
    public class Brightness
    {
        /// <summary>
        /// Method sets image's brightness level
        /// </summary>
        /// <param name="originalImage">image to be chainged</param>
        /// <param name="coef">value of level</param>
        /// <returns>changed image</returns>
        public static byte[] SetBrightness(byte[] originalImage, int coef)
        {
            // newPixel = oldPixel + coef
            byte[] brightImage = new byte[originalImage.Length];
            for (int i = 0; i < originalImage.Length; )
            {
                brightImage[i] = PixelProcessing.CheckPixelValue(originalImage[i] + coef);
                brightImage[i + 1] = PixelProcessing.CheckPixelValue(originalImage[i + 1] + coef);
                brightImage[i + 2] = PixelProcessing.CheckPixelValue(originalImage[i + 2] + coef);
                brightImage[i + 3] = originalImage[i + 3];
                i += 4;
            }//for
            return brightImage;
        }//SetBrightness()
    }
}
