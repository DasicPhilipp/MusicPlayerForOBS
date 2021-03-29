using System;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    public partial class PlaylistCreation : Form
    {
        private Playlist playlist;

        private Button selectPlaylistButton;
        private bool playlistNameIsCorrect;

        public PlaylistCreation(Button selectPlaylistButton)
        {
            InitializeComponent();

            playlist = new Playlist(textBox1.Text);

            this.selectPlaylistButton = selectPlaylistButton;

            checkBox1.Checked = playlist.AutomaticallyAddSongsFromFolders;
        }

        private void CreateButton(object sender, EventArgs e)
        {
            if (playlistNameIsCorrect)
            {
                JsonSerialization.SerializeAsync(playlist, AppData.playlistsFolder, playlist.Name + Playlist.extension);
                selectPlaylistButton.Enabled = true;

                Close();
            }
        }

        private void ChooseMusicFolder(object sender, EventArgs e)
        {
            PlaylistBuilder.ChooseMusicFolder(folderBrowserDialog1, comboBox1, playlist, checkBox1.Checked);
        }

        private void ChooseSongFile(object sender, EventArgs e)
        {
            PlaylistBuilder.ChooseMusicFile(openFileDialog1, comboBox1, playlist);
        }

        private void PlaylistNameChanged(object sender, EventArgs e)
        {
            playlistNameIsCorrect = PlaylistBuilder.PlaylistNameChange(textBox1, errorLabel, playlist);
        }

        private void DropdownListDeleteIndex(object sender, EventArgs e)
        {
            PlaylistBuilder.DeleteSongComboBox(comboBox1, playlist);
        }

        private void RemoveSongsInPlatlistFromFolder(object sender, EventArgs e)
        {
            PlaylistBuilder.DeleteSongsFromFolder(folderBrowserDialog2, comboBox1, playlist);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PlaylistBuilder.UpdateFolders(playlist, checkBox1.Checked);
        }
    }
}
