using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerVisionLibrary;

namespace JacondiseMe
{
    public class Pyramid
    {
        /// <summary>
        /// Creates Gaussian Pyramide for one chanel
        /// </summary>
        /// <param name="color">One chanel from image</param>
        /// <param name="height">Height of image</param>
        /// <param name="width">Width of image</param>
        /// <param name="amountOfLayers">Amount of layers in Pyramid</param>
        /// <returns>Array of layers of pyramid</returns>
        public static byte[][,] Gaussian(byte[,] color, int height, int width, int amountOfLayers)
        {
            byte[][,] pyramide = new byte[amountOfLayers][,];
            pyramide[0] = new byte[color.GetLength(0), color.GetLength(1)];
            Array.Copy(color, pyramide[0], color.Length);
            for (int i = 1; i < amountOfLayers; i++)
            {
                  pyramide[i] = Reduce(pyramide[i-1]);
            }
                return pyramide;
        }//end of Gaussian

        /// <summary>
        /// Reduces level of pyramid
        /// </summary>
        /// <param name="temp">level to reduce</param>
        /// <returns>reduced level</returns>
        private static byte[,] Reduce(byte[,] temp)
        {
            int tempLength_0 = temp.GetLength(0);
            int tempLength_1 = temp.GetLength(1);
            int currLength_0, currLength_1;
            
            byte[] q = ArrayProcessing.ConvertTwoDimArrayInOneDim(temp, temp.GetLength(0), temp.GetLength(1));
            q = LinearFilter.ApplyKernelOnOneD2(q, tempLength_0, tempLength_1, KernelCollection.GaussianBlur);
            temp = ArrayProcessing.ConvertOneDimArrayToTwoDim2(q, tempLength_0, tempLength_1);
            
            currLength_0 = tempLength_0 / 2;
            currLength_1 = tempLength_1 / 2;
            byte[,] reduced = new byte[currLength_0, currLength_1];
            for (int l = 0; l < currLength_0; l++)
            {
                for (int m = 0; m < currLength_1; m++)
                {

                    reduced[l, m] = temp[2*l, 2 * m];
                }
                    
            }
            return reduced;
        }

        /// <summary>
        /// Fills the gaps for the erron-containing layer
        /// </summary>
        /// <param name="pyLevel">Level in pyramid</param>
        /// <returns>Layer that contains error</returns>
        private static byte[,] Expand(byte[,] pyLevel)
        {
            int iLength = pyLevel.GetLength(0)*2;
            int jLength = pyLevel.GetLength(1)*2;
            byte[,] level = new byte[iLength, jLength];
            int originalLength_0 = pyLevel.GetLength(0);
            int originalLength_1 = pyLevel.GetLength(1);
            int i1 = 0, j1 = 0;
            
            byte[] temp = ArrayProcessing.ConvertTwoDimArrayInOneDim(pyLevel, originalLength_0, originalLength_1);
            temp = LinearFilter.ApplyKernelOnOneD2(temp, originalLength_0, originalLength_1, KernelCollection.GaussianBlur);
            pyLevel = ArrayProcessing.ConvertOneDimArrayToTwoDim2(temp, originalLength_0, originalLength_1);
            
            for (int i = 0; i < originalLength_0; i++)
            {
                j1 = 0;
                for (int j = 0; j < originalLength_1; j++)
                {
                    level[i1, j1] = ImageProcessing.CheckPixel(pyLevel[i, j]);
                    level[i1, j1 + 1] = ImageProcessing.CheckPixel(pyLevel[i, j]);
                    level[i1 + 1, j1] =ImageProcessing.CheckPixel(pyLevel[i, j]);
                    level[i1 + 1, j1 + 1] =ImageProcessing.CheckPixel(pyLevel[i, j]);
                    j1 += 2;
                }
                i1 += 2;
            }//end of for
         
            return level;
        }
        
