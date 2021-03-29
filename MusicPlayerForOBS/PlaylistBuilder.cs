using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    public static class PlaylistBuilder
    {
        public static void ChooseMusicFolder(FolderBrowserDialog folderBrowserDialog, ComboBox comboBox, Playlist playlist, bool updateFolders)
        {
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                foreach (string path in Directory.GetFiles(folderBrowserDialog.SelectedPath))
                {
                    if (MainForm.AudioExtensions.Contains(Path.GetExtension(path)))
                    {
                        playlist.AddSong(path, updateFolders);
                    }
                }

                if (updateFolders)
                {
                    playlist.SetFolderToUpdate(folderBrowserDialog.SelectedPath);
                }

                comboBox.Items.Clear();
                comboBox.Items.AddRange(playlist.GetSongsKeys());
            }
        }

        public static void ChooseMusicFile(OpenFileDialog openFileDialog, ComboBox comboBox, Playlist playlist)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                foreach (string path in openFileDialog.FileNames)
                {
                    if (MainForm.AudioExtensions.Contains(Path.GetExtension(path)))
                    {
                        playlist.AddSong(path);
                    }
                }

                comboBox.Items.Clear();
                comboBox.Items.AddRange(playlist.GetSongsKeys());
            }
        }

        public static bool PlaylistNameChange(TextBox textBox, Label errorLabel, Playlist playlist)
        {
            string[] filesPaths = Directory.GetFiles(AppData.playlistsFolder, $"*{Playlist.extension}");
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
            if (string.IsNullOrEmpty(textBox.Text) || textBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) > 0)
            {
                errorLabel.Text = "File name is incorrect";
                errorLabel.Visible = true;
            } else
            {
                playlist.Name = textBox.Text;

                errorLabel.Visible = false;
            }
            //else
            //{
            //    textBox.Text = "";

            //    errorLabel.Text = "This playlist already exists";
            //    errorLabel.Visible = true;
            //}

            return !errorLabel.Visible;
        }

        public static void DeleteSongComboBox(ComboBox comboBox, Playlist playlist)
        {
            playlist.TryDeleteSongByKey(comboBox.Items[comboBox.SelectedIndex] as string);
            comboBox.Items.Clear();
            comboBox.Items.AddRange(playlist.GetSongsKeys());
        }

        public static void DeleteSongsFromFolder(FolderBrowserDialog folderBrowserDialog, ComboBox comboBox, Playlist playlist)
        {
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                foreach (string path in Directory.GetFiles(folderBrowserDialog.SelectedPath))
                {
                    if (MainForm.AudioExtensions.Contains(Path.GetExtension(path)))
                    {
                        playlist.TryDeleteSongByPath(path);
                    }
                }

                comboBox.Items.Clear();
                comboBox.Items.AddRange(playlist.GetSongsKeys());
            }
        }

        public static void UpdateFolders(Playlist playlist, bool update)
        {
            playlist.AutomaticallyAddSongsFromFolders = update;
        }
    }
}
