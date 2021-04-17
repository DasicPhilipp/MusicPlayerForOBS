using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    public enum PlayMode
    {
        None,
        Loop,
        Shuffle,
        LoopAndShuffle
    }

    public partial class MainForm : Form
    {
        public readonly string Version;

        public Playlist CurrentPlaylist;

        public static readonly string[] AudioExtensions =
        {
            ".mp3",
            ".wav"
        };

        private string _pauseSymbol = " ||";
        private string _playSymbol = " >";

        private WaveOutEvent _waveOut;

        private Mp3FileReader _mp3Reader;
        private WaveFileReader _wavReader;

        private List<string> _audioPathsToPlay;

        private List<int> _playOrder;
        private int _playingSongNumber;
        private bool _nextButtonClicked;

        private int _volume;

        private string _obsFilePath;

        private PlayMode _playMode;

        public MainForm()
        {
            InitializeComponent();

            Version = AppData.Version;

            _waveOut = new WaveOutEvent();
            _audioPathsToPlay = new List<string>();

            Application.ApplicationExit += OnAppClose;
            _waveOut.PlaybackStopped += OnPlaybackStopped;

            Directory.CreateDirectory(AppData.PlaylistsFolder);
            Directory.CreateDirectory(AppData.SettingsFolder);

            if (File.Exists(Path.Combine(AppData.SettingsFolder, "settings.json")))
            {
                AppData data = JsonSerialization.Deserialize<AppData>(Path.Combine(AppData.SettingsFolder, "settings.json"));
                _volume = data.Volume;
                _obsFilePath = data.ObsFilePath;
            }
            else
            {
                _volume = 10;
            }

            if (_obsFilePath == null) _obsFilePath = "";

            if (Directory.GetFiles(AppData.PlaylistsFolder).Length != 0)
            {
                _selectPlaylist.Enabled = true;
            }
            else
            {
                _selectPlaylist.Enabled = false;
            }

            Text += " " + Version;

            _playMode = PlayMode.None;

            _volumeTrackBar.Value = _volume;
            _volumeIndicator.Text = $"Volume: {_volume}";

            _musicTimer.Interval = 1000;
            _musicTimer.Enabled = true;
        }

        private void SetSongToPlay(int songNumber)
        {
            string[] brokenPaths = CurrentPlaylist?.CheckPathsActuality();
            if (brokenPaths != null)
            {
                foreach (string path in _audioPathsToPlay.ToArray())
                {
                    foreach (string brokenPath in brokenPaths)
                    {
                        if (path == brokenPath)
                        {
                            int brokenIndex = _audioPathsToPlay.IndexOf(path);
                            _audioPathsToPlay.Remove(path);
                            int brokenItem = _playOrder[brokenIndex];
                            _playOrder.RemoveAt(brokenIndex);

                            int[] newPlayOrder = new int[_playOrder.Count];
                            for (int i = 0; i < _playOrder.Count; i++)
                            {
                                if (_playOrder[i] > brokenItem)
                                {
                                    newPlayOrder[i] = _playOrder[i] - 1;
                                }
                                else
                                {
                                    newPlayOrder[i] = _playOrder[i];
                                }
                            }
                            _playOrder = newPlayOrder.ToList();
                        }
                    }
                }
            }

            try
            {
                if (songNumber >= _audioPathsToPlay.Count)
                    songNumber = 0;
                else if (songNumber < 0)
                    songNumber = _audioPathsToPlay.Count - 1;

                if (_playingSongNumber >= _audioPathsToPlay.Count)
                    _playingSongNumber = 0;
                else if (_playingSongNumber < 0)
                    _playingSongNumber = _audioPathsToPlay.Count - 1;

                _waveOut.Dispose();
                if (_mp3Reader != null) _mp3Reader.Dispose();
                if (_wavReader != null) _wavReader.Dispose();

                songNumber = _playOrder[songNumber];
                switch (Path.GetExtension(_audioPathsToPlay[songNumber]))
                {
                    case ".mp3":
                        _mp3Reader = new Mp3FileReader(_audioPathsToPlay[songNumber]);
                        _waveOut.Init(_mp3Reader);

                        timeline.Maximum = (int)double.Parse(_mp3Reader.TotalTime.TotalSeconds.ToString());

                        break;

                    case ".wav":
                        _wavReader = new WaveFileReader(_audioPathsToPlay[songNumber]);
                        _waveOut.Init(_wavReader);

                        timeline.Maximum = (int)double.Parse(_wavReader.TotalTime.TotalSeconds.ToString());

                        break;
                }
                MusicName.Text = Path.GetFileNameWithoutExtension(_audioPathsToPlay[songNumber]);
                File.WriteAllText(Path.Combine(_obsFilePath, "!OBS.txt"), MusicName.Text);

                _errorsLabel.Text = "";
            }
            catch (NullReferenceException)
            {
                _errorsLabel.Text = "Error: Path is incorrect";
            }
            catch (ArgumentOutOfRangeException)
            {
                _errorsLabel.Text = "Error: Folder does not contain any audio";
            }
        }

        private bool ShuffleSongs(PlayMode mode)
        {
            if (mode == PlayMode.Shuffle || mode == PlayMode.LoopAndShuffle)
            {
                Random random = new Random();
                _playOrder = Enumerable.Repeat(0, _audioPathsToPlay.Count).Select((x, i) => new { i, rand = random.Next() }).OrderBy(x => x.rand).Select(x => x.i).ToList();

                return true;
            }
            else
            {
                _playOrder = Enumerable.Range(-1, _audioPathsToPlay.Count).Select(x => x + 1).ToList();

                return false;
            }
        }

        public void PlaylistChanged(Playlist playlist)
        {
            _waveOut.Dispose();
            if (_mp3Reader != null) _mp3Reader.Dispose();
            if (_wavReader != null) _wavReader.Dispose();

            _namePlaylistPlaying.Text = playlist.Name;
            CurrentPlaylist = playlist;

            _audioPathsToPlay.Clear();
            _audioPathsToPlay.AddRange(playlist.GetSongsValues());

            ShuffleSongs(_playMode);
            _waveOut.Stop();

            _editPlaylist.Enabled = true;
        }

        public void PlaylistDeleted(Playlist playlist)
        {
            if (playlist == CurrentPlaylist)
            {
                _waveOut.Dispose();
                if (_mp3Reader != null) _mp3Reader.Dispose();
                if (_wavReader != null) _wavReader.Dispose();

                _namePlaylistPlaying.Text = "";
                CurrentPlaylist = null;

                _audioPathsToPlay.Clear();
                _playOrder.Clear();

                _editPlaylist.Enabled = false;
            }
        }

        //
        //
        // WinForms buttons and other methods:
        //
        //

        private void ModeChanged(object sender, EventArgs e)
        {
            if (!loopCheckbox.Checked && !shuffleCheckBox.Checked)
            {
                _playMode = PlayMode.None;
            }

            else if (loopCheckbox.Checked && !shuffleCheckBox.Checked)
            {
                _playMode = PlayMode.Loop;
            }

            else if (!loopCheckbox.Checked && shuffleCheckBox.Checked)
            {
                _playMode = PlayMode.Shuffle;
            }

            else if (loopCheckbox.Checked && shuffleCheckBox.Checked)
            {
                _playMode = PlayMode.LoopAndShuffle;
            }

            ShuffleSongs(_playMode);
        }

        private void OnAppClose(object sender, EventArgs e)
        {
            _waveOut.Dispose();
            if (_mp3Reader != null) _mp3Reader.Dispose();
            if (_wavReader != null) _wavReader.Dispose();

            AppData data = new AppData
            {
                Volume = _volume,
                ObsFilePath = _obsFilePath
            };

            JsonSerialization.SerializeAsync(data, AppData.SettingsFolder + "settings.json");
        }
        private void NextTrack_Click(object sender, EventArgs e)
        {
            _waveOut.Stop();
            _nextButtonClicked = true;
            SetSongToPlay(++_playingSongNumber);

            try
            {
                _waveOut.Play();
            }
            catch (InvalidOperationException)
            {
                _errorsLabel.Text = "Error: Folder does not contain any audio";
            }
        }

        private void PreviousTrack_Click(object sender, EventArgs e)
        {
            _waveOut.Stop();
            _nextButtonClicked = true;
            SetSongToPlay(--_playingSongNumber);

            try
            {
                _waveOut.Play();
            }
            catch (InvalidOperationException)
            {
                _errorsLabel.Text = "Error: Folder does not contain any audio";
            }
        }

        private void VolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;

            _volume = trackBar.Value;
            _waveOut.Volume = _volume / 100f;
            _volumeIndicator.Text = $"Volume: {trackBar.Value}";
        }

        private void ChooseMusicFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                try
                {
                    _waveOut.Dispose();
                    if (_mp3Reader != null) _mp3Reader.Dispose();
                    if (_wavReader != null) _wavReader.Dispose();

                    _audioPathsToPlay.Clear();

                    foreach (string path in Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                    {
                        if (AudioExtensions.Contains(Path.GetExtension(path)))
                        {
                            _audioPathsToPlay.Add(path);
                        }
                    }
                    _errorsLabel.Text = "";

                    ShuffleSongs(_playMode);
                    _waveOut.Stop();
                }
                catch (NullReferenceException)
                {
                    _errorsLabel.Text = "Error: Folder is empty";
                }
                catch (ArgumentException)
                {
                    _errorsLabel.Text = "Error: Path is incorrect";
                }

                _namePlaylistPlaying.Text = "";
            }

        }

        private void PlayAndPause_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (_waveOut.PlaybackState == PlaybackState.Playing)
            {
                _waveOut.Pause();

                button.Text = _playSymbol;
            }
            else if (_waveOut.PlaybackState == PlaybackState.Paused)
            {
                _waveOut.Play();

                button.Text = _pauseSymbol;
            }
            else if (_waveOut.PlaybackState == PlaybackState.Stopped)
            {
                try
                {
                    SetSongToPlay(_playingSongNumber);
                    _waveOut.Play();
                }
                catch { }

                button.Text = _pauseSymbol;
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (_audioPathsToPlay.Count - 1 <= _playingSongNumber && _playMode == PlayMode.None || _playMode == PlayMode.LoopAndShuffle)
            {
                return;
            }

            if (_playMode == PlayMode.Loop || _playMode == PlayMode.LoopAndShuffle)
            {
                SetSongToPlay(_playingSongNumber);
            }

            else if (!_nextButtonClicked)
            {
                SetSongToPlay(++_playingSongNumber);
            }

            _nextButtonClicked = false;

            _waveOut.Play();
        }

        private void PlaylistEditor_Click(object sender, EventArgs e)
        {
            PlaylistEditorForm playlistEditor = new PlaylistEditorForm(CurrentPlaylist, this);
            playlistEditor.Show();
        }

        private void ScrollTimeline_ValueChange(object sender, EventArgs e)
        {
            if (_waveOut.PlaybackState == PlaybackState.Playing)
            {
                if (_mp3Reader != null)
                {
                    _mp3Reader.CurrentTime = TimeSpan.FromSeconds(timeline.Value);
                }
                else if (_wavReader != null)
                {
                    _wavReader.CurrentTime = TimeSpan.FromSeconds(timeline.Value);
                }
            }
        }

        private void SongTimer_Tick(object sender, EventArgs e)
        {
            if (_waveOut.PlaybackState == PlaybackState.Playing)
            {
                timeline.Enabled = true;

                if (_mp3Reader != null)
                {
                    int secondsPassed = (int)double.Parse(_mp3Reader.CurrentTime.TotalSeconds.ToString());
                    if (secondsPassed < timeline.Maximum)
                    {
                        timeline.Value = secondsPassed;
                        label1.Text = _mp3Reader.CurrentTime.ToString("mm\\:ss");
                    }
                }
                else if (_wavReader != null)
                {
                    int secondsPassed = (int)double.Parse(_wavReader.CurrentTime.TotalSeconds.ToString());
                    if (secondsPassed < timeline.Maximum)
                    {
                        timeline.Value = secondsPassed;
                        label1.Text = _wavReader.CurrentTime.ToString("mm\\:ss");
                    }
                }
            }
            else
            {
                timeline.Enabled = false;
            }
        }

        private void CreateNewPlaylist_Click(object sender, EventArgs e)
        {
            PlaylistEditorForm playlistEditor = new PlaylistEditorForm(_selectPlaylist);
            playlistEditor.Show();
        }

        private void SelectPlaylist_Click(object sender, EventArgs e)
        {
            _waveOut.Pause();

            playButton.Text = _playSymbol;

            PlaylistSelectorForm playlistSelector = new PlaylistSelectorForm(this);
            playlistSelector.Show();
        }

        private void ObsFileChangePath_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = _obsPathBrowserDialog.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                _obsFilePath = _obsPathBrowserDialog.SelectedPath;
            }
        }
    }
}
