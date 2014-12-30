using System;

namespace ComputerVisionLibrary
{
    /// <summary>
    /// This class contains methods for 
    /// inverting the image
    /// </summary>
    public class Invert
    {
        /// <summary>
        /// Inverts image
        /// </summary>
        /// <param name="originalImage">Image to be inverted</param>
        /// <returns>Inverted image</returns>
        public static byte[] SetInvert(byte[] originalImage)
        {
            byte[] invert = new byte[originalImage.Length];
            for (int i = 0; i < originalImage.Length; )
            {
                invert[i] = (byte)(255 - originalImage[i]);
                invert[i + 1] = (byte)(255 - originalImage[i + 1]);
                invert[i + 2] = (byte)(255 - originalImage[i + 2]);
                invert[i + 3] = (byte)originalImage[i + 3];
                i = i + 4;
            }//for
            return invert;
        }//SetInvert
    }
}
