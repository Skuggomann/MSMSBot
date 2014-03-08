using AForge.Imaging;
using MSMSBot.Classes.Window_Interaction;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace MSMSBot
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Image img = ScreenReader.CaptureWindow();
            

            Bitmap test = new Bitmap(img);
            Bitmap t1 = Properties.Resources.TestImage;// ScreenGateway.UnClicked;//new Bitmap("t1.png");



            Bitmap t2 = test;

            // create filter
            AForge.Imaging.Filters.Grayscale filter = new AForge.Imaging.Filters.GrayscaleBT709();
            //AForge.Imaging.Filters.Grayscale filter = new AForge.Imaging.Filters.Grayscale(1.0, 1.0, 1.0);
            //AForge.Imaging.Filters.Median filter = new AForge.Imaging.Filters.Median();

            // apply filter
            System.Drawing.Bitmap newImage1 = filter.Apply(test);
            System.Drawing.Bitmap newImage2 = filter.Apply(test);



            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching();// (1.0f);
            //TemplateMatch[] matchings = tm.ProcessImage(newImage1, newImage2, new System.Drawing.Rectangle(220,100,200,200));
            //DebugText.Text = matchings[0].Similarity.ToString();



            ImageDisplay.Source = ScreenReader.GetImageStream(t1);
        }
    }
}
