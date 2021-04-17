using Newtonsoft.Json;
using System;
using System.IO;

namespace MusicPlayerForOBS
{
    [Serializable]
    public class AppData
    {
        public const string Version = "0.1.4";

        public string ObsFilePath;
        public int Volume;

        [JsonIgnore] public static readonly string PlaylistsFolder = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData),
            @"MusicPlayerForOBS\data\playlists\");
        [JsonIgnore] public static readonly string SettingsFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            @"MusicPlayerForOBS\data\settings\");
    }
}
