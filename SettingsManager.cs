using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace FFXIVRelicTracker
{
    public static class SettingsManager
    {
        private const string Filename = "settings.json";

        public static Settings Settings { get; private set; }

        public static void Load()
        {
            var path = Path.Combine(GetLocalDirectory(), Filename);
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                var settings = JsonSerializer.Deserialize<Settings>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });
                Settings = settings;
            } 
            else
            {
                Settings = new Settings { FontSize = 12 };
            }
        }

        public static bool CanLoad()
        {
            var path = Path.Combine(GetLocalDirectory(), Filename);
            return File.Exists(path);
        }

        public static void Save()
        {
            var directoryPath = GetLocalDirectory();
            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            File.WriteAllText(Path.Combine(directoryPath, Filename), json);
        }

        private static string GetLocalDirectory()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appName = Assembly.GetEntryAssembly().GetName().Name;
            return Path.Combine(appData, appName);
        }
    }
}
