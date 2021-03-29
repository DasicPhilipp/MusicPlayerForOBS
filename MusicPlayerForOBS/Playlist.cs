using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicPlayerForOBS
{
    [Serializable]
    public class Playlist
    {
        public const string Extension = ".pl";

        public string Name;

        [Newtonsoft.Json.JsonProperty] private readonly Dictionary<string, string> Songs;
        [Newtonsoft.Json.JsonProperty] private List<string> RefreshingFolders;

        public Playlist(string playlistName)
        {
            Name = playlistName;
            Songs = new Dictionary<string, string>();

            CheckPathsActuality();
        }

        public void AddSong(string songPath)
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

        public void AddSong(string[] songsPaths)
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
                JsonSerialization.SerializeAsync(this, AppData.PlaylistsFolder + Name + Extension);
            }

            if (RefreshingFolders != null)
            {
                foreach (string folderPath in RefreshingFolders.ToArray())
                {
                    if (!Directory.Exists(folderPath))
                    {
                        RefreshingFolders.Remove(folderPath);
                    }
                    else
                    {
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
            }

            return brokenPaths.ToArray();
        }

        public void SetRefreshingFolder(string path)
        {
            if (RefreshingFolders == null)
            {
                RefreshingFolders = new List<string>();
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

            if (!RefreshingFolders.Contains(folderPath))
            {
                RefreshingFolders.Add(folderPath);
            }
        }

        public void RemoveRefreshingFolder(string path)
        {
            if (RefreshingFolders.Contains(path))
            {
                RefreshingFolders.Remove(path);
            }
        }

        public string[] GetRefreshingFolders()
        {
            return RefreshingFolders.ToArray();
        }
    }
}
