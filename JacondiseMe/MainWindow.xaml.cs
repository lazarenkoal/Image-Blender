using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace JacondiseMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private BitmapImage originalImage;
        private BitmapImage maskImage;
        private BitmapImage targetImage;
        private byte[] originalImageBytes;
        private byte[] maskImageBytes;
        private byte[] targetImageBytes;

        /// <summary>
        /// Показывает картинку под первой кнопкой
        /// </summary>
        /// <param name="filename"></param>
        private void showImage(string filename)
        {
            originalImage = ImageConvertor.FilenameToImage(filename);
            originalImageBytes = ImageConvertor.ImageToByteArray(filename);
            originalPanel.Source = originalImage;
        }

        /// <summary>
        /// Показывает картинку под кнопкой загрузить маску
        /// и еще инициализирует массивчик
        /// </summary>
        /// <param name="filename"></param>
        private void showImage1(string filename)
        {
            maskImage = ImageConvertor.FilenameToImage(filename);
            maskImageBytes = ImageConvertor.ImageToByteArray(filename);
            maskPanel.Source = maskImage;
        }
        
        /// <summary>
        /// Показывает картинку под targetButton
        /// </summary>
        /// <param name="filename"></param>
        private void showImage2(string filename)
        {
            targetImage = ImageConvertor.FilenameToImage(filename);
            targetImageBytes = ImageConvertor.ImageToByteArray(filename);
            targetPanel.Source = targetImage;
        }

        private void uploadSourceImage_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg)|*.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                showImage(openFileDialog.FileName);
            }
        }

        private void uploadMaskImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg)|*.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                showImage1(openFileDialog.FileName);
            }
        }

        private void uploadTargetImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg)|*.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                showImage2(openFileDialog.FileName);
            }
        }

        private void jacondiseMe_Click(object sender, RoutedEventArgs e)
        {
            
                ImageProcessing process = new ImageProcessing();
                byte[] jacondisedImage;
                jacondisedImage = process.JacondiseMe(originalImageBytes, originalImage.PixelHeight, originalImage.PixelWidth,
                                                      maskImageBytes, maskImage.PixelHeight, maskImage.PixelWidth,
                                                      targetImageBytes, targetImage.PixelHeight, targetImage.PixelWidth);
                resultPanel.Source = ImageConvertor.ByteArrayToImage(jacondisedImage, targetImage.PixelWidth, targetImage.PixelHeight, 4);
            
       
            
        }
    }
}
