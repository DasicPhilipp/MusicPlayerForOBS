using Newtonsoft.Json;
using System;
using System.IO;

namespace MusicPlayerForOBS
{
    public static class JsonSerialization
    {
        private static JsonSerializerSettings jsonSettings;
        static JsonSerialization()
        {
            jsonSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public static async void SerializeAsync<T>(T ob, string path, string name, Formatting format = Formatting.Indented)
        {
            string json = JsonConvert.SerializeObject(ob, format, jsonSettings);

            using (StreamWriter file = new StreamWriter(Path.Combine(path, name), false))
            {
                await file.WriteAsync(json);
            }
        }

        public static T Deserialize<T> (string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                return (T)JsonConvert.DeserializeObject(json, jsonSettings);
            }
            else
            {
                throw new NullReferenceException("Error: File is empty or does not exist");
            }
        }
    }
}
