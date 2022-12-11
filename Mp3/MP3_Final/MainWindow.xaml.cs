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
using System.Windows.Threading;

namespace MP3_Final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer media = new MediaPlayer();
        string fileName = string.Empty, path = string.Empty;
        List<string> songs = new List<string>();
        int i = 0;// bien toan cuc chi vi tri bai hat trong playlist

        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            media.MediaOpened += Media_MediaOpened;
            media.MediaEnded += Media_MediaEnded;
        }

        

        bool repeatMedia = false;
        private void Media_MediaEnded(object? sender, EventArgs e)
        {
            if (repeatMedia)
            {
                i--;
                media.Position = TimeSpan.Zero;
                media.Play();
            }
            else
            {
                slider_seek.Value = 0;
                tbStart.Text = "00:00";
                media.Pause();                
                pausebtn.Content = pausebtn.FindResource("Play");
                Storyboard s = (Storyboard)pausebtn.FindResource("stopellipse");
                s.Begin();
            }
        }

        private void Media_MediaOpened(object? sender, EventArgs e)
        {
            tbEnd.Text = string.Format("{0}", media.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            TimeSpan ts = media.NaturalDuration.TimeSpan;
            slider_seek.Maximum = ts.TotalSeconds;
            timer.Start();
            pausebtn.Content = pausebtn.FindResource("Pause");
            Storyboard s = (Storyboard)pausebtn.FindResource("spinellipse");
            s.Begin();

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            slider_seek.Value = media.Position.TotalSeconds;
            tbStart.Text = string.Format("{0}", media.Position.ToString(@"mm\:ss"));
        }
        private void heartbtn_Click(object sender, RoutedEventArgs e)
        {
            heartbtn.Foreground = (heartbtn.Foreground != Brushes.DeepPink) ? Brushes.DeepPink : Brushes.White;
        }

        private void pausebtn_Click(object sender, RoutedEventArgs e)
        {
            if (media.Source != null)
            {
                if (pausebtn.Content == pausebtn.FindResource("Pause"))
                {
                    pausebtn.Content = pausebtn.FindResource("Play");
                    Storyboard s = (Storyboard)pausebtn.FindResource("stopellipse");
                    s.Begin();
                    media.Pause();
                }
                else
                {
                    pausebtn.Content = pausebtn.FindResource("Pause");
                    Storyboard s = (Storyboard)pausebtn.FindResource("spinellipse");
                    s.Begin();
                    media.Play();
                }
            } 
        }
        private void FileUpload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            // code open 1 file
            dialog.Multiselect = false;
            dialog.DefaultExt = ".mp3,.flac,.ogg,.wav"; // Default file extension
            dialog.Filter = "All Media Files|*.wav;*.flac;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV"; // Filter files by extension 

            // ket qua cua dialog
            if (dialog.ShowDialog() == false)
            {
                return;
            }
            songs.Add(dialog.FileName);
            i = songs.Count - 1;
            fileName = dialog.FileName;
            //code duoi la chay nhac    
            media.Open(new Uri(fileName));
            media.Play();
        }

        private void FolderUpload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            i = 0;
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.MyDocuments;//
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel) return;// thoat va khong thuc hien doan code duoi
                path = dialog.SelectedPath;
                /*FileInfo[] file = new DirectoryInfo(path).GetFiles("*.mp3");*/
                var fileInfos = new DirectoryInfo(path).GetFilesByExtentions(".wav", ".flac", ".aac", ".wma", ".wmv", ".avi", ".mpg", ".mpeg", ".m1v", ".mp2", ".mp3", ".mpa", ".mpe", ".m3u", ".mp4", ".mov", ".3g2", ".3gp2", ".3gp", ".3gpp", ".m4a", ".cda", ".aif", ".aifc", ".aiff", ".mid", ".midi", ".rmi", ".mkv", ".WAV", ".AAC", ".WMA", ".WMV", ".AVI", ".MPG", ".MPEG", ".M1V", ".MP2", ".MP3", ".MPA", ".MPE", ".M3U", ".MP4", ".MOV", ".3G2", ".3GP2", ".3GP", ".3GPP", ".M4A", ".CDA", ".AIF", ".AIFC", ".AIFF", ".MID", ".MIDI", ".RMI", ".MKV");
                foreach (FileInfo fil in fileInfos)
                {
                    songs.Add(fil.FullName);
                }
            }

            fileName = songs[i];
            media.Open(new Uri(fileName));
            media.MediaEnded += Media_Ended;// them event chay bai tiep theo
            media.Play();
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            if (i < songs.Count)
                ++i;
            media.Stop();
            media.Open(new Uri(songs[i]));
            media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            media.Play();
        }

        private void replaybtn_Click(object sender, RoutedEventArgs e)
        {
            replaybtn.Foreground = (replaybtn.Foreground != Brushes.DeepPink) ? Brushes.DeepPink : Brushes.White;
            repeatMedia = (repeatMedia == false && replaybtn.Foreground == Brushes.DeepPink) ? true : false;
        }

        private void slider_seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Position = TimeSpan.FromSeconds(slider_seek.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sldVolume.Value == 0)
            {
                sldVolume.Value = 1;
            }
            else
            {
                sldVolume.Value = 0;
            }
        }

        private void sldVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Volume = sldVolume.Value;
        }

        private void nextbtn_Click(object sender, RoutedEventArgs e)
        {
            if (i < songs.Count - 1)
                ++i;
            else return;
            media.Stop();
            media.Open(new Uri(songs[i]));
            media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            media.Play();
        }

        private void previousbtn_Click(object sender, RoutedEventArgs e)
        {
            if (i > 0)
                i--;
            else return;
            media.Stop();
            media.Open(new Uri(songs[i]));
            media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            media.Play();
        }

        private void shufflebtn_Click(object sender, RoutedEventArgs e)
        {
            i = 0;
            songs.Shuffle();
            media.Stop();
            media.Open(new Uri(songs[i]));
            media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            media.Play();
        }

        private void darkmodeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (darkmodeBtn.Content == darkmodeBtn.FindResource("Light"))
            {
                darkmodeBtn.Content = darkmodeBtn.FindResource("Dark");
                Music_Player.Background = Brushes.Black;
                searchBar.Background = (Brush)new BrushConverter().ConvertFrom("#3a3b3d");
                searchTB.Foreground = Brushes.White;
            }
            else
            {
                darkmodeBtn.Content = darkmodeBtn.FindResource("Light");
                Music_Player.Background = Brushes.White;
                searchBar.Background = Brushes.White;
                searchTB.Foreground = Brushes.Black;
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
        // make a shuffle funct for playlist
        private static Random rng = new Random();

        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
