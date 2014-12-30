using System;


namespace ComputerVisionLibrary
{
    /// <summary>
    /// This class contains the collection of kernels
    /// which can be applied on images
    /// </summary>
    public class KernelCollection
    {
        //gaussian blur
        private static float[,] gaussianBlur = {
                                 {1/16f, 1/8f, 1/16f},
                                 {1/8f, 1/4f, 1/8f},
                                 {1/16f, 1/8f, 1/16f}
                              };
        /// <summary>
        /// Returns Gaussian blur Kernel;
        /// </summary>
        public static float[,] GaussianBlur
        {
            get
            {
                return gaussianBlur;
            }
        }

        private static float[,] smoothFilter = {
                                                   /*
                                                   {1/25f,1/25f,1/25f,1/25f,1/25f},
                                                   {1/25f,1/25f,1/25f,1/25f,1/25f},
                                                   {1/25f,1/25f,1/25f,1/25f,1/25f},
                                                   {1/25f,1/25f,1/25f,1/25f,1/25f},
                                                   {1/25f,1/25f,1/25f,1/25f,1/25f}
                                                    */
                                                   {1/32f, 1/16f, 1/32f},
                                                   {1/16f, 1/8f, 1/16f},
                                                   {1/32f, 1/16f, 1/32f}
                                               };
        public static float[,] SmoothFilter
        {
            get
            {
                return smoothFilter;
            }
        }
        
    }
}
