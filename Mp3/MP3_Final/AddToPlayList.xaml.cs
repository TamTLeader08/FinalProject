using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddToPlayList.xaml
    /// </summary>
    public partial class AddToPlayList : Window
    {
        public string playListPath = null;

        public AddToPlayList()
        {
            InitializeComponent();
            Load();
        }

        void Load()
        {
            string[] files = Directory.GetFiles(MainWindow.head);
            listPlayList.Children.Clear();
            foreach (var file in files)
            {
                if (System.IO.Path.GetFileName(file) != "Favorite.txt")
                {
                    System.Windows.Controls.Button button = new System.Windows.Controls.Button();
                    button.Style = System.Windows.Application.Current.TryFindResource("albumButton") as Style;
                    button.Content = MainWindow.GetFileNameOnly(System.IO.Path.GetFileName(file));
                    button.Click += PlayListClick;
                    listPlayList.Children.Add(button);
                }
            }
        }

        private void PlayListClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            playListPath = MainWindow.ButtonToPath(button.Content.ToString());
            this.Close();
        }
    }
}
