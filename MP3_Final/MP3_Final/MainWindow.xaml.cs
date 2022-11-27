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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MP3_Final
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
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.Visibility = Visibility.Hidden;

            if(PauseButton.Visibility == Visibility.Hidden)
            {
                PauseButton.Visibility = Visibility.Visible;
            }
        }

        private void PauseButton_Click(object sender, MouseButtonEventArgs e)
        {
            PauseButton.Visibility = Visibility.Hidden;

            if (PlayButton.Visibility == Visibility.Hidden)
            {
                PlayButton.Visibility = Visibility.Visible;
            }
        }
    }
}
