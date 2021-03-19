using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Data;
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

        public Playlist currentPlaylist;

        public static readonly string[] AudioExtensions =
        {
            ".mp3",
            ".wav"
        };

        private string pauseSymbol = " ||";
        private string playSymbol = " >";

        private WaveOutEvent waveOut;

        private Mp3FileReader mp3Reader;
        private WaveFileReader wavReader;

        private List<string> audioPathsToPlay;

        private List<int> playOrder;
        private int playingSongNumber;
        private bool nextButtonClicked;

        private int volume;

        private string obsFilePath;

        private PlayMode playMode;

        public MainForm()
        {
            InitializeComponent();

            waveOut = new WaveOutEvent();
            audioPathsToPlay = new List<string>();

            Application.ApplicationExit += OnAppClose;
            waveOut.PlaybackStopped += OnPlaybackStopped;

            Directory.CreateDirectory(AppData.playlistsFolder);
            Directory.CreateDirectory(AppData.settingsFolder);

            if (File.Exists(Path.Combine(AppData.settingsFolder, "settings.json")))
            {
                AppData data = JsonSerialization.Deserialize<AppData>(Path.Combine(AppData.settingsFolder, "settings.json"));
                Version = data.Version;
                volume = data.Volume;
                obsFilePath = data.ObsFilePath;
            }

            if (obsFilePath == null) obsFilePath = "";

            if (Directory.GetFiles(AppData.playlistsFolder).Length != 0)
            {
                button6.Enabled = true;
            }
            else
            {
                button6.Enabled = false;
            }

            Text += " " + Version;

            playMode = PlayMode.None;

            VolumeTrackBar.Value = volume;
            VolumeIndicator.Text = $"Volume: {volume}";

            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void SetSongToPlay(int songNumber)
        {
            string[] brokenPaths = currentPlaylist?.CheckPathsActuality();
            if (brokenPaths != null)
            {
                foreach (string path in audioPathsToPlay.ToArray())
                {
                    foreach (string brokenPath in brokenPaths)
                    {
                        if (path == brokenPath)
                        {
                            int brokenIndex = audioPathsToPlay.IndexOf(path);
                            audioPathsToPlay.Remove(path);
                            int brokenItem = playOrder[brokenIndex];
                            playOrder.RemoveAt(brokenIndex);

                            int[] newPlayOrder = new int[playOrder.Count];
                            for (int i = 0; i < playOrder.Count; i++)
                            {
                                if (playOrder[i] > brokenItem)
                                {
                                    newPlayOrder[i] = playOrder[i] - 1;
                                }
                                else
                                {
                                    newPlayOrder[i] = playOrder[i];
                                }
                            }
                            playOrder = newPlayOrder.ToList();
                        }
                    }
                }
            }

            try
            {
                if (songNumber >= audioPathsToPlay.Count)
                    songNumber = 0;
                else if (songNumber < 0)
                    songNumber = audioPathsToPlay.Count - 1;

                if (playingSongNumber >= audioPathsToPlay.Count)
                    playingSongNumber = 0;
                else if (playingSongNumber < 0)
                    playingSongNumber = audioPathsToPlay.Count - 1;

                waveOut.Dispose();
                if (mp3Reader != null) mp3Reader.Dispose();
                if (wavReader != null) wavReader.Dispose();

                songNumber = playOrder[songNumber];
                switch (Path.GetExtension(audioPathsToPlay[songNumber]))
                {
                    case ".mp3":
                        mp3Reader = new Mp3FileReader(audioPathsToPlay[songNumber]);
                        waveOut.Init(mp3Reader);

                        trackBar1.Maximum = (int)double.Parse(mp3Reader.TotalTime.TotalSeconds.ToString());

                        break;

                    case ".wav":
                        wavReader = new WaveFileReader(audioPathsToPlay[songNumber]);
                        waveOut.Init(wavReader);

                        trackBar1.Maximum = (int)double.Parse(wavReader.TotalTime.TotalSeconds.ToString());

                        break;
                }
                MusicName.Text = Path.GetFileNameWithoutExtension(audioPathsToPlay[songNumber]);
                File.WriteAllText(Path.Combine(obsFilePath, "!OBS.txt"), MusicName.Text);

                ErrorsLabel.Text = "";
            }
            catch (NullReferenceException)
            {
                ErrorsLabel.Text = "Error: Path is incorrect";
            }
            catch (ArgumentOutOfRangeException)
            {
                ErrorsLabel.Text = "Error: Folder does not contain any audio";
            }
        }

        private bool ShuffleSongs(PlayMode mode)
        {
            if (mode == PlayMode.Shuffle || mode == PlayMode.LoopAndShuffle)
            {
                Random random = new Random();
                playOrder = Enumerable.Repeat(0, audioPathsToPlay.Count).Select((x, i) => new { i, rand = random.Next() }).OrderBy(x => x.rand).Select(x => x.i).ToList();

                return true;
            }
            else
            {
                playOrder = Enumerable.Range(-1, audioPathsToPlay.Count).Select(x => x + 1).ToList();

                return false;
            }
        }

        public void PlaylistChanged(Playlist playlist)
        {
            waveOut.Dispose();
            if (mp3Reader != null) mp3Reader.Dispose();
            if (wavReader != null) wavReader.Dispose();

            label2.Text = playlist.Name;
            currentPlaylist = playlist;

            audioPathsToPlay.Clear();
            audioPathsToPlay.AddRange(playlist.GetSongsValues());

            ShuffleSongs(playMode);
            waveOut.Stop();

            button7.Enabled = true;
        }

        public void PlaylistDeleted(Playlist playlist)
        {
            waveOut.Dispose();
            if (mp3Reader != null) mp3Reader.Dispose();
            if (wavReader != null) wavReader.Dispose();

            label2.Text = "";
            currentPlaylist = null;

            audioPathsToPlay.Clear();
            playOrder.Clear();

            button7.Enabled = false;
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
                playMode = PlayMode.None;
            }

            else if (loopCheckbox.Checked && !shuffleCheckBox.Checked)
            {
                playMode = PlayMode.Loop;
            }

            else if (!loopCheckbox.Checked && shuffleCheckBox.Checked)
            {
                playMode = PlayMode.Shuffle;
            }

            else if (loopCheckbox.Checked && shuffleCheckBox.Checked)
            {
                playMode = PlayMode.LoopAndShuffle;
            }

            ShuffleSongs(playMode);
        }

        private void OnAppClose(object sender, EventArgs e)
        {
            waveOut.Dispose();
            if (mp3Reader != null) mp3Reader.Dispose();
            if (wavReader != null) wavReader.Dispose();

            AppData data = new AppData
            {
                Version = Version,
                Volume = volume,
                ObsFilePath = obsFilePath
            };

            JsonSerialization.SerializeAsync(data, AppData.settingsFolder, "settings.json");
        }
        private void NextTrack_Click(object sender, EventArgs e)
        {
            waveOut.Stop();
            nextButtonClicked = true;
            SetSongToPlay(++playingSongNumber);

            try
            {
                waveOut.Play();
            }
            catch (InvalidOperationException)
            {
                ErrorsLabel.Text = "Error: Folder does not contain any audio";
            }
        }

        private void PreviousTrack_Click(object sender, EventArgs e)
        {
            waveOut.Stop();
            nextButtonClicked = true;
            SetSongToPlay(--playingSongNumber);

            try
            {
                waveOut.Play();
            }
            catch (InvalidOperationException)
            {
                ErrorsLabel.Text = "Error: Folder does not contain any audio";
            }
        }

        private void VolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;

            volume = trackBar.Value;
            waveOut.Volume = volume / 100f;
            VolumeIndicator.Text = $"Volume: {trackBar.Value}";
        }

        private void ChooseMusicFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                try
                {
                    waveOut.Dispose();
                    if (mp3Reader != null) mp3Reader.Dispose();
                    if (wavReader != null) wavReader.Dispose();

                    audioPathsToPlay.Clear();

                    foreach (string path in Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                    {
                        if (AudioExtensions.Contains(Path.GetExtension(path)))
                        {
                            audioPathsToPlay.Add(path);
                        }
                    }
                    ErrorsLabel.Text = "";

                    ShuffleSongs(playMode);
                    waveOut.Stop();
                }
                catch (NullReferenceException)
                {
                    ErrorsLabel.Text = "Error: Folder is empty";
                }
                catch (ArgumentException)
                {
                    ErrorsLabel.Text = "Error: Path is incorrect";
                }

                label2.Text = "";
            }

        }

        private void PlayAndPause_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Pause();

                button.Text = playSymbol;
            }
            else if (waveOut.PlaybackState == PlaybackState.Paused)
            {
                waveOut.Play();

                button.Text = pauseSymbol;
            }
            else if (waveOut.PlaybackState == PlaybackState.Stopped)
            {
                try
                {
                    SetSongToPlay(playingSongNumber);
                    waveOut.Play();
                }
                catch { }

                button.Text = pauseSymbol;
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (audioPathsToPlay.Count - 1 <= playingSongNumber && playMode == PlayMode.None || playMode == PlayMode.LoopAndShuffle)
            {
                return;
            }

            if (playMode == PlayMode.Loop || playMode == PlayMode.LoopAndShuffle)
            {
                SetSongToPlay(playingSongNumber);
            }

            else if (!nextButtonClicked)
            {
                SetSongToPlay(++playingSongNumber);
            }

            nextButtonClicked = false;

            waveOut.Play();
        }

        private void PlaylistEditor_Click(object sender, EventArgs e)
        {
            PlaylistEditor playlistEditorForm = new PlaylistEditor(currentPlaylist, this);
            playlistEditorForm.Show();
        }

        private void ScrollTimeline(object sender, EventArgs e)
        {
            if (waveOut.PlaybackState == PlaybackState.Playing)
            {
                if (mp3Reader != null)
                {
                    mp3Reader.CurrentTime = TimeSpan.FromSeconds(trackBar1.Value);
                }
                else if (wavReader != null)
                {
                    wavReader.CurrentTime = TimeSpan.FromSeconds(trackBar1.Value);
                }
            }
        }

        private void SongTimer(object sender, EventArgs e)
        {
            if (waveOut.PlaybackState == PlaybackState.Playing)
            {
                trackBar1.Enabled = true;

                if (mp3Reader != null)
                {
                    int secondsPassed = (int)double.Parse(mp3Reader.CurrentTime.TotalSeconds.ToString());
                    if (secondsPassed < trackBar1.Maximum)
                    {
                        trackBar1.Value = secondsPassed;
                        label1.Text = mp3Reader.CurrentTime.ToString("mm\\:ss");
                    }
                }
                else if (wavReader != null)
                {
                    int secondsPassed = (int)double.Parse(wavReader.CurrentTime.TotalSeconds.ToString());
                    if (secondsPassed < trackBar1.Maximum)
                    {
                        trackBar1.Value = secondsPassed;
                        label1.Text = wavReader.CurrentTime.ToString("mm\\:ss");
                    }
                }
            }
            else
            {
                trackBar1.Enabled = false;
            }
        }

        private void CreateNewPlaylist(object sender, EventArgs e)
        {
            PlaylistCreation playlistCreationForm = new PlaylistCreation(button6);
            playlistCreationForm.Show();
        }

        private void SelectPlaylist(object sender, EventArgs e)
        {
            waveOut.Pause();

            button4.Text = playSymbol;

            PlaylistSelector playlistSelector = new PlaylistSelector(this);
            playlistSelector.Show();
        }

        private void ObsFileChangePath_Clicked(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog2.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                obsFilePath = folderBrowserDialog2.SelectedPath;
            }
        }
    }
}
