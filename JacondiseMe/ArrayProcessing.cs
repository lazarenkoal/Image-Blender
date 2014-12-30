using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacondiseMe
{
    class ArrayProcessing
    {
        //bgra chanels
        /// <summary>
        /// This method converts one-dimentional byte array
        /// to two - dimentional byte array
        /// </summary>
        /// <param name="byteArray">byte array to be converted</param>
        /// <param name="height">height of image</param>
        /// <param name="width">width of image (mult on 4 is into account)</param>
        /// <returns>matrix of image's pixels</returns>
        public static byte[,] ConvertOneDimArrayToTwoDim(byte[] byteArray, int height, int width)
        {
            byte[,] resultedArray = new byte[height, 4 * width];
            int index = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width * 4; j++)
                {
                    resultedArray[i, j] = byteArray[index++];
                }//end of Jfor
            }//end of Ifor
            return resultedArray;
        }//end of ConvertOne...

        /// <summary>
        /// it is the same as previous one, but it is for one chanel
        /// </summary>
        /// <param name="byteArray">array to be converted</param>
        /// <param name="height">height of picture</param>
        /// <param name="width">width of picture (without multiplication on 4)</param>
        /// <returns>converted array</returns>
        public static byte[,] ConvertOneDimArrayToTwoDim2(byte[] byteArray, int height, int width)
        {
            byte[,] resultedArray = new byte[height, width];
            int index = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    resultedArray[i, j] = byteArray[index++];
                }//end of Jfor
            }//end of Ifor
            return resultedArray;
        }//end of ConvertOne...

        /// <summary>
        /// Converts Two-dim array in one-dim
        /// </summary>
        /// <param name="byteArray">Matrix of image</param>
        /// <param name="height">height of image</param>
        /// <param name="width">widht of image</param>
        /// <returns>one-dim array of image</returns>
        public static byte[] ConvertTwoDimArrayInOneDim(byte[,] byteArray, int height, int width)
        {
            byte[] resultedArray = new byte[height * width];
            int index = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    resultedArray[index++] = byteArray[i, j];
                }//end of for j
            }//end of for i
            return resultedArray;
        }//end of ConvertTwoDimArrayInOneDim
    }
}
