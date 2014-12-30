using System;
namespace ComputerVisionLibrary
{
    /// <summary>
    /// This class contains methods for
    /// setting different color filteres
    /// </summary>
    public class ColorFilteres
    {
        /// <summary>
        /// Sets red filter for image
        /// </summary>
        /// <param name="originalImage">Image to be filtered</param>
        /// <returns>Filtered image</returns>
        public static byte[] SetRedFilter(byte[] originalImage)
        {
            // bgra
            byte[] redFilt = new byte[originalImage.Length];
            for (int i = 0; i < originalImage.Length; )
            {
                redFilt[i] = 0; //blue
                redFilt[i + 1] = 0; //green
                redFilt[i + 2] = originalImage[i + 2]; //red
                redFilt[i + 3] = originalImage[i + 3]; //capacity
                i += 4;
            }//for
            return redFilt;
        }//SetRedFilter

        /// <summary>
        /// Sets green filter for image
        /// </summary>
        /// <param name="originalImage">Image to be filtered</param>
        /// <returns>Filtered image</returns>
        public static byte[] SetGreenFilter(byte[] originalImage)
        {
            byte[] redFilt = new byte[originalImage.Length];
            //bgra format
            for (int i = 0; i < originalImage.Length;)
            {
                redFilt[i] = 0; //blue
                redFilt[i + 1] = originalImage[i + 1]; //green
                redFilt[i + 2] = 0; //red
                redFilt[i + 3] = originalImage[i + 3]; //capacity
                i += 4;
            }//for
            return redFilt;
        }//SetGreenFilter

        /// <summary>
        /// Sets blue filter for image
        /// </summary>
        /// <param name="originalImage">Image to be filtered</param>
        /// <returns>Filtered image</returns>
        public static byte[] SetBlueFilter(byte[] originalImage)
        {
            byte[] redFilt = new byte[originalImage.Length];
            //bgra format
            for (int i = 0; i < originalImage.Length; )
            {
                redFilt[i] = originalImage[i]; //blue
                redFilt[i + 1] = 0; //green
                redFilt[i + 2] = 0; //red
                redFilt[i + 3] = originalImage[i + 3]; //capacity
                i += 4;
            }//for
            return redFilt;
        }//SetBlueFilter
    }
}
