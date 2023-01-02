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
using System.Windows.Shapes;

namespace MP3_Final
{
    /// <summary>
    /// Interaction logic for ChangeAlbumName.xaml
    /// </summary>
    public partial class ChangeAlbumName : Window
    {
        public string name = string.Empty;

        public ChangeAlbumName(string name)
        {
            InitializeComponent();
            this.name = name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            name = tb.Text;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
