using System;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    public partial class PlaylistSelector : Form
    {
        private MainForm mainForm;

        private string[] filesPaths;
        private string[] filesNames;

        public PlaylistSelector(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            filesPaths = Directory.GetFiles(AppData.playlistsFolder, $"*{Playlist.extension}");
            filesNames = new string[filesPaths.Length];

            for (int i = 0; i < filesPaths.Length; i++)
            {
                filesNames[i] = Path.GetFileNameWithoutExtension(filesPaths[i]);
            }

            comboBox1.Items.AddRange(filesNames);
        }

        private void Confirm(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                mainForm.PlaylistChanged(JsonSerialization.Deserialize<Playlist>(filesPaths[comboBox1.SelectedIndex]));

                Close();
            }
        }
    }
}
