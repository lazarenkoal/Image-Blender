using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacondiseMe
{
    public class Preprocessing
    {
        /// <summary>
        /// Return bynary-converted matrix
        /// </summary>
        /// <param name="arr">Matrix to convert</param>
        /// <returns>converted matrix</returns>
        public static byte[,] ApplyBinary(byte[,] arr)
        {
            byte[,] binary = new byte[arr.GetLength(0), arr.GetLength(1)];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    binary[i, j] = SetBinary(arr[i, j]);
                }// end of for
            }//enf of for
            return binary;
        }


        /// <summary>
        /// Returns bunary pixel value
        /// </summary>
        /// <param name="pixel">value of inputed pixel</param>
        /// <returns>binary value</returns>
        private static byte SetBinary(byte pixel)
        {
            if (pixel > 100)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
