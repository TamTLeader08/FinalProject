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
        private void heartbtn_Click(object sender, RoutedEventArgs e)
        {
            heartbtn.Foreground = (heartbtn.Foreground != Brushes.DeepPink) ? Brushes.DeepPink : Brushes.White;
        }

        private void pausebtn_Click(object sender, RoutedEventArgs e)
        {
            if (pausebtn.Content == pausebtn.FindResource("Pause"))
            {
                pausebtn.Content = pausebtn.FindResource("Play");
                Storyboard s = (Storyboard)pausebtn.FindResource("stopellipse");
                s.Begin();
                //mediaElement1.Pause();
            }
            else
            {
                pausebtn.Content = pausebtn.FindResource("Pause");
                Storyboard s = (Storyboard)pausebtn.FindResource("spinellipse");
                s.Begin();
                //mediaElement1.Play();
            }
        }

        private void darkmodeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (darkmodeBtn.Content == darkmodeBtn.FindResource("Light"))
            {
                darkmodeBtn.Content = darkmodeBtn.FindResource("Dark");
            }
            else
            {
                darkmodeBtn.Content = darkmodeBtn.FindResource("Light");
            }
        }
    }
    public static class Linqhelper /* class extention */
    {
        /*
         * Muc dich: them 1 ham de get file extention. Dung cho load folder
         */
        public static IEnumerable<FileInfo> GetFilesByExtentions(this DirectoryInfo dir, params string[] exts) /* https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/params */
        {
            if (exts == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            return files.Where(f => exts.Contains(f.Extension));/*=> la 1 lambda expressions. Xem them tai https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions */
        }
    }
}
