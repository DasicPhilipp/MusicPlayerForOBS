using Newtonsoft.Json;
using System;

namespace MusicPlayerForOBS
{
    [Serializable]
    public class AppData
    {
        [JsonIgnore] public string Version = "0.1.2";
        public string ObsFilePath;
        public int Volume;

        [JsonIgnore] public const string playlistsFolder = "data\\playlists";
        [JsonIgnore] public const string settingsFolder = "data\\settings";
    }
}
