using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MP3_Final.UserControls
{
    /// <summary>
    /// Interaction logic for SongItem.xaml
    /// </summary>
    public partial class SongName : UserControl
    {
        public SongName()
        {
            InitializeComponent();
        }
        public int IndexOfSong
        {
            get { return (int)GetValue(IndexOfSongProperty); }
            set { SetValue(IndexOfSongProperty, value); }
        }

        public static readonly DependencyProperty IndexOfSongProperty = DependencyProperty.Register
            ("IndexOfSong", typeof(int), typeof(SongName));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register
            ("Path", typeof(string), typeof(SongName));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register
            ("Title", typeof(string), typeof(SongName));

        public string Singer
        {
            get { return (string)GetValue(SingerProperty); }
            set { SetValue(SingerProperty, value); }
        }

        public static readonly DependencyProperty SingerProperty = DependencyProperty.Register
            ("Singer", typeof(string), typeof(SongName));



        public string Number
        {
            get { return (string)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register
            ("Number", typeof(string), typeof(SongName));



        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register
            ("Time", typeof(string), typeof(SongName));


        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register
            ("IsActive", typeof(bool), typeof(SongName));

        public static readonly RoutedEvent MLeftBtnD_BdSongNameEvent = EventManager.RegisterRoutedEvent(
        "MLeftBtnD_BdSongName", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SongName));

        public event RoutedEventHandler MLeftBtnD_BdSongName
        {
            add { AddHandler(Border.MouseLeftButtonDownEvent, value); }
            remove { AddHandler(Border.MouseLeftButtonDownEvent, value); }
        }

        private void AddAlbumBtn_PopupClick(object sender, RoutedEventArgs e)
        {
            AddToPlayList add  = new AddToPlayList();
            add.ShowDialog();
            string playList = add.playListPath;
            if (playList != null)
            {
                string[] files = File.ReadAllLines(playList);
                foreach ( string file in files )
                {
                    if (Path == file)
                    {
                        MessageBox.Show("Thêm thành công!");
                        return;
                    }
                }

                if (new FileInfo(playList).Length != 0)
                {
                    File.AppendAllText(playList, "\n");
                }

                File.AppendAllText(playList, Path);
                MessageBox.Show("Thêm thành công!");
            }
        }

        public event Action<object> DeleteClick;
        private void DelSongBtn_PopupClick(object sender, RoutedEventArgs e)
        {
            if (DeleteClick != null)
            {
                string tempPath = MainWindow.currentPlaylist;
                DeleteClick(this);
                if (!File.Exists(tempPath))
                {
                    return;
                }
                File.WriteAllLines(tempPath, File.ReadLines(tempPath).Where(l => l != Path).ToList());
            }
        }

    }
}
