using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace FFXIVRelicTracker
{
    public static class CharactersManager
    {
        private const string Filename = "characters.json";

        public static IEnumerable<Character> Load()
        {
            var path = Path.Combine(GetLocalDirectory(), Filename);
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                var characters = JsonSerializer.Deserialize<List<Character>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });
                return characters;
            }
            else
            {
                return new List<Character>();
            }
        }

        public static bool CanLoad()
        {
            var path = Path.Combine(GetLocalDirectory(), Filename);
            return File.Exists(path);
        }

        public static void Save(IEnumerable<Character> characters)
        {
            var directoryPath = GetLocalDirectory();
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var json = JsonSerializer.Serialize(characters, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
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
