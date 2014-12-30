using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerVisionLibrary;
namespace JacondiseMe
{
    public class ImageProcessing
    {
        /// <summary>
        /// Makes up Jaconda's face
        /// </summary>
        /// <param name="sourceImage">Image from which we'll take a part</param>
        /// <param name="sourceImageHeight">Height of source image</param>
        /// <param name="sourceImageWidth">Width of source image</param>
        /// <param name="maskImage">Mask</param>
        /// <param name="maskImageHeight">Height of mask</param>
        /// <param name="maskImageWidth">Width of mask</param>
        /// <param name="targetImage">Image for injection</param>
        /// <param name="targetImageHeight">Height of target image</param>
        /// <param name="targetImageWidth">Width of target image</param>
        /// <returns>Blended image</returns>
        public byte[] JacondiseMe(byte[] sourceImage, int sourceImageHeight, int sourceImageWidth,
                                  byte[] maskImage, int maskImageHeight, int maskImageWidth,
                                  byte[] targetImage, int targetImageHeight, int targetImageWidth)
        {
            //Convetation of one-dimentional arrays to two
            byte[,] sourceImageMatrix = ArrayProcessing.ConvertOneDimArrayToTwoDim(sourceImage, sourceImageHeight, sourceImageWidth);
            byte[,] maskImageMatrix = ArrayProcessing.ConvertOneDimArrayToTwoDim(maskImage, maskImageHeight, maskImageWidth);
            byte[,] targetImageMatrix = ArrayProcessing.ConvertOneDimArrayToTwoDim(targetImage, targetImageHeight, targetImageWidth);
            
            //Binarisation of mask
            byte[,] maskImageBinaryMatrix = Preprocessing.ApplyBinary(maskImageMatrix);
            
            //Takin one color chanel 
            byte[,] maskChanel = GetOneColorFromMask(maskImageBinaryMatrix, maskImageMatrix.GetLength(0), maskImageMatrix.GetLength(1));

            //takin color chanels from source and target images
            byte[,] blueSource, greenSource, redSource;
            byte[,] blueTarget, greenTarget, redTarget;
            DivideMatrixInColors(sourceImageMatrix, out blueSource, out greenSource,out redSource, sourceImageHeight, sourceImageWidth);
            DivideMatrixInColors(targetImageMatrix, out blueTarget, out greenTarget, out redTarget, targetImageHeight, targetImageWidth);

            //amount of layers in pyramids
            int layersAmount = 7;
            
            //Performing laplacians for source image
            byte[][,] blueSourcePy = Pyramid.Laplacian(blueSource, layersAmount);
            byte[][,] greenSourcePy = Pyramid.Laplacian(greenSource, layersAmount);
            byte[][,] redSourcePy = Pyramid.Laplacian(redSource, layersAmount);

            //performing laplacians for tarheg images
            byte[][,] blueTargetPy = Pyramid.Laplacian(blueTarget, layersAmount);
            byte[][,] greenTargetPy = Pyramid.Laplacian(greenTarget, layersAmount);
            byte[][,] redTargetPy = Pyramid.Laplacian(redTarget, layersAmount);

            //and gaussian for mask
            byte[][,] maskGausian = Pyramid.Gaussian(maskChanel, maskChanel.GetLength(0), maskChanel.GetLength(1), layersAmount);

            //Here i count output pyramids
            byte[][,] blueResultPy = Pyramid.CountResultPyramid(blueSourcePy, blueTargetPy, maskGausian);
            byte[][,] greenResultPy = Pyramid.CountResultPyramid(greenSourcePy, greenTargetPy, maskGausian);
            byte[][,] redResultPy = Pyramid.CountResultPyramid(redSourcePy, redTargetPy, maskGausian);

            //collapsing output pyramids
            byte[,] blueResult = Pyramid.CollapseLaplacian(blueResultPy);
            byte[,] greenResult = Pyramid.CollapseLaplacian(greenResultPy);
            byte[,] redResult = Pyramid.CollapseLaplacian(redResultPy);

            //pasting all colors 2getha
            byte[,] image = SplitColorsInImage(blueResult, greenResult, redResult, blueResult.GetLength(0), blueResult.GetLength(1));
            
            //converting it back to 1 dim
            byte[] output = ArrayProcessing.ConvertTwoDimArrayInOneDim(image, image.GetLength(0), image.GetLength(1));

            //just kostil
            output = Brightness.SetBrightness(output, -22);

            //printing to the screen
            return output;
        }
        
        /// <summary>
        /// This method divides ImageMatrix in color-filtered arrays
        /// </summary>
        /// <param name="matrix">Matrix - donor</param>
        /// <param name="blueArray">Here will be blue values</param>
        /// <param name="greenArray">Here will be green values</param>
        /// <param name="redArray">Here will be red values</param>
        public static void DivideMatrixInColors(byte[,] matrix, out byte[,] blueArray, out byte[,] greenArray, out byte[,] redArray, int h, int w)
        {
            blueArray = new byte[h, w];
            greenArray = new byte[h, w];
            redArray = new byte[h, w];
            int i1 = 0, j1 = 0;
            for (int i = 0; i < h; i++)
            {
                j1 = 0;
                for (int j = 0; j < w*4; j += 4)
                {
                    blueArray[i1, j1] = matrix[i, j];
                    greenArray[i1, j1] = matrix[i, j + 1];
                    redArray[i1, j1] = matrix[i, j + 2];
                    j1++;
                }//end of for
                i1++;
            }//end of for
        }//end of DivideMatrixInColors

       
        /// <summary>
        /// checks pixel for appropriate value
        /// </summary>
        /// <param name="n">value for check up</param>
        /// <returns>checked value</returns>
        public static byte CheckPixel(int n)
        {
            if (n < 0)
            {
                return 0;
            }
            else if (n > 255)
            {
                return 255;
            }
            else
            {
                return (byte)n;
            }
        }

        /// <summary>
        /// Splits 3 b g r a arrays in one image
        /// </summary>
        /// <param name="blue">blue color array</param>
        /// <param name="green">green color array</param>
        /// <param name="red">red color array</param>
        /// <param name="height">height of image</param>
        /// <param name="width">width of image</param>
        /// <returns>splitted image</returns>
        public static byte[,] SplitColorsInImage(byte[,] blue, byte[,] green, byte[,] red, int height, int width)
        {
            byte[,] result = new byte[height, width * 4];
            int i1 = 0, j1 = 0;
            for (int i = 0; i < height; i++)
            {
                j1 = 0;
                for (int j = 0; j < width * 4; j+=4)
                {
                    result[i, j] = blue[i1, j1];
                    result[i, j + 1] = green[i1, j1];
                    result[i, j + 2] = red[i1, j1];
                    j1++;
                }//end of for j
                i1++;
            }//end of for i
            return result;
        }//end of splitter

        /// <summary>
        /// Gets one color from mask for looping
        /// </summary>
        /// <param name="mask">Mask actually</param>
        /// <param name="height">Height of image</param>
        /// <param name="width">Width of image</param>
        /// <returns>Extracted color</returns>
        public static byte[,] GetOneColorFromMask(byte[,] mask, int height, int width)
        {
            byte[,] color = new byte[height, width/4];
            int i1 = 0, j1 = 0;
            for (int i = 0; i < height; i++)
            {
                j1 = 0;
                for (int j = 0; j < width; j+=4)
                {
                    color[i1, j1++] = mask[i, j];
                }
                i1++;
            }
            return color;
        }
    }//end of ImageProcessing
}//end of namespace
