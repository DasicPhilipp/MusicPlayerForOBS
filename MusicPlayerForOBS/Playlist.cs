using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicPlayerForOBS
{
    [Serializable]
    public class Playlist
    {
        public const string extension = ".pl";

        public string Name;
        public bool AutomaticallyAddSongsFromFolders;
        [Newtonsoft.Json.JsonProperty] private List<string> FoldersToUpdate;
        [Newtonsoft.Json.JsonProperty] private readonly Dictionary<string, string> Songs;

        public Playlist(string playlistName)
        {
            Name = playlistName;
            Songs = new Dictionary<string, string>();

            CheckPathsActuality();
        }

        public void AddSong(string songPath, bool updateFolders = false)
        {
            CheckPathsActuality();

            if (MainForm.AudioExtensions.Contains(Path.GetExtension(songPath)))
            {
                if (!Songs.ContainsValue(songPath))
                {
                    Songs.Add(Path.GetFileNameWithoutExtension(songPath), songPath);
                }
            }
        }

        public void AddSong(string[] songsPaths, bool updateFolders = false)
        {
            CheckPathsActuality();

            foreach (string songPath in songsPaths)
            {
                if (MainForm.AudioExtensions.Contains(Path.GetExtension(songPath)))
                {
                    if (!Songs.ContainsValue(songPath))
                    {
                        Songs.Add(Path.GetFileNameWithoutExtension(songPath), songPath);
                    }
                        
                }
            }
        }

        public bool TryDeleteSongByPath(string songPath)
        {
            CheckPathsActuality();

            if (Songs.ContainsValue(songPath))
            {
                Songs.Remove(Path.GetFileNameWithoutExtension(songPath));

                return true;
            }
            else
                return false;
        }

        public bool TryDeleteSongByKey(string key)
        {
            CheckPathsActuality();

            if (Songs.ContainsKey(key))
            {
                Songs.Remove(key);

                return true;
            }
            else
                return false;
        }

        public bool TryGetSongPathByKey(string key, out string songPath)
        {
            CheckPathsActuality();

            return Songs.TryGetValue(key, out songPath);
        }

        public string[] GetSongsKeys()
        {
            CheckPathsActuality();

            return Songs.Keys.ToArray();
        }

        public string[] GetSongsValues()
        {
            CheckPathsActuality();

            return Songs.Values.ToArray();
        }

        public string[] CheckPathsActuality()
        {
            int brokenPathsCount = 0;
            List<string> brokenPaths = new List<string>();
            foreach (string path in Songs.Values.ToArray())
            {
                if (!File.Exists(path))
                {
                    Songs.Remove(Path.GetFileNameWithoutExtension(path));
                    brokenPaths.Add(path);
                    brokenPathsCount++;
                }
            }

            if (brokenPathsCount != 0)
            {
                JsonSerialization.SerializeAsync(this, AppData.playlistsFolder, Name + extension);
            }

            if (AutomaticallyAddSongsFromFolders)
            {
                if (FoldersToUpdate == null)
                {
                    FoldersToUpdate = new List<string>();
                }

                foreach (string folderPath in FoldersToUpdate.ToArray())
                {
                    if (!Directory.Exists(folderPath))
                    {
                        FoldersToUpdate.Remove(folderPath);
                    }

                    foreach (string path in Directory.GetFiles(folderPath))
                    {
                        if (MainForm.AudioExtensions.Contains(Path.GetExtension(path)))
                        {
                            if (!Songs.ContainsValue(path))
                            {
                                Songs.Add(Path.GetFileNameWithoutExtension(path), path);
                            }
                        }
                    }
                }
            }

            return brokenPaths.ToArray();
        }

        public void SetFolderToUpdate(string path)
        {
            if (FoldersToUpdate == null)
            {
                FoldersToUpdate = new List<string>();
            }

            string folderPath;
            if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                folderPath = Path.GetDirectoryName(path);
            }
            else
            {
                folderPath = path;
            }

            if (!FoldersToUpdate.Contains(folderPath))
            {
                FoldersToUpdate.Add(folderPath);
            }
        }
    }
}
