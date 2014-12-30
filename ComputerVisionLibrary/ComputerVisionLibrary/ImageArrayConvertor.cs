using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVisionLibrary
{
    /// <summary>
    /// Contains methods for array processing
    /// </summary>
    public class ImageArrayConvertor
    {
        /// <summary>
        /// Converts 2d array to 1d
        /// </summary>
        /// <param name="height">Height of picture</param>
        /// <param name="width">Width of image (Assuming whole pixel (not r g b a format))</param>
        /// <param name="originalArray">Array to be converted</param>
        /// <returns>Converted 2 dimentional array</returns>
        public static byte[,] Convert1Dto2D(int height, int width, byte[] originalArray)
        {
            //mult by 4 coz of r g b a format
            byte[,] blurImage = new byte[height, width * 4];
            int m = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width * 4; j++)
                {
                    blurImage[i, j] = originalArray[m++];
                }
            }
            return blurImage;
        }


        /// <summary>
        /// Converts 2dimentional array which represent image
        /// to 1dimentioanal
        /// </summary>
        /// <param name="height">Height of image</param>
        /// <param name="width">Width of image (Assuming whole pixel (not r g b a format))</param>
        /// <param name="originalArray">Array to be converted</param>
        /// <returns>Converted 1d array</returns>
        public static byte[] Convert2Dto1D(int height, int width, byte[,] originalArray)
        {
            //multiplyied by 4, coz of r g b a format 
            byte[] convertedImage = new byte[width * 4 * height];
            int l1 = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width * 4; j++)
                {
                    convertedImage[l1++] = originalArray[i, j];
                }
            }
            return convertedImage;
        }
    }
}
