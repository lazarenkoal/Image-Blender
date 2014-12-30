using System;
namespace ComputerVisionLibrary
{
    /// <summary>
    /// This class contains methods
    /// for pixel processing
    /// </summary>
    public class PixelProcessing
    {
        /// <summary>
        /// Method checks if the pixel's 
        /// value is in the correct interval [0, 255] 
        /// </summary>
        /// <param name="pixel">The value of pixel</param>
        /// <returns>Pixel's checked value</returns>
        public static byte CheckPixelValue(int pixel)
        {
            int pixel1;
            if (pixel > 255)
            { 
                pixel1 = 255; 
            }
            else 
                if (pixel < 0) 
                { 
                    pixel1 = 0; 
                }
            else 
                { 
                    pixel1 = pixel; 
                }
            return (byte)pixel1;
        }//checkPixelValue()
    }
}
