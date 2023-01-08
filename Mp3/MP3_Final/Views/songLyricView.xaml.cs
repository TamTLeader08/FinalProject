using MP3_Final.UserControls;
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

namespace MP3_Final.Views
{
    /// <summary>
    /// Interaction logic for songLyricView.xaml
    /// </summary>
    public partial class songLyricView : UserControl
    {
        public songLyricView()
        {
            InitializeComponent();
        }

        public string Lyric
        {
            get { return (string)GetValue(LyricProperty); }
            set { SetValue(LyricProperty, value); }
        }

        public static readonly DependencyProperty LyricProperty = DependencyProperty.Register
            ("Lyric", typeof(string), typeof(songLyricView));

        public BitmapImage PathImage
        {
            get { return (BitmapImage)GetValue(PathImageProperty); }
            set { SetValue(PathImageProperty, value); }
        }

        public static readonly DependencyProperty PathImageProperty = DependencyProperty.Register
            ("PathImage", typeof(BitmapImage), typeof(songLyricView));

        public event Action<object> Close;
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            if (Close != null)
            {
                Close(this);
            }
        }
    }
}
