using MSMSBot.Classes.AI;
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
            System.Drawing.Image img = ScreenReader.CaptureBoard();  
            Bitmap test = new Bitmap(img);

            // create filter
            AForge.Imaging.Filters.Grayscale filter = new AForge.Imaging.Filters.GrayscaleBT709();
            // apply filter
            System.Drawing.Bitmap newImage1 = filter.Apply(test);

            ImageDisplay.Source = ScreenReader.GetImageStream(newImage1);

            //ScreenGateway.ClickSquerep(1, 8);

            // Show char array showing layout in debug field:
            DebugText.Text = ScreenGateway.BoardToString(ScreenGateway.GetBoardLayout());
        }

        private void btnCSPClick(object sender, RoutedEventArgs e)
        {
            AI test = new AI();
            test.nextMove();
        }
    }
}
