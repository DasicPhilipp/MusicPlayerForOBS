using System;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    public partial class PlaylistExtractionForm : Form
    {
        public delegate void PlaylistDeletion(Playlist deletedPlaylist);
        public event PlaylistDeletion Deleted;

        private Playlist _playlist;
        private Form _closureForm;

        public PlaylistExtractionForm(Playlist playlist, Form form)
        {
            InitializeComponent();

            this._playlist = playlist;
            this._closureForm = form;
        }

        private void YesClick(object sender, EventArgs e)
        {
            string path = Path.Combine(AppData.PlaylistsFolder, $"{_playlist.Name}{Playlist.Extension}");
            if (File.Exists(path))
            {
                File.Delete(path);
                Deleted?.Invoke(_playlist);
            }

            Close();
            _closureForm.Close();
        }

        private void NoClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
