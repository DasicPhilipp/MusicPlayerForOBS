
namespace MusicPlayerForOBS
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this._volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.MusicName = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this._volumeIndicator = new System.Windows.Forms.Label();
            this._errorsLabel = new System.Windows.Forms.Label();
            this.timeline = new System.Windows.Forms.TrackBar();
            this.loopCheckbox = new System.Windows.Forms.CheckBox();
            this.shuffleCheckBox = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this._selectPlaylist = new System.Windows.Forms.Button();
            this._editPlaylist = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._musicTimer = new System.Windows.Forms.Timer(this.components);
            this._namePlaylistPlaying = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this._obsPathBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this._volumeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Minecraft Rus", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(12, 386);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(323, 52);
            this.button2.TabIndex = 1;
            this.button2.Text = "<<";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.PreviousTrack_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Minecraft Rus", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(465, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(323, 52);
            this.button1.TabIndex = 2;
            this.button1.Text = ">>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.NextTrack_Click);
            // 
            // VolumeTrackBar
            // 
            this._volumeTrackBar.Location = new System.Drawing.Point(30, 167);
            this._volumeTrackBar.Maximum = 100;
            this._volumeTrackBar.Name = "VolumeTrackBar";
            this._volumeTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this._volumeTrackBar.Size = new System.Drawing.Size(45, 161);
            this._volumeTrackBar.TabIndex = 3;
            this._volumeTrackBar.Scroll += new System.EventHandler(this.VolumeTrackBar_Scroll);
            // 
            // MusicName
            // 
            this.MusicName.AutoSize = true;
            this.MusicName.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MusicName.Location = new System.Drawing.Point(12, 87);
            this.MusicName.Name = "MusicName";
            this.MusicName.Size = new System.Drawing.Size(0, 16);
            this.MusicName.TabIndex = 4;
            this.MusicName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(12, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(256, 35);
            this.button3.TabIndex = 5;
            this.button3.Text = "Choose the music folder\r\n";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ChooseMusicFolder_Click);
            // 
            // button4
            // 
            this.playButton.Font = new System.Drawing.Font("Minecraft Rus", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playButton.Location = new System.Drawing.Point(350, 386);
            this.playButton.Name = "button4";
            this.playButton.Size = new System.Drawing.Size(103, 52);
            this.playButton.TabIndex = 6;
            this.playButton.Text = " >";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.PlayAndPause_Click);
            // 
            // VolumeIndicator
            // 
            this._volumeIndicator.AutoSize = true;
            this._volumeIndicator.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._volumeIndicator.Location = new System.Drawing.Point(12, 148);
            this._volumeIndicator.Name = "VolumeIndicator";
            this._volumeIndicator.Size = new System.Drawing.Size(82, 16);
            this._volumeIndicator.TabIndex = 9;
            this._volumeIndicator.Text = "Volume: ";
            this._volumeIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ErrorsLabel
            // 
            this._errorsLabel.AutoSize = true;
            this._errorsLabel.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._errorsLabel.ForeColor = System.Drawing.Color.Red;
            this._errorsLabel.Location = new System.Drawing.Point(294, 21);
            this._errorsLabel.Name = "ErrorsLabel";
            this._errorsLabel.Size = new System.Drawing.Size(0, 16);
            this._errorsLabel.TabIndex = 10;
            this._errorsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar1
            // 
            this.timeline.Location = new System.Drawing.Point(12, 335);
            this.timeline.Name = "trackBar1";
            this.timeline.Size = new System.Drawing.Size(776, 45);
            this.timeline.TabIndex = 11;
            this.timeline.Scroll += new System.EventHandler(this.ScrollTimeline_ValueChange);
            // 
            // loopCheckbox
            // 
            this.loopCheckbox.AutoSize = true;
            this.loopCheckbox.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loopCheckbox.Location = new System.Drawing.Point(71, 289);
            this.loopCheckbox.Name = "loopCheckbox";
            this.loopCheckbox.Size = new System.Drawing.Size(75, 20);
            this.loopCheckbox.TabIndex = 12;
            this.loopCheckbox.Text = "Loop";
            this.loopCheckbox.UseVisualStyleBackColor = true;
            this.loopCheckbox.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // shuffleCheckBox
            // 
            this.shuffleCheckBox.AutoSize = true;
            this.shuffleCheckBox.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shuffleCheckBox.Location = new System.Drawing.Point(71, 263);
            this.shuffleCheckBox.Name = "shuffleCheckBox";
            this.shuffleCheckBox.Size = new System.Drawing.Size(101, 20);
            this.shuffleCheckBox.TabIndex = 13;
            this.shuffleCheckBox.Text = "Shuffle";
            this.shuffleCheckBox.UseVisualStyleBackColor = true;
            this.shuffleCheckBox.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.Location = new System.Drawing.Point(582, 141);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(206, 31);
            this.button5.TabIndex = 14;
            this.button5.Text = "Create new playlist";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.CreateNewPlaylist_Click);
            // 
            // button6
            // 
            this._selectPlaylist.Enabled = false;
            this._selectPlaylist.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._selectPlaylist.Location = new System.Drawing.Point(582, 189);
            this._selectPlaylist.Name = "button6";
            this._selectPlaylist.Size = new System.Drawing.Size(206, 29);
            this._selectPlaylist.TabIndex = 15;
            this._selectPlaylist.Text = "Select playlist";
            this._selectPlaylist.UseVisualStyleBackColor = true;
            this._selectPlaylist.Click += new System.EventHandler(this.SelectPlaylist_Click);
            // 
            // button7
            // 
            this._editPlaylist.Enabled = false;
            this._editPlaylist.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._editPlaylist.Location = new System.Drawing.Point(582, 239);
            this._editPlaylist.Name = "button7";
            this._editPlaylist.Size = new System.Drawing.Size(206, 29);
            this._editPlaylist.TabIndex = 16;
            this._editPlaylist.Text = "Edit playlist";
            this._editPlaylist.UseVisualStyleBackColor = true;
            this._editPlaylist.Click += new System.EventHandler(this.PlaylistEditor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(373, 293);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "00:00";
            // 
            // timer1
            // 
            this._musicTimer.Tick += new System.EventHandler(this.SongTimer_Tick);
            // 
            // label2
            // 
            this._namePlaylistPlaying.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._namePlaylistPlaying.Location = new System.Drawing.Point(227, 158);
            this._namePlaylistPlaying.Name = "label2";
            this._namePlaylistPlaying.Size = new System.Drawing.Size(349, 23);
            this._namePlaylistPlaying.TabIndex = 18;
            this._namePlaylistPlaying.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Blue;
            this.button8.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(582, 274);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(206, 54);
            this.button8.TabIndex = 19;
            this.button8.Text = "Select path of the file for OBS";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.ObsFileChangePath_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button8);
            this.Controls.Add(this._namePlaylistPlaying);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._editPlaylist);
            this.Controls.Add(this._selectPlaylist);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.shuffleCheckBox);
            this.Controls.Add(this.loopCheckbox);
            this.Controls.Add(this.timeline);
            this.Controls.Add(this._errorsLabel);
            this.Controls.Add(this._volumeIndicator);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.MusicName);
            this.Controls.Add(this._volumeTrackBar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Music Player for OBS";
            ((System.ComponentModel.ISupportInitialize)(this._volumeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar _volumeTrackBar;
        private System.Windows.Forms.Label MusicName;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Label _volumeIndicator;
        private System.Windows.Forms.Label _errorsLabel;
        private System.Windows.Forms.TrackBar timeline;
        private System.Windows.Forms.CheckBox loopCheckbox;
        private System.Windows.Forms.CheckBox shuffleCheckBox;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button _selectPlaylist;
        private System.Windows.Forms.Button _editPlaylist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer _musicTimer;
        private System.Windows.Forms.Label _namePlaylistPlaying;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.FolderBrowserDialog _obsPathBrowserDialog;
    }
}

