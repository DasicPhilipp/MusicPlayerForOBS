using System;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    public partial class PlaylistSelectorForm : Form
    {
        private MainForm _mainForm;

        private string[] _filesPaths;
        private string[] _filesNames;

        public PlaylistSelectorForm(MainForm mainForm)
        {
            InitializeComponent();

            this._mainForm = mainForm;

            _filesPaths = Directory.GetFiles(AppData.PlaylistsFolder, $"*{Playlist.Extension}");
            _filesNames = new string[_filesPaths.Length];

            for (int i = 0; i < _filesPaths.Length; i++)
            {
                _filesNames[i] = Path.GetFileNameWithoutExtension(_filesPaths[i]);
            }

            _platlistsNamesBox.Items.AddRange(_filesNames);
        }

        private void Confirm(object sender, EventArgs e)
        {
            if (_platlistsNamesBox.SelectedIndex != -1)
            {
                _mainForm.PlaylistChanged(JsonSerialization.Deserialize<Playlist>(_filesPaths[_platlistsNamesBox.SelectedIndex]));

                Close();
            }
        }
    }
}
