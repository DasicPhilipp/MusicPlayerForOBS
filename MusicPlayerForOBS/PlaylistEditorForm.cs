using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    public partial class PlaylistEditorForm : Form
    {
        private Playlist _playlist;
        private MainForm _mainForm;

        private Button _selectPlaylist;

        private string _currentPlaylistName;
        private bool _playlistNameIsCorrect;

        public PlaylistEditorForm(Playlist playlist, MainForm mainForm)
        {
            InitializeComponent();
            this.FormClosing += PlaylistEditor_Closing;

            this._playlist = playlist;
            this._mainForm = mainForm;

            _currentPlaylistName = playlist.Name;

            _playlistName.Text = playlist.Name;

            _playlistSaver.Visible = true;
            _playlistSaver.Enabled = true;

            _playlistCreator.Visible = false;
            _playlistCreator.Enabled = false;

            _playlistDeleter.Visible = true;
            _playlistDeleter.Enabled = true;

            _filesInPlaylist.Items.AddRange(playlist.GetSongsKeys());

            _refreshingFolders.Items.AddRange(_playlist.GetRefreshingFolders());
        }

        public PlaylistEditorForm(Button selectPlaylistButton)
        {
            InitializeComponent();

            _playlist = new Playlist(textBox1.Text);

            _playlistSaver.Visible = false;
            _playlistSaver.Enabled = false;

            _playlistCreator.Visible = true;
            _playlistCreator.Enabled = true;

            _playlistDeleter.Visible = false;
            _playlistDeleter.Enabled = false;

            this._selectPlaylist = selectPlaylistButton;
        }

        private void PlaylistName_Change(object sender, EventArgs e)
        {
            string[] filesPaths = Directory.GetFiles(AppData.PlaylistsFolder, $"*{Playlist.Extension}");
            string[] filesNames = new string[filesPaths.Length];

            for (int i = 0; i < filesPaths.Length; i++)
            {
                filesNames[i] = Path.GetFileNameWithoutExtension(filesPaths[i]);
            }

            //if (!filesNames.Contains(textBox.Text))
            //{
            //    playlist.Name = textBox.Text;

            //    errorLabel.Visible = false;
            //}
            if (string.IsNullOrEmpty(_playlistName.Text) || _playlistName.Text.IndexOfAny(Path.GetInvalidFileNameChars()) > 0)
            {
                _errorsLabel.Text = "File name is incorrect";
                _errorsLabel.Visible = true;
            }
            else
            {
                _playlist.Name = _playlistName.Text;

                _errorsLabel.Visible = false;
            }
            //else
            //{
            //    textBox.Text = "";

            //    errorLabel.Text = "This playlist already exists";
            //    errorLabel.Visible = true;
            //}

            _playlistNameIsCorrect = !_errorsLabel.Visible;
        }

        private void AddFile_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = _chooseMusicFile.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                foreach (string path in _chooseMusicFile.FileNames)
                {
                    if (MainForm.AudioExtensions.Contains(Path.GetExtension(path)))
                    {
                        _playlist.AddSong(path);
                    }
                }

                _filesInPlaylist.Items.Clear();
                _filesInPlaylist.Items.AddRange(_playlist.GetSongsKeys());
            }
        }

        private void AddFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = _chooseMusicFolder.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                foreach (string path in Directory.GetFiles(_chooseMusicFolder.SelectedPath))
                {
                    if (MainForm.AudioExtensions.Contains(Path.GetExtension(path)))
                    {
                        _playlist.AddSong(path);
                    }
                }

                _filesInPlaylist.Items.Clear();
                _filesInPlaylist.Items.AddRange(_playlist.GetSongsKeys());
            }
        }

        private void RemoveFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = _removeMusicFolder.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                foreach (string path in Directory.GetFiles(_removeMusicFolder.SelectedPath))
                {
                    if (MainForm.AudioExtensions.Contains(Path.GetExtension(path)))
                    {
                        _playlist.TryDeleteSongByPath(path);
                    }
                }

                _filesInPlaylist.Items.Clear();
                _filesInPlaylist.Items.AddRange(_playlist.GetSongsKeys());
            }
        }

        private void DeleteFile_IndexChanged(object sender, EventArgs e)
        {
            _playlist.TryDeleteSongByKey(_filesInPlaylist.Items[_filesInPlaylist.SelectedIndex] as string);
            _filesInPlaylist.Items.Clear();
            _filesInPlaylist.Items.AddRange(_playlist.GetSongsKeys());
        }

        private void SavePlaylist_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppData.PlaylistsFolder, $"{_currentPlaylistName}{Playlist.Extension}");

            if (_playlistNameIsCorrect)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                JsonSerialization.SerializeAsync(_playlist, AppData.PlaylistsFolder + _playlist.Name + Playlist.Extension);
                _currentPlaylistName = _playlist.Name;
                _playlistName.Text = _playlist.Name;

                _mainForm.PlaylistChanged(_playlist);

                Close();
            }
        }

        private void ConfirmExtraction_Click(object sender, EventArgs e)
        {
            PlaylistExtractionForm confirm = new PlaylistExtractionForm(_playlist, this);
            confirm.Deleted += _mainForm.PlaylistDeleted;
            confirm.Show();
        }

        private void AddRefreshingFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = _chooseRefreshingFolder.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                _playlist.SetRefreshingFolder(_chooseRefreshingFolder.SelectedPath);
                _refreshingFolders.Items.Add(_chooseRefreshingFolder.SelectedPath);
            }
        }
        private void RefreshingFolders_IndexChanged(object sender, EventArgs e)
        {
            _playlist.RemoveRefreshingFolder(_refreshingFolders.Items[_refreshingFolders.SelectedIndex] as string);
            _refreshingFolders.Items.Clear();
            _refreshingFolders.Items.AddRange(_playlist.GetRefreshingFolders());
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if (_playlistNameIsCorrect)
            {
                JsonSerialization.SerializeAsync(_playlist, AppData.PlaylistsFolder + _playlist.Name + Playlist.Extension);
                _selectPlaylist.Enabled = true;

                Close();
            }
        }

        private void PlaylistEditor_Closing(object sender, FormClosingEventArgs e)
        {
            _playlist.Name = _currentPlaylistName;
        }
    }
}
