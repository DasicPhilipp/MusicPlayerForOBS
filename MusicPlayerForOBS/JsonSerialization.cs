using Newtonsoft.Json;
using System;
using System.IO;

namespace MusicPlayerForOBS
{
    public static class JsonSerialization
    {
        public static async void SerializeAsync<T>(T ob, string path, Formatting format = Formatting.Indented)
        {
            string json = JsonConvert.SerializeObject(ob, format);

            using (StreamWriter file = new StreamWriter(path, false))
            {
                await file.WriteAsync(json);
            }
        }

        public static T Deserialize<T> (string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                throw new NullReferenceException("Error: File is empty or does not exist");
            }
        }
    }
}
