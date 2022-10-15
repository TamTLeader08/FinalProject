using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_MP3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            mediaPlayer.Height = listFilesView.Height;
            tbSongName.Width = mediaPlayer.Width;
        }

        OpenFileDialog openFileDialog;
        string[] fileNames;
        string[] filePaths;

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3, MP4, (*.mp3, *.mp4)| *.mp*";
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Open file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePaths = openFileDialog.FileNames;
                fileNames = openFileDialog.SafeFileNames;
                foreach (var file in fileNames)
                {
                    listFilesView.Items.Add(file);
                }
            }
        }

        private void listFiles_DoubleClick(object sender, EventArgs e)
        {
            int choice = listFilesView.SelectedIndex;
            if (choice < 0)
            {
                return;
            }
            mediaPlayer.URL = filePaths[choice];
            tbSongName.Text = fileNames[choice];
        }
    }
}
