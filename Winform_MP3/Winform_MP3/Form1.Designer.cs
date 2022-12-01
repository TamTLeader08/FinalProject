namespace Winform_MP3
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbSongName = new System.Windows.Forms.TextBox();
            this.mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.listFilesView = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSongName
            // 
            this.tbSongName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSongName.Location = new System.Drawing.Point(16, 455);
            this.tbSongName.Name = "tbSongName";
            this.tbSongName.Size = new System.Drawing.Size(677, 34);
            this.tbSongName.TabIndex = 1;
            // 
            // mediaPlayer
            // 
            this.mediaPlayer.Enabled = true;
            this.mediaPlayer.Location = new System.Drawing.Point(16, 10);
            this.mediaPlayer.Name = "mediaPlayer";
            this.mediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mediaPlayer.OcxState")));
            this.mediaPlayer.Size = new System.Drawing.Size(677, 419);
            this.mediaPlayer.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBrowser);
            this.panel1.Controls.Add(this.listFilesView);
            this.panel1.Location = new System.Drawing.Point(730, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 492);
            this.panel1.TabIndex = 3;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowser.Location = new System.Drawing.Point(4, 439);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(246, 44);
            this.btnBrowser.TabIndex = 1;
            this.btnBrowser.Text = "Browser";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // listFilesView
            // 
            this.listFilesView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listFilesView.FormattingEnabled = true;
            this.listFilesView.ItemHeight = 25;
            this.listFilesView.Location = new System.Drawing.Point(4, 4);
            this.listFilesView.Name = "listFilesView";
            this.listFilesView.Size = new System.Drawing.Size(243, 429);
            this.listFilesView.TabIndex = 0;
            this.listFilesView.DoubleClick += new System.EventHandler(this.listFiles_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mediaPlayer);
            this.panel2.Controls.Add(this.tbSongName);
            this.panel2.Location = new System.Drawing.Point(12, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(712, 498);
            this.panel2.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 523);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(1027, 570);
            this.MinimumSize = new System.Drawing.Size(1027, 570);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Music Player";
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
        private System.Windows.Forms.TextBox tbSongName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.ListBox listFilesView;
        private System.Windows.Forms.Panel panel2;
    }
}

