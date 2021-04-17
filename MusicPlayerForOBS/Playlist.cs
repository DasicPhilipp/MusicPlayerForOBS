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

        [Newtonsoft.Json.JsonProperty] private readonly Dictionary<string, string> _songs;
        [Newtonsoft.Json.JsonProperty] private List<string> _refreshingFolders;

        public Playlist(string playlistName)
        {
            Name = playlistName;
            _songs = new Dictionary<string, string>();
            _refreshingFolders = new List<string>();

            CheckPathsActuality();
        }

        public void AddSong(string songPath)
        {
            CheckPathsActuality();

            if (MainForm.AudioExtensions.Contains(Path.GetExtension(songPath)))
            {
                if (!_songs.ContainsValue(songPath))
                {
                    _songs.Add(Path.GetFileNameWithoutExtension(songPath), songPath);
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
                    if (!_songs.ContainsValue(songPath))
                    {
                        _songs.Add(Path.GetFileNameWithoutExtension(songPath), songPath);
                    }
                        
                }
            }
        }

        public bool TryDeleteSongByPath(string songPath)
        {
            CheckPathsActuality();

            if (_songs.ContainsValue(songPath))
            {
                _songs.Remove(Path.GetFileNameWithoutExtension(songPath));

                return true;
            }
            else
                return false;
        }

        public bool TryDeleteSongByKey(string key)
        {
            CheckPathsActuality();

            if (_songs.ContainsKey(key))
            {
                _songs.Remove(key);

                return true;
            }
            else
                return false;
        }

        public bool TryGetSongPathByKey(string key, out string songPath)
        {
            CheckPathsActuality();

            return _songs.TryGetValue(key, out songPath);
        }

        public string[] GetSongsKeys()
        {
            CheckPathsActuality();

            return _songs.Keys.ToArray();
        }

        public string[] GetSongsValues()
        {
            CheckPathsActuality();

            return _songs.Values.ToArray();
        }

        public string[] CheckPathsActuality()
        {
            int brokenPathsCount = 0;
            List<string> brokenPaths = new List<string>();
            foreach (string path in _songs.Values.ToArray())
            {
                if (!File.Exists(path))
                {
                    _songs.Remove(Path.GetFileNameWithoutExtension(path));
                    brokenPaths.Add(path);
                    brokenPathsCount++;
                }
            }

            if (brokenPathsCount != 0)
            {
                JsonSerialization.SerializeAsync(this, AppData.PlaylistsFolder + Name + Extension);
            }

            if (_refreshingFolders != null)
            {
                foreach (string folderPath in _refreshingFolders.ToArray())
                {
                    if (!Directory.Exists(folderPath))
                    {
                        _refreshingFolders.Remove(folderPath);
                    }
                    else
                    {
                        foreach (string path in Directory.GetFiles(folderPath))
                        {
                            if (MainForm.AudioExtensions.Contains(Path.GetExtension(path)))
                            {
                                if (!_songs.ContainsValue(path))
                                {
                                    _songs.Add(Path.GetFileNameWithoutExtension(path), path);
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
            string folderPath;

            if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                folderPath = Path.GetDirectoryName(path);
            }
            else
            {
                folderPath = path;
            }

            if (!_refreshingFolders.Contains(folderPath))
            {
                _refreshingFolders.Add(folderPath);
            }
        }

        public void RemoveRefreshingFolder(string path)
        {
            if (_refreshingFolders.Contains(path))
            {
                _refreshingFolders.Remove(path);
            }
        }

        public string[] GetRefreshingFolders()
        {
            return _refreshingFolders.ToArray();
        }
    }
}