        /// <summary>
        /// Returns array which consists calculated errors
        /// between original layer of picta and expanded
        /// </summary>
        /// <param name="originalLayer">level in original size</param>
        /// <param name="expandedLayer">expanded blured original layer</param>
        /// <returns>Errors array</returns>
        private static byte[,] CountErrorsBetweenLayers(byte[,] originalLayer, byte[,] expandedLayer)
        {
            byte[,] result = new byte[originalLayer.GetLength(0), originalLayer.GetLength(1)];
            for (int i = 0; i < originalLayer.GetLength(0); i++)
            {
                for (int j = 0; j < originalLayer.GetLength(1); j++)
                {
                    result[i, j] = ImageProcessing.CheckPixel(originalLayer[i, j] - expandedLayer[i, j]);
                }//end of for
            }//end of for
            return result;
        }
        /// <summary>
        /// Constructs laplacian pyramid
        /// </summary>
        /// <param name="originalColor">one chanel from original image</param>
        /// <param name="amountOfLayers">amount of layers in pyramid</param>
        /// <returns>laplacian pyramid</returns>
        public static byte[][,] Laplacian(byte[,] originalColor, int amountOfLayers)
        {
            byte[][,] Py = Gaussian(originalColor, originalColor.GetLength(0), originalColor.GetLength(1), amountOfLayers);
            byte[][,] lPy = new byte[amountOfLayers][,];
            for (int i = 0; i < amountOfLayers - 1; i++)
            {
                lPy[i] = CountErrorsBetweenLayers(Py[i],Expand(Py[i + 1]));
            }
            lPy[amountOfLayers - 1] = Py[amountOfLayers - 1];
            return lPy;
        } 
        
        /// <summary>
        /// Creates New Pyramid from source laplacian pyramid,
        /// target laplacian pyramid and laplacian pyramid from 
        /// chosen area
        /// </summary>
        /// <param name="sourceLa">Source laplacian pyramid</param>
        /// <param name="targetLa">Target laplacian pyramid</param>
        /// <param name="gaussPy">Gauss pyramid of chosen area</param>
        /// <returns>New blended image pyramid</returns>
        public static byte[][,] CountResultPyramid(byte[][,] sourceLa, byte[][,] targetLa, byte[][,] gaussPy)
        {
            byte[][,] resultPyramid = new byte[sourceLa.Length][,];
            for (int i = 0; i < sourceLa.Length; i++)
            {
                resultPyramid[i] = new byte[sourceLa[i].GetLength(0), sourceLa[i].GetLength(1)];
                for (int l = 0; l < sourceLa[i].GetLength(0); l++)
                {
                    for (int m = 0; m < sourceLa[i].GetLength(1); m++)
                    {
                        resultPyramid[i][l, m] = 
                            (byte)((gaussPy[i][l, m]) * sourceLa[i][l, m] + (1 - gaussPy[i][l, m]) * targetLa[i][l, m]);
                    }//end of for
                }//end of for
            }//end of for
            return resultPyramid;
        }//end of CountResultPyramidl

        /// <summary>
        /// Collapsing laplacian pyramid
        /// </summary>
        /// <param name="lPy">Pyramid which you want to collapse</param>
        /// <returns>array with collapsed pyramid</returns>
        public static byte[,] CollapseLaplacian(byte[][,] lPy)
        {
            for (int i = lPy.Length - 1; i > 0; i--)
            {
                byte[,] temp = Expand(lPy[i]);
                lPy[i - 1] = Summator(temp, lPy[i - 1]);
            }
            return lPy[0];
        }
        
        /// <summary>
        /// summs two levels in laplacian pyramid
        /// </summary>
        /// <param name="first">Lower level</param>
        /// <param name="second">expanded level</param>
        /// <returns>summ of levels</returns>
        private static byte[,] Summator(byte[,] first, byte[,] second)
        {
            int i1 = first.GetLength(0);
            int j1 = first.GetLength(1);
            int i2 = second.GetLength(0);
            int j2 = second.GetLength(1);
            int diff_i = Math.Abs(i2 - i1);
            int diff_j = Math.Abs(j2 - j1);

            byte[,] sum = new byte[i1, j1];
            for (int i = 0; i < i1; i++ )
            {
                for (int j = 0; j < j1; j++)
                {
                    if (i >= (i2 -1) & j < (j2 -1))
                    {
                        sum[i, j] = ImageProcessing.CheckPixel(first[i, j] + second[i - diff_i, j]);
                    }
                    else if (j >= (j2 -1) & i < (i2 -1) )
                    {
                        sum[i, j] = ImageProcessing.CheckPixel(first[i, j] + second[i, j - diff_j]);
                    }
                    else if (i >= (i2 -1) & j >= (j2 - 1))
                    {
                        sum[i, j] = ImageProcessing.CheckPixel(first[i, j] + second[i - diff_i, j - diff_j]);
                    }
                    else
                    {
                        sum[i, j] = ImageProcessing.CheckPixel(first[i, j] + second[i, j]);
                    }
                }
            }
            return sum;
        }
       
        
    }

}
