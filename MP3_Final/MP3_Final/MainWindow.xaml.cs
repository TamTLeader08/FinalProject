using Microsoft.Win32;
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
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace MP3_Final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // static variants
        MediaPlayer media = new MediaPlayer();
        string fileName = string.Empty, path = string.Empty;
        List<string> files = new List<string>();
        int i = 0;// bien toan cuc chi vi tri bai hat trong playlist
        // methods
        public MainWindow()
        {
            InitializeComponent();
            LoadVolume();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                DefaultExt = ".mp3",
            };
            if (fileDialog.ShowDialog() == true)
            {
                fileName = fileDialog.FileName;
                media.Open(new Uri(fileName));
                media.Play();
            }// code cua huy 
            */
            //var dialog = new Microsoft.Win32.OpenFileDialog();
            /* // code open 1 file
             dialog.Multiselect= false;
            dialog.DefaultExt = ".mp3,.flac,.ogg,.wav"; // Default file extension
            dialog.Filter = "All Media Files|*.wav;*.flac;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV"; // Filter files by extension 

            // ket qua cua dialog
            if (dialog.ShowDialog() == true)
            {
                fileName = dialog.FileName;
            }*/

            // code open nhieu file trong folder hay con goi la open folder
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.RootFolder= Environment.SpecialFolder.MyDocuments;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                path = dialog.SelectedPath;
                /*FileInfo[] file = new DirectoryInfo(path).GetFiles("*.mp3");*/
                var fileInfos = new DirectoryInfo(path).GetFilesByExtentions(".wav",".flac", ".aac", ".wma", ".wmv", ".avi", ".mpg", ".mpeg", ".m1v", ".mp2", ".mp3", ".mpa", ".mpe", ".m3u", ".mp4", ".mov", ".3g2", ".3gp2", ".3gp", ".3gpp", ".m4a", ".cda", ".aif", ".aifc", ".aiff", ".mid", ".midi", ".rmi", ".mkv", ".WAV", ".AAC", ".WMA", ".WMV", ".AVI", ".MPG", ".MPEG", ".M1V", ".MP2", ".MP3", ".MPA", ".MPE", ".M3U", ".MP4", ".MOV", ".3G2", ".3GP2", ".3GP", ".3GPP", ".M4A", ".CDA", ".AIF", ".AIFC", ".AIFF", ".MID", ".MIDI", ".RMI", ".MKV");
                foreach( FileInfo fil in fileInfos) 
                {
                    files.Add(fil.FullName);
                }
            }

            fileName = files[i];
            media.Open(new Uri(fileName));
            media.MediaEnded += Media_Ended;// them event chay bai tiep theo
            media.Play();

        }
        private void Media_Ended (object sender, EventArgs e)
        {
            ++i;
            media.Stop();
            media.Open(new Uri(files[i]));
            media.Position = TimeSpan.Zero;// chay nhac tu 00:00
            media.Play();
        }
        
        
        private void sldVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Volume = sldVolume.Value;
        }

        void LoadVolume()
        {
            sldVolume.Value = 1;
            media.Volume = sldVolume.Value;
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
