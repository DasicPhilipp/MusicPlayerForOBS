
namespace MusicPlayerForOBS
{
    partial class PlaylistEditorForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._filesInPlaylist = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this._errorsLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this._playlistName = new System.Windows.Forms.TextBox();
            this._playlistSaver = new System.Windows.Forms.Button();
            this._playlistDeleter = new System.Windows.Forms.Button();
            this._chooseMusicFile = new System.Windows.Forms.OpenFileDialog();
            this._chooseMusicFolder = new System.Windows.Forms.FolderBrowserDialog();
            this._removeMusicFolder = new System.Windows.Forms.FolderBrowserDialog();
            this._addRefreshFolder = new System.Windows.Forms.Button();
            this._refreshingFolders = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._chooseRefreshingFolder = new System.Windows.Forms.FolderBrowserDialog();
            this._playlistCreator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(-113, -29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Title:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(-49, -32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(494, 20);
            this.textBox1.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(558, 22);
            this.label2.TabIndex = 23;
            this.label2.Text = "Remove the music file (click on a song to remove it):\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _filesInPlaylist
            // 
            this._filesInPlaylist.BackColor = System.Drawing.SystemColors.Window;
            this._filesInPlaylist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._filesInPlaylist.Font = new System.Drawing.Font("Minecraft Rus", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._filesInPlaylist.ForeColor = System.Drawing.SystemColors.WindowText;
            this._filesInPlaylist.FormattingEnabled = true;
            this._filesInPlaylist.IntegralHeight = false;
            this._filesInPlaylist.Location = new System.Drawing.Point(12, 207);
            this._filesInPlaylist.Name = "_filesInPlaylist";
            this._filesInPlaylist.Size = new System.Drawing.Size(558, 21);
            this._filesInPlaylist.TabIndex = 22;
            this._filesInPlaylist.Tag = "";
            this._filesInPlaylist.SelectedIndexChanged += new System.EventHandler(this.DeleteFile_IndexChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(12, 130);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(558, 39);
            this.button2.TabIndex = 21;
            this.button2.Text = "Remove Songs in Folder From the Playlist";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.RemoveFolder_Click);
            // 
            // _errorsLabel
            // 
            this._errorsLabel.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._errorsLabel.ForeColor = System.Drawing.Color.Red;
            this._errorsLabel.Location = new System.Drawing.Point(12, 36);
            this._errorsLabel.Name = "_errorsLabel";
            this._errorsLabel.Size = new System.Drawing.Size(558, 46);
            this._errorsLabel.TabIndex = 20;
            this._errorsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._errorsLabel.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Title:";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Minecraft Rus", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button4.Location = new System.Drawing.Point(298, 85);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(272, 39);
            this.button4.TabIndex = 18;
            this.button4.Text = "Choose All Music In the Folder";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.AddFolder_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Minecraft Rus", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button3.Location = new System.Drawing.Point(12, 85);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(280, 39);
            this.button3.TabIndex = 17;
            this.button3.Text = "Choose the Music File";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.AddFile_Click);
            // 
            // _playlistName
            // 
            this._playlistName.Location = new System.Drawing.Point(76, 13);
            this._playlistName.Name = "_playlistName";
            this._playlistName.Size = new System.Drawing.Size(494, 20);
            this._playlistName.TabIndex = 16;
            this._playlistName.TextChanged += new System.EventHandler(this.PlaylistName_Change);
            // 
            // _playlistSaver
            // 
            this._playlistSaver.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._playlistSaver.Location = new System.Drawing.Point(222, 407);
            this._playlistSaver.Name = "_playlistSaver";
            this._playlistSaver.Size = new System.Drawing.Size(132, 44);
            this._playlistSaver.TabIndex = 15;
            this._playlistSaver.Text = "Confirm Changes";
            this._playlistSaver.UseVisualStyleBackColor = true;
            this._playlistSaver.Click += new System.EventHandler(this.SavePlaylist_Click);
            // 
            // _playlistDeleter
            // 
            this._playlistDeleter.BackColor = System.Drawing.Color.Red;
            this._playlistDeleter.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._playlistDeleter.ForeColor = System.Drawing.Color.White;
            this._playlistDeleter.Location = new System.Drawing.Point(443, 407);
            this._playlistDeleter.Name = "_playlistDeleter";
            this._playlistDeleter.Size = new System.Drawing.Size(127, 44);
            this._playlistDeleter.TabIndex = 24;
            this._playlistDeleter.Text = "Delete This Playlist";
            this._playlistDeleter.UseVisualStyleBackColor = false;
            this._playlistDeleter.Click += new System.EventHandler(this.ConfirmExtraction_Click);
            // 
            // _chooseMusicFile
            // 
            this._chooseMusicFile.FileName = "openFileDialog1";
            // 
            // _addRefreshFolder
            // 
            this._addRefreshFolder.Font = new System.Drawing.Font("Minecraft Rus", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._addRefreshFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this._addRefreshFolder.Location = new System.Drawing.Point(12, 281);
            this._addRefreshFolder.Name = "_addRefreshFolder";
            this._addRefreshFolder.Size = new System.Drawing.Size(558, 39);
            this._addRefreshFolder.TabIndex = 26;
            this._addRefreshFolder.Text = "Choose folders to automatically add new songs from them to the playlist";
            this._addRefreshFolder.UseVisualStyleBackColor = true;
            this._addRefreshFolder.Click += new System.EventHandler(this.AddRefreshingFolder_Click);
            // 
            // _refreshingFolders
            // 
            this._refreshingFolders.BackColor = System.Drawing.SystemColors.Window;
            this._refreshingFolders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._refreshingFolders.Font = new System.Drawing.Font("Minecraft Rus", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._refreshingFolders.FormattingEnabled = true;
            this._refreshingFolders.Location = new System.Drawing.Point(12, 348);
            this._refreshingFolders.Name = "_refreshingFolders";
            this._refreshingFolders.Size = new System.Drawing.Size(558, 21);
            this._refreshingFolders.TabIndex = 27;
            this._refreshingFolders.SelectedIndexChanged += new System.EventHandler(this.RefreshingFolders_IndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(9, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(561, 22);
            this.label4.TabIndex = 28;
            this.label4.Text = "Remove automatically updating folders from the playlist:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _playlistCreator
            // 
            this._playlistCreator.Font = new System.Drawing.Font("Minecraft Rus", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._playlistCreator.Location = new System.Drawing.Point(222, 407);
            this._playlistCreator.Name = "_playlistCreator";
            this._playlistCreator.Size = new System.Drawing.Size(132, 44);
            this._playlistCreator.TabIndex = 29;
            this._playlistCreator.Text = "Create";
            this._playlistCreator.UseVisualStyleBackColor = true;
            this._playlistCreator.Click += new System.EventHandler(this.Create_Click);
            // 
            // PlaylistEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 463);
            this.Controls.Add(this._playlistCreator);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._refreshingFolders);
            this.Controls.Add(this._addRefreshFolder);
            this.Controls.Add(this._playlistDeleter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._filesInPlaylist);
            this.Controls.Add(this.button2);
            this.Controls.Add(this._errorsLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this._playlistName);
            this.Controls.Add(this._playlistSaver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PlaylistEditorForm";
            this.Text = "Playlist Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _filesInPlaylist;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label _errorsLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox _playlistName;
        private System.Windows.Forms.Button _playlistSaver;
        private System.Windows.Forms.Button _playlistDeleter;
        private System.Windows.Forms.OpenFileDialog _chooseMusicFile;
        private System.Windows.Forms.FolderBrowserDialog _chooseMusicFolder;
        private System.Windows.Forms.FolderBrowserDialog _removeMusicFolder;
        private System.Windows.Forms.Button _addRefreshFolder;
        private System.Windows.Forms.ComboBox _refreshingFolders;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog _chooseRefreshingFolder;
        private System.Windows.Forms.Button _playlistCreator;
    }
}