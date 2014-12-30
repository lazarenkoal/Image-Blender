using System;
namespace ComputerVisionLibrary
{
    /// <summary>
    /// This class contains methods which
    /// are used to apply kernels
    /// </summary>
    public class LinearFilter
    {
        /// <summary>
        /// Method for applying kernel
        /// (Linear filteer)
        /// It is now suitable only for 1 chanel
        /// </summary>
        /// <param name="originalImage">Image to be filtered</param>
        /// <param name="coef">how many times to repeat transformation</param>
        /// <param name="height">Heigth of image</param>
        /// <param name="width">Width of image</param>
        /// <param name="kernel">kernel itself</param>
        /// <returns>filtered image</returns>
        public static byte[,] ApplyFilterTwoD(byte[,] originalImage, int height, int width, float[,] kernel)
        {
            //kernel parameters 
            int kernelWidth = kernel.GetUpperBound(0);
            int kernelLength = kernel.GetUpperBound(1);
            byte[,] blurImage = new byte[height, width];
            Array.Copy(originalImage, blurImage, originalImage.Length);
            byte[,] blurImage2 = new byte[height, width];
            
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        float pixelValue = 0f;
                        for (int l = 0; l <= kernelLength; l++)
                        {
                            for (int k = 0; k <= kernelWidth; k++)
                            {
                                //int i1 = i - 2*l + 1; 
                                //int j1 = j - 2*k + 1;
                                int i1 = i - 2*l + 3;
                                int j1 = j - 2*k + 3;
                                if ((j1 < 0) || (i1 < 0) || (j1 >= width) || (i1 >= height))
                                {
                                    continue;
                                }
                                else
                                {
                                    pixelValue += kernel[l, k] * blurImage[i1, j1];
                                }
                            }
                        }
                        blurImage2[i, j] = (byte)(PixelProcessing.CheckPixelValue((int)pixelValue));
                    }
                }
            return blurImage2;
        }

        /// <summary>
        /// This method apply kernel on matrix
        /// for 1d array.
        /// </summary>
        /// <param name="originalImage">source image</param>
        /// <param name="coef">amount of applyings</param>
        /// <param name="height">height of picture</param>
        /// <param name="width">width of picture</param>
        /// <param name="kernel">filter</param>
        /// <returns>filtered image</returns>
        public static byte[] ApplyKernelOnOneD(byte[] originalImage, int coef, int height, int width, float[,] kernel)
        {
            int kernelWidth = kernel.GetUpperBound(0);
            int kernelHeight = kernel.GetUpperBound(1);
            //First converted array
            byte[] blurImage = new byte[width * 4 * height];
            //this for can be removed!!!!!!
            //it is only for better bluring
            for (int w = 0; w < coef; w++)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < (height); j++)
                    {
                        float red = 0f, green = 0f, blue = 0f, capacity = 0f;
                        for (int k = 0; k < kernelWidth; k++)
                        {
                            for (int l = 0; l < kernelHeight; l++)
                            {
                                //вычисление позиции в матрице.
                                int pixelPlaceX = i + k - (kernelWidth / 2);
                                int pixelPlaceY = j + l - (kernelHeight / 2);
                                // проверка нормальности расчтанного пискеля
                                if ((pixelPlaceX < 0) || (pixelPlaceX >= width) || (pixelPlaceY < 0) || (pixelPlaceY >= height))
                                {
                                    continue;
                                }
                                else
                                {
                                    //вычисляем значения каждого цвета
                                    byte b = originalImage[4 * (width * pixelPlaceY + pixelPlaceX) + 0];
                                    byte g = originalImage[4 * (width * pixelPlaceY + pixelPlaceX) + 1];
                                    byte r = originalImage[4 * (width * pixelPlaceY + pixelPlaceX) + 2];

                                    // получаем значение кернела
                                    float kernelValue = kernel[k, l];

                                    // рассчитываем здесь значения пикселей после кернелирирования
                                    red += r * kernelValue;
                                    green += g * kernelValue;
                                    blue += b * kernelValue; 
                                }//else
                            }//for l
                        }//for k
                        //засовываем значения в байт
                        byte redValue = PixelProcessing.CheckPixelValue((int)red);
                        byte greenValue = PixelProcessing.CheckPixelValue((int)green);
                        byte blueValue = PixelProcessing.CheckPixelValue((int)blue);
                        //присваиваем их
                        blurImage[4 * (width * j + i) + 0] = blueValue;
                        blurImage[4 * (width * j + i) + 1] = greenValue;
                        blurImage[4 * (width * j + i) + 2] = redValue;
                    }//for j
                }//for i
                originalImage = blurImage;
            }//for w
            return blurImage;
        }

        public static byte[] ApplyKernelOnOneD2(byte[] originalImage, int height, int width, float[,] kernel)
        {
            int kernelWidth = kernel.GetUpperBound(0);
            int kernelHeight = kernel.GetUpperBound(1);
            //First converted array
            byte[] blurImage = new byte[width * height];
            

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < (height); j++)
                    {
                        float blue = 0f;
                        for (int k = 0; k <= kernelWidth; k++)
                        {
                            for (int l = 0; l <= kernelHeight; l++)
                            {
                                //вычисление позиции в матрице.
                                int pixelPlaceX = i + k - (kernelWidth / 2);
                                int pixelPlaceY = j + l - (kernelHeight / 2);
                                // проверка нормальности расчтанного пискеля
                                if ((pixelPlaceX < 0) || (pixelPlaceX >= width) || (pixelPlaceY < 0) || (pixelPlaceY >= height))
                                {
                                    continue;
                                }
                                else
                                {
                                    //вычисляем значения каждого цвета
                                    byte b = originalImage[(width * pixelPlaceY + pixelPlaceX) + 0];
                                    // получаем значение кернела
                                    float kernelValue = kernel[k, l];
                                    // рассчитываем здесь значения пикселей после кернелирирования
                                    blue += b * kernelValue;
                                }//else
                            }//for l
                        }//for k
                        //засовываем значения в байт
                        byte blueValue = PixelProcessing.CheckPixelValue((int)blue);
                        //присваиваем их
                        blurImage[(width * j + i) + 0] = blueValue;
                    }//for j
              
            }
            return blurImage;
        }
    }
}
