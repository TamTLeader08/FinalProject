using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using MP3_Final.UserControls;
using MP3_Final.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
using TagLib;
using TagLib.Aac;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using MessageBox = System.Windows.MessageBox;

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
        // dùng cho shuffle
        List<Song> subSongs = new List<Song>(); 
        List<Song> search = new List<Song>();
        string current;
        int i = 0;// bien toan cuc chi vi tri bai hat trong playlist
        public static string tail = @".txt";
        public static string head = Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()) + @"\PlayList\";
        string fav = head + @"Favorite.txt";
        string history = head + @"History.txt";
        public static string currentPlaylist = string.Empty;

        DispatcherTimer timer;
        bool shuffleMedia = false;
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
            if (!System.IO.Directory.Exists(head))
            {
                System.IO.Directory.CreateDirectory(head);
            }
            if (!System.IO.File.Exists(fav))
            {
                using (System.IO.File.Create(fav)) ;
            }
            if (!System.IO.File.Exists(history))
            {
                using (System.IO.File.Create(history)) ;
            }

            LoadPlayList(head);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            media.MediaOpened += Media_MediaOpened;
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
            //TagLib.File file = TagLib.File.Create(songs[i].path);
            TagLib.File file;
            //if (!shuffleMedia)
            //    file = TagLib.File.Create(songs[i].path);
            //else
            //    file = TagLib.File.Create(subSongs[i].path);
            file = TagLib.File.Create(current);
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
            //string lyric = "Lyrics" + file.Tag.Lyrics;
            //infotextblock.Text += "\n" + lyric;
        }
        private void Media_MediaOpened(object? sender, EventArgs e)
        {
            ActiveSongName(i);
            tbEnd.Text = string.Format("{0}", media.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            TimeSpan ts = media.NaturalDuration.TimeSpan;
            slider_seek.Maximum = ts.TotalSeconds;
            timer.Start();
            pausebtn.Content = pausebtn.FindResource("Pause");
            Coverload();
            Storyboard s = (Storyboard)pausebtn.FindResource("spinellipse");
            s.Begin();
            //if (songs[i].favor)
            //{
            //    heartbtn.Foreground = Brushes.DeepPink;
            //}
            //else heartbtn.Foreground = Brushes.White;
            string[] files = System.IO.File.ReadAllLines(fav);
            foreach (string file in files)
            {
                if (current == file)
                {
                    heartbtn.Foreground = Brushes.DeepPink;
                    return;
                }
            }
            heartbtn.Foreground = Brushes.White;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            slider_seek.Value = media.Position.TotalSeconds;
            tbStart.Text = string.Format("{0}", media.Position.ToString(@"mm\:ss"));
        }
        private void heartbtn_Click(object sender, RoutedEventArgs e)
        {
            if (heartbtn.Foreground == Brushes.White)
            {
                System.IO.File.AppendAllText(fav, songs[i].path + "\n");
            }
            else System.IO.File.WriteAllLines(fav, System.IO.File.ReadLines(fav).Where(l => l != songs[i].path).ToList());
            heartbtn.Foreground = (heartbtn.Foreground != Brushes.DeepPink) ? Brushes.DeepPink : Brushes.White;
            //if (!songs[i].favor)
            //{
            //    System.IO.File.AppendAllText(fav, songs[i].path + "\n");
            //    songs[i].favor = true;
            //}
            //else
            //{
            //    songs[i].favor = false;
            //    System.IO.File.WriteAllLines(fav, System.IO.File.ReadLines(fav).Where(l => l != songs[i].path).ToList());
            //}
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
            int temp = songMenu.Children.IndexOf(uc) + 1;
            if (temp >= 1 && temp <= 9)
                uc.Number = '0' + temp.ToString();
            else
                uc.Number = temp.ToString();
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
            //Nếu xóa bài đang phát thì dừng bài đó
            if (i == uc.IndexOfSong)
            {
                media.Stop();
                Storyboard s = (Storyboard)pausebtn.FindResource("stopellipse");
                s.Begin();
                infotextblock.Text = "";
            }
            for (int i = 0; i < songs.Count; i++)
            {
                if (songs[i].path == uc.Path)
                {
                    songs.RemoveAt(i);
                    break;
                }

            }

            for (int i = 0; i < subSongs.Count; i++)
            {
                if (subSongs[i].path == uc.Path)
                {
                    subSongs.RemoveAt(i);
                    break;
                }

            }

            for (int i = 0; i < songMenu.Children.Count; i++)
            {
                SongName songname = (SongName)songMenu.Children[i];
                int temp = i + 1;
                if (temp >= 1 && temp <= 9)
                    songname.Number = '0' + temp.ToString();
                else
                    songname.Number = temp.ToString();
                if (!shuffleMedia)
                    for (int j = 0; j < songs.Count; j++)
                    {
                        if (songname.Path == songs[j].path)
                        {
                            songname.IndexOfSong = j;
                            break;
                        }
                    }
                else
                {
                    for (int j = 0; j < subSongs.Count; j++)
                    {
                        if (songname.Path == subSongs[j].path)
                        {
                            songname.IndexOfSong = j;
                            break;
                        }
                    }
                }

            }
        }

        // MouseLeftButtonDown Event of usercontrol songname
        private void Uc_MLeftBtnD_BdSongName(object sender, RoutedEventArgs e)
        {
            if (onSearch) playSearch = true;
            SongName uc = (SongName)sender;
            current = uc.Path;
            try
            {
                uc.IsActive = (uc.IsActive == false) ? true : false;
                if (uc.IsActive == true)
                {
                    // gán index của bài hát cho biến toàn cục vị trí bài hát i 
                    i = uc.IndexOfSong;
                    ActiveSongName(i);
                    //if (!shuffleMedia)
                    //    fileName = songs[i].path;
                    //else
                    //    fileName = subSongs[i].path;
                    fileName = uc.Path;
                    media.Open(new Uri(fileName));
                    media.Play();
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
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        //Change properties of ucSongName fit current playing song
        private void ActiveSongName(int IndexOfCurrentSong)
        {
            SongName CurrentSong = new SongName();
            for (int i = 0; i < songMenu.Children.Count; i++)
            {
                SongName sn = (SongName)songMenu.Children[i];

                if (sn.IndexOfSong == IndexOfCurrentSong)
                {
                    CurrentSong = sn;
                    CurrentSong.IsActive = true;
                }
            }
            for (int i = 0; i < songMenu.Children.Count; i++)
            {
                SongName remainUc = (SongName)songMenu.Children[i];
                if (remainUc.Path != CurrentSong.Path)
                    remainUc.IsActive = false;
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
            subSongs.Add(song);
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
                    subSongs.Add(song);
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
            i++;
            if (onSearch && playSearch)
            {
                if (i == search.Count) i = 0;
                current = search[i].path;
                media.Stop();
                media.Open(new Uri(search[i].path));
                media.Position = TimeSpan.Zero;// chay nhac tu 00:00
                media.Play();
                return;
            }
            if (i == songs.Count)
            {
                i = 0;
            }
            media.Stop();
            if (!shuffleMedia)
            {
                current = songs[i].path;
                media.Open(new Uri(songs[i].path));
            }
            else
            {
                current = subSongs[i].path;
                media.Open(new Uri(subSongs[i].path));
            }
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
            if (onSearch && playSearch)
            {
                if (i < search.Count - 1)
                    ++i;
                else i = 0;
                current = search[i].path;
                media.Stop();
                media.Open(new Uri(search[i].path));
                media.Position = TimeSpan.Zero;// chay nhac tu 00:00
                media.Play();
                return;
            }
            if (songs.Count == 0) { return; }
            if (i < songs.Count - 1)
                ++i;
            else i = 0;
            media.Stop();
            if (!shuffleMedia)
            {
                current = songs[i].path;
                media.Open(new Uri(songs[i].path));
            }
            else
            {
                current = subSongs[i].path;
                media.Open(new Uri(subSongs[i].path));
            }
            media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            media.Play();
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            onSearch = false;
            playSearch = false;
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
            songMenu.Children.Clear();
            for (int i = 0; i < songs.Count; i++)
            {
                Add_UcSongName(songs[i], i);
            }
            //if (pausebtn.Content == pausebtn.FindResource("Play"))
            //{
            //    pausebtn.Content = pausebtn.FindResource("Pause");
            //    Storyboard s = (Storyboard)pausebtn.FindResource("spinellipse");
            //    s.Begin();
            //    media.Play();
            //}
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

        private void PlayListClick(object sender, RoutedEventArgs e)
        {
            onSearch = false;
            playSearch = false;
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            string path = head + button.Content.ToString() + tail;
            currentPlaylist = button.Tag.ToString();
            OpenPlayList(path);
        }

        void OpenPlayList(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                using (System.IO.File.Create(path));
            }
            string[] contents = System.IO.File.ReadAllLines(path);
            string line;
            bool heart = false;
            songMenu.Children.Clear();
            songs = new List<Song>();
            subSongs = new List<Song>();

            foreach (var file in contents)
            {
                StreamReader reader = new StreamReader(fav);
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == file)
                    {
                        heart = true;
                        break;
                    }
                }
                reader.Close();
                Song song = new Song(file, heart);
                heart = false;
                songs.Add(song);
                subSongs.Add(song);
                int index = songs.Count - 1;
                Add_UcSongName(songs[index], index);
            }
        }

        private void AddPlayList(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = new System.Windows.Controls.Button();
            button.Style = System.Windows.Application.Current.TryFindResource("albumButton") as Style;
            button.Content = "Album mới";
            button.Tag = ButtonToPath(button.Content.ToString());
            button.Click += PlayListClick;
            button.MouseRightButtonDown += ChangePlayListName;
            string path = ButtonToPath(button.Content.ToString());
            if (System.IO.File.Exists(path))
            {
                System.Windows.MessageBox.Show("Album bị trùng tên!");
            }
            else
            {
                using (System.IO.File.Create(path)) ;
                listMenu.Children.Add(button);
            }    
        }

        void LoadPlayList(string path)
        {
            string[] files = Directory.GetFiles(path);
            listMenu.Children.Clear();
            foreach (var file in files)
            {
                if (System.IO.Path.GetFileName(file) != "Favorite.txt" && System.IO.Path.GetFileName(file) != "History.txt")
                {
                    System.Windows.Controls.Button button = new System.Windows.Controls.Button();
                    button.Style = System.Windows.Application.Current.TryFindResource("albumButton") as Style;
                    button.Content = GetFileNameOnly(System.IO.Path.GetFileName(file));
                    button.Click += PlayListClick;
                    button.MouseRightButtonDown += ChangePlayListName;
                    button.Tag = file;
                    listMenu.Children.Add(button);
                }
            }    
        }

        public static string GetFileNameOnly(string fullName)
        {
            string res = "";
            foreach (var i in fullName)
            {
                if (i == '.')
                {
                    return res;
                }
                res += i;
            }
            return res;
        }

        private void previousbtn_Click(object sender, RoutedEventArgs e)
        {
            if (onSearch && playSearch)
            {
                if (i < search.Count - 1)
                    ++i;
                else i = 0;
                media.Stop();
                media.Open(new Uri(search[i].path));
                media.Position = TimeSpan.Zero;// chay nhac tu 00:00
                media.Play();
                return;
            }
            if (songs.Count == 0) { return; }
            if (i > 0)
                i--;
            else i = songs.Count - 1;
            media.Stop();
            if (!shuffleMedia)
                media.Open(new Uri(songs[i].path));
            else
                media.Open(new Uri(subSongs[i].path));
            media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            media.Play();
        }

        private void shufflebtn_Click(object sender, RoutedEventArgs e)
        {
            shufflebtn.Foreground = (shufflebtn.Foreground != Brushes.DeepPink) ? Brushes.DeepPink : Brushes.White;
            shuffleMedia = (shuffleMedia == false && shufflebtn.Foreground == Brushes.DeepPink) ? true : false;
            i = 0; 

            if (shuffleMedia)
            {
                subSongs.Clear();
                foreach (var song in songs)
                {
                    subSongs.Add(song);
                }
                subSongs.Shuffle();
                //songMenu.Children.Clear();
                //for (int i = 0; i < subSongs.Count; i++)
                //{
                //    Add_UcSongName(subSongs[i], i);
                //}
                //songs.Shuffle();
                for (int i = 0; i < songMenu.Children.Count; i++)
                {
                    SongName songname = (SongName)songMenu.Children[i];
                    for (int j = 0; j < songs.Count; j++)
                    {
                        if (songname.Path == subSongs[j].path)
                        {
                            songname.IndexOfSong = j;
                            break;
                        }
                    }
                }
            }
            else
            {
                songMenu.Children.Clear();
                for (int i = 0; i < songs.Count; i++)
                {
                    Add_UcSongName(songs[i], i);
                }
                //string currentSongPath = songs[i].path;

                //for (int i = 0; i < songMenu.Children.Count; i++)
                //{
                //    SongName songname = (SongName)songMenu.Children[i];
                //    for (int j = 0; j < subSongs.Count; j++)
                //    {
                //        if (songname.Path == subSongs[j].path)
                //        {
                //            songname.IndexOfSong = j;
                //            break;
                //        }
                //    }
                //}
                //for (int n = 0; n < songMenu.Children.Count; n++)
                //{
                //    SongName songname = (SongName)songMenu.Children[n];
                //    if (songname.IsActive)
                //        i = songname.IndexOfSong;
                //}

                //media.Stop();
                //media.Open(new Uri(songs[i].path));
                //media.Position = TimeSpan.Zero;// chay nhac tu 00:00
                //media.Play();
            }
        }

        private void ChangePlayListName(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            string old = head + button.Content.ToString() + tail;
            ChangeAlbumName change = new ChangeAlbumName(button.Content.ToString());
            change.ShowDialog();
            button.Content = change.name;
            string newName = ButtonToPath(button.Content.ToString());
            button.Tag = newName;
            System.IO.File.Move(old, newName);
        }

        private void DeletePlayList(object sender, RoutedEventArgs e)
        {
            foreach (var playList in listMenu.Children)
            {
                System.Windows.Controls.Button button = playList as System.Windows.Controls.Button;
                if (button.Tag.ToString() == currentPlaylist)
                {
                    listMenu.Children.Remove(button);
                    System.IO.File.Delete(button.Tag.ToString());
                    return;
                }
            }
        }

        public static string ButtonToPath(string content)
        {
            return head + content + tail;
        }

        private void minimizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void maximizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;

        }

        private void closeBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        void AddList(string playlist, string path)
        {
            string[] files = System.IO.File.ReadAllLines(playlist);
            foreach (string file in files)
            {
                if (path == file)
                {
                    return;
                }
            }

            if (new FileInfo(playlist).Length != 0)
            {
                System.IO.File.AppendAllText(playlist, "\n");
            }

            System.IO.File.AppendAllText(playlist, path);
        }
        //real load file
        private void LoadFileButton(object sender, RoutedEventArgs e)
        {
            if (onSearch)
            {
                songMenu.Children.Clear();
                onSearch = false;
                playSearch = false;
            }
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
            AddList(history, dialog.FileName);
            foreach (var s in songs)
            {
                if (s.path == dialog.FileName) return;
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
            subSongs.Add(song);
            i = songs.Count - 1;
            //fileName = dialog.FileName;
            //Coverload();
            //code duoi la chay nhac    
            //media.Open(new Uri(fileName));
            Add_UcSongName(songs[i], i);
            //media.Play();
        }
        //real load folder
        private void LoadFolderButton(object sender, RoutedEventArgs e)
        {
            if (onSearch)
            {
                songMenu.Children.Clear();
                onSearch = false;
                playSearch = false;
            }
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
                    AddList(history, fil.FullName);
                    bool check = false;
                    foreach (var s in songs)
                    {
                        if (s.path == fil.FullName) { check = true; break; }
                    }
                    if (check) continue;
                    StreamReader reader = new StreamReader(fav);
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == fil.FullName)
                        {
                            heart = true;
                            break;
                        }
                    }
                    reader.Close();
                    Song song = new Song(fil.FullName, heart);
                    heart = false;
                    songs.Add(song);
                    subSongs.Add(song);
                    int index = songs.Count - 1;
                    Add_UcSongName(songs[index], index);
                }
            }
        }


        songLyricView activeSlv = null;
        private void DisplayLyric(object sender, RoutedEventArgs e)
        {
            songLyricView slviewUC = new songLyricView();
            TagLib.File file;
            //if (!shuffleMedia)
            //    file = TagLib.File.Create(songs[i].path);
            //else
            //    file = TagLib.File.Create(subSongs[i].path);
            file = TagLib.File.Create(current);
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

                    slviewUC.PathImage = bitmap;
                }
            }
            slviewUC.Lyric = file.Tag.Lyrics;  
            
            if (activeSlv != null) centerUI.Children.Remove(activeSlv);
            activeSlv = slviewUC;
            slviewUC.Close += new Action<object>(CloseLyric);
            Grid.SetColumn(slviewUC, 0);

            Grid.SetColumnSpan(slviewUC, 2);

            centerUI.Children.Add(slviewUC);
            //Grid.SetRow(a, 0);
            //Grid.SetRowSpan(a, 4);
        }

        private void CloseLyric(object sender)
        {
            centerUI.Children.Remove((songLyricView)sender);
        }
        public static string RemoveSign(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        bool onSearch = false;
        bool playSearch = false;
        private void searchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            onSearch = true;
            string[] keywords = searchTB.Text.Split(' ');
            search.Clear();
            songMenu.Children.Clear();
            StreamReader reader = new StreamReader(history);
            string line;
            string convertedLine;
            while ((line = reader.ReadLine()) != null)
            {
                convertedLine = RemoveSign(line);
                int flag = 0;
                foreach (var keyword in keywords)
                {
                    if (keyword != "" && System.Text.RegularExpressions.Regex.IsMatch(convertedLine, RemoveSign(keyword), System.Text.RegularExpressions.RegexOptions.IgnoreCase)) { flag++; }
                }
                if (flag > keywords.Length / 2 && flag >= 1)
                {
                    StreamReader reader2 = new StreamReader(fav);
                    string line2;
                    Song song;
                    while ((line2 = reader2.ReadLine()) != null)
                    {
                        if (line == line2)
                        {
                            song = new Song(line, true);
                            break;
                        }
                    }
                    song = new Song(line, false);
                    reader2.Close();
                    search.Add(song);
                }
            }
            reader.Close();
            for (int i = 0; i < search.Count; i++)
            {
                Add_UcSongName(search[i], i);
            }
        }

        private void darkmodeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (darkmodeBtn.Content == darkmodeBtn.FindResource("Light"))
            {
                darkmodeBtn.Content = darkmodeBtn.FindResource("Dark");
                mainBorder.Background = Brushes.Black;
                searchBar.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                searchTB.Foreground = Brushes.Black;
                menuBorder.Background = this.FindResource("BlackBgrMenu") as LinearGradientBrush;
                CenterBorder.Background = this.FindResource("BlackBgrCenter") as LinearGradientBrush;
                PlayerBorder.Background = this.FindResource("BlackBgrPlayer") as LinearGradientBrush;
                BorderPopupMoreOption.Background = this.FindResource("BlackBgrMenu") as LinearGradientBrush;
                singerTxtBlock.Foreground = (Brush)new BrushConverter().ConvertFrom("#6e6e6e");

            }
            else
            {
                darkmodeBtn.Content = darkmodeBtn.FindResource("Light");
                mainBorder.Background = (Brush)new BrushConverter().ConvertFrom("#18c274");
                searchBar.Background = (Brush)new BrushConverter().ConvertFrom("#97e6c1");
                searchTB.Foreground = Brushes.Black;
                menuBorder.Background = this.FindResource("greenBgr1") as LinearGradientBrush;
                CenterBorder.Background = this.FindResource("greenBgr1") as LinearGradientBrush;
                PlayerBorder.Background = this.FindResource("greenBgr2") as LinearGradientBrush;
                BorderPopupMoreOption.Background = this.FindResource("greenBgr1") as LinearGradientBrush;
                singerTxtBlock.Foreground = (Brush)new BrushConverter().ConvertFrom("#9ae5c3");


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
