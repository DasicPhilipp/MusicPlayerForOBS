using System;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    public partial class PlaylistEditor : Form
    {
        private Playlist playlist;
        private MainForm mainForm;

        private string currentPlaylistName;
        private bool playlistNameIsCorrect;

        public PlaylistEditor(Playlist playlist, MainForm mainForm)
        {
            InitializeComponent();
            this.FormClosing += PlaylistEditor_FormClosing;

            this.playlist = playlist;
            this.mainForm = mainForm;

            currentPlaylistName = playlist.Name;

            textBox2.Text = currentPlaylistName;

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(playlist.GetSongsKeys());

            checkBox1.Checked = playlist.AutomaticallyAddSongsFromFolders;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            playlistNameIsCorrect = PlaylistBuilder.PlaylistNameChange(textBox2, errorLabel, playlist);
        }

        private void PlaylistEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            playlist.Name = currentPlaylistName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PlaylistBuilder.ChooseMusicFile(openFileDialog1, comboBox1, playlist);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PlaylistBuilder.ChooseMusicFolder(folderBrowserDialog1, comboBox1, playlist, checkBox1.Checked);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlaylistBuilder.DeleteSongsFromFolder(folderBrowserDialog2, comboBox1, playlist);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlaylistBuilder.DeleteSongComboBox(comboBox1, playlist);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppData.playlistsFolder, $"{currentPlaylistName}{Playlist.extension}");

            if (playlistNameIsCorrect)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                JsonSerialization.SerializeAsync(playlist, AppData.playlistsFolder, playlist.Name + Playlist.extension);
                currentPlaylistName = playlist.Name;
                textBox2.Text = playlist.Name;

                mainForm.PlaylistChanged(playlist);

                Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ConfirmPlaylistDeletion confirm = new ConfirmPlaylistDeletion(playlist, this);
            confirm.Deleted += mainForm.PlaylistDeleted;
            confirm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PlaylistBuilder.UpdateFolders(playlist, checkBox1.Checked);
        }
    }
}
