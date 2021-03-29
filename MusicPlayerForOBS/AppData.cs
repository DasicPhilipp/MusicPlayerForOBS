using Newtonsoft.Json;
using System;

namespace MusicPlayerForOBS
{
    [Serializable]
    public class AppData
    {
        public const string Version = "0.1.3";

        public string ObsFilePath;
        public int Volume;

        [JsonIgnore] public const string PlaylistsFolder = "data\\playlists\\";
        [JsonIgnore] public const string SettingsFolder = "data\\settings\\";
    }
}
