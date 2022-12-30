using MP3_Final.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace MP3_Final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer media = new MediaPlayer();
        string fileName = string.Empty, path = string.Empty;
        List<Song> songs = new List<Song>();
        int i = 0;// bien toan cuc chi vi tri bai hat trong playlist
        string playlist1 = @"Playlist1.txt", playlist2 = @"Playlist2.txt", playlist3 = @"Playlist3.txt";
        string fav = @"Favorite.txt";

        DispatcherTimer timer;

        public class Song
        {
            public bool favor = false;
            public string path;
            public Song(string x, bool foo)
            {
                path = x;
                favor = foo;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            if (!System.IO.File.Exists(fav))
            {
                System.IO.File.Create(fav);
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            //media.MediaOpened += Media_MediaOpened;
            media.MediaEnded += Media_MediaEnded;
            media.MediaEnded += Media_Ended;// them event chay bai tiep theo
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
        private void Coverload()
        {
            TagLib.File file = TagLib.File.Create(songs[i].path);
            var firstp = file.Tag.Pictures.FirstOrDefault();
            if (firstp != null)
            {
                MemoryStream memoryStream = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                memoryStream.Seek(0, SeekOrigin.Begin);
                if (file.Tag.Pictures != null && file.Tag.Pictures.Length != 0)
                {
                    //memoryStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = memoryStream;
                    //memoryStream.Dispose();
                    bitmap.EndInit();

                    img.ImageSource = bitmap;//load hinh
                    staticImage.ImageSource = bitmap; // load hinh len anh nho 
                }
            }
            string title = "Tên bài hát:" + file.Tag.Title, album = "Album: " + file.Tag.Album, date = "Năm ra mắt: " + ((file.Tag.Year == 0) ? "" : file.Tag.Year.ToString()),
                kbit = "Bitrate: " + file.Properties.AudioBitrate.ToString() + "kbps";
            string[] artist = file.Tag.Performers, gerne = file.Tag.Genres;
            // infortexblock at sliderbar section
            titleTxtBlock.Text = file.Tag.Title;
            singerTxtBlock.Text = "";
            for (int i = 0; i < artist.Count(); i++)
            {
                singerTxtBlock.Text += artist[i];
                if (i > 0 && i < artist.Count() - 1) infotextblock.Text += ",";
            }
            // infortextblock at left section 
            infotextblock.Text = title + "\n" + album + "\nCa sĩ: ";
            for (int i = 0; i < artist.Count(); i++)
            {
                infotextblock.Text += artist[i];
                if (i > 0 && i < artist.Count() - 1) infotextblock.Text += ",";
            }
            infotextblock.Text += "\nThể loại: ";
            for (int i = 0; i < gerne.Count(); i++)
            {
                infotextblock.Text += gerne[i];
                if (i > 0 && i < gerne.Count() - 1) infotextblock.Text += ",";
            }
            infotextblock.Text += "\n" + kbit;
            string lyric = "Lyrics" + file.Tag.Lyrics;
            infotextblock.Text += "\n" + lyric;
        }
        private void Media_MediaOpened(object? sender, EventArgs e)
        {
            tbEnd.Text = string.Format("{0}", media.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            TimeSpan ts = media.NaturalDuration.TimeSpan;
            slider_seek.Maximum = ts.TotalSeconds;
            timer.Start();
            pausebtn.Content = pausebtn.FindResource("Pause");
            Coverload();
            Storyboard s = (Storyboard)pausebtn.FindResource("spinellipse");
            s.Begin();
            if (songs[i].favor)
            {
                heartbtn.Foreground = Brushes.DeepPink;
            }
            else heartbtn.Foreground = Brushes.White;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            slider_seek.Value = media.Position.TotalSeconds;
            tbStart.Text = string.Format("{0}", media.Position.ToString(@"mm\:ss"));
        }
        private void heartbtn_Click(object sender, RoutedEventArgs e)
        {
            heartbtn.Foreground = (heartbtn.Foreground != Brushes.DeepPink) ? Brushes.DeepPink : Brushes.White;
            if (!songs[i].favor)
            {
                System.IO.File.AppendAllText(fav, songs[i].path + "\n");
                songs[i].favor = true;
            }
            else
            {
                songs[i].favor = false;
                System.IO.File.WriteAllLines(fav, System.IO.File.ReadLines(fav).Where(l => l != songs[i].path).ToList());
            }
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
        // add songname to playlist table
        private void Add_UcSongName(Song song, int index)
        {
            SongName uc = new SongName();
            songMenu.Children.Add(uc);
            uc.Path = song.path;
            TagLib.File file = TagLib.File.Create(song.path);
            uc.Number = songMenu.Children.IndexOf(uc).ToString();
            uc.Title = file.Tag.Title;
            string[] artist = file.Tag.Performers;
            string listSinger = "";
            for (int i = 0; i < artist.Count(); i++)
            {
                listSinger += artist[i];
                if (i > 0 && i < artist.Count() - 1) listSinger += ",";
            }
            uc.Singer = listSinger;
            TimeSpan t = new TimeSpan();
            t = file.Properties.Duration;
            uc.Time = string.Format("{0}", t.ToString(@"mm\:ss"));
            uc.IndexOfSong = songMenu.Children.IndexOf(uc);
            uc.MLeftBtnD_BdSongName += Uc_MLeftBtnD_BdSongName;
            uc.DeleteClick += new Action<object>(OnDelete);
        }

        // Xử lý button delete ở popup
        private void OnDelete(object sender)
        {
            SongName uc = (SongName)sender;
            songMenu.Children.Remove(uc);
            media.Stop();
            Storyboard s = (Storyboard)pausebtn.FindResource("stopellipse");
            s.Begin();
            infotextblock.Text = "";
            songs.RemoveAt(uc.IndexOfSong);
            for (int i = 0; i < songMenu.Children.Count; i++)
            {
                SongName songname = (SongName)songMenu.Children[i];
                songname.Number = i.ToString();
                for (int j = 0; j < songs.Count; j++)
                {
                    if (songname.Path == songs[j].path)
                    {
                        songname.IndexOfSong = j; 
                        break;
                    }
                }
            }
        }

        // MouseLeftButtonDown Event of usercontrol songname
        private void Uc_MLeftBtnD_BdSongName(object sender, RoutedEventArgs e)
        {
            SongName uc = (SongName)sender;
            // gán index của bài hát cho biến toàn cục vị trí bài hát i 
            try
            {
                uc.IsActive = (uc.IsActive == false) ? true : false;
                for (int i = 0; i < songMenu.Children.Count; i++)
                {
                    SongName remainUc = (SongName)songMenu.Children[i];
                    if (remainUc != uc)
                        remainUc.IsActive = false;
                }
                if (uc.IsActive == true)
                {
                    i = uc.IndexOfSong;
                    fileName = songs[i].path;
                    media.Open(new Uri(fileName));
                    Coverload();
                    media.Play();
                    media.MediaOpened += Media_MediaOpened;
                    pausebtn.Content = pausebtn.FindResource("Pause");
                    Storyboard s = (Storyboard)pausebtn.FindResource("spinellipse");
                    s.Begin();

                }     
                else
                {
                    media.Pause();
                    pausebtn.Content = pausebtn.FindResource("Play");
                    Storyboard s = (Storyboard)pausebtn.FindResource("stopellipse");
                    s.Begin();    
                }
            }
            catch { }
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
            string line;
            bool heart = false;
            StreamReader reader = new StreamReader(fav);
            while ((line = reader.ReadLine()) != null)
            {
                if (line == dialog.FileName)
                {
                    heart = true;
                    break;
                }
            }
            reader.Close();
            Song song = new Song(dialog.FileName, heart);
            songs.Add(song);
            i = songs.Count - 1;
            //fileName = dialog.FileName;
            //Coverload();
            //code duoi la chay nhac    
            //media.Open(new Uri(fileName));
            Add_UcSongName(songs[i], i);
            //media.Play();
        }

        private void FolderUpload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.MyDocuments;//
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel) return;// thoat va khong thuc hien doan code duoi
                path = dialog.SelectedPath;
                /*FileInfo[] file = new DirectoryInfo(path).GetFiles("*.mp3");*/
                var fileInfos = new DirectoryInfo(path).GetFilesByExtentions(".wav", ".flac", ".aac", ".wma", ".wmv", ".avi", ".mpg", ".mpeg", ".m1v", ".mp2", ".mp3", ".mpa", ".mpe", ".m3u", ".mp4", ".mov", ".3g2", ".3gp2", ".3gp", ".3gpp", ".m4a", ".cda", ".aif", ".aifc", ".aiff", ".mid", ".midi", ".rmi", ".mkv", ".WAV", ".AAC", ".WMA", ".WMV", ".AVI", ".MPG", ".MPEG", ".M1V", ".MP2", ".MP3", ".MPA", ".MPE", ".M3U", ".MP4", ".MOV", ".3G2", ".3GP2", ".3GP", ".3GPP", ".M4A", ".CDA", ".AIF", ".AIFC", ".AIFF", ".MID", ".MIDI", ".RMI", ".MKV");
                
                string line;
                bool heart = false;
                foreach (FileInfo fil in fileInfos)
                {
                    StreamReader reader = new StreamReader(fav);
                    while ((line = reader.ReadLine()) != null)
                    {
                        if(line == fil.FullName)
                        {
                            heart = true;
                            break;
                        }
                    }
                    reader.Close();
                    Song song = new Song(fil.FullName, heart);
                    heart = false;
                    songs.Add(song);
                    int index = songs.Count - 1;
                    Add_UcSongName(songs[index], index);
                }
            }
            //fileName = songs[i].path;
            //media.Open(new Uri(fileName));
            //media.Play();
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            if (i < songs.Count - 1)
            {
                ++i;
                media.Stop();
                media.Open(new Uri(songs[i].path));
                media.Position = TimeSpan.Zero;// chay nhac tu 00:00
                Coverload();
                media.Play();
            }                
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
            media.Open(new Uri(songs[i].path));
            media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            media.Play();
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            StreamReader reader = new StreamReader(fav);
            i = 0;
            songs = new List<Song>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Song song = new Song(line, true);
                songs.Add(song);
            }
            reader.Close();
            if (songs.Count == 0) { return; }
            if (pausebtn.Content == pausebtn.FindResource("Play"))
            {
                pausebtn.Content = pausebtn.FindResource("Pause");
                Storyboard s = (Storyboard)pausebtn.FindResource("spinellipse");
                s.Begin();
                media.Play();
            }
            //fileName = songs[i].path;
            //media.Open(new Uri(fileName));
            //media.Play();
        }

        private UserControl1 activeUI = null;
        private void CreateAlbumClick(object sender, RoutedEventArgs e)
        {
            if (activeUI != null) Music_Player.Children.Remove(activeUI);
            UserControl1 a = new UserControl1();
            activeUI = a;
            a.Close += new Action<object>(OnClose);
            Grid.SetColumn(a, 1);

            Grid.SetColumnSpan(a, 2);
            
            Music_Player.Children.Add(a);
            //Grid.SetRow(a, 0);
            //Grid.SetRowSpan(a, 4);
        }

        private void OnClose(object sender)
        {
            Music_Player.Children.Remove((UserControl1)sender);
        }
        private void previousbtn_Click(object sender, RoutedEventArgs e)
        {
            if (i > 0)
                i--;
            else return;
            media.Stop();
            media.Open(new Uri(songs[i].path));
            media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            media.Play();
        }

        private void shufflebtn_Click(object sender, RoutedEventArgs e)
        {
            i = 0;
            songs.Shuffle();
            for (int i = 0; i < songMenu.Children.Count; i++)
            {
                SongName songname = (SongName)songMenu.Children[i];
                for (int j = 0; j < songs.Count; j++)
                {
                    if (songname.Path == songs[j].path)
                    {
                        songname.IndexOfSong = j;
                        break;
                    }   
                }
            }
            //media.Stop();
            //media.Open(new Uri(songs[i].path));
            //media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            //media.Play();
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
