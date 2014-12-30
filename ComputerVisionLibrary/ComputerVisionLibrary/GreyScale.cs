using System;

namespace ComputerVisionLibrary
{
    /// <summary>
    /// This class contains methods
    /// for converting image to gray-scale
    /// </summary>
    public class GreyScale
    {
        /// <summary>
        /// Converts image to gray-scale
        /// </summary>
        /// <param name="originalImage">image to be converted</param>
        /// <returns>converted image</returns>
        public static byte[] SetGrayscale(byte[] originalImage)
        {
            //x = 0.299R + 0.587G + 0.114B
            byte[] grayScale = new byte[originalImage.Length / 4];
            for (int i = 0, j = 0; i < originalImage.Length; )
            {
                grayScale[j] = (byte)(0.114 * originalImage[i] + 0.587 * originalImage[i + 1] + 0.299 * originalImage[i + 2]);
                i = i + 4;
                j = i / 4;
            }//for
            return grayScale;
        }//SetGrayScale
    }
}
