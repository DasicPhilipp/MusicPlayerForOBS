using System;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    public partial class ConfirmPlaylistDeletion : Form
    {
        public delegate void PlaylistDeletion(Playlist deletedPlaylist);
        public event PlaylistDeletion Deleted;

        private Playlist playlist;
        private Form form;

        public ConfirmPlaylistDeletion(Playlist playlist, Form form)
        {
            InitializeComponent();

            this.playlist = playlist;
            this.form = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppData.playlistsFolder, $"{playlist.Name}{Playlist.extension}");
            if (File.Exists(path))
            {
                File.Delete(path);
                Deleted?.Invoke(playlist);
            }

            Close();
            form.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
