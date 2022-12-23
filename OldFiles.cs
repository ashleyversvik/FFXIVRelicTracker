using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FFXIVRelicTracker
{
    public class OldFiles
    {
        private const string OldSettingsFilename = "FFXIVRelicTrackerSettings.txt";
        private const string OldCharactersFilename = "Characters.txt";
        public static void Convert()
        {
            ConvertOldSettings();
            ConvertOldCharacters();
        }

        private static void ConvertOldSettings()
        {
            if (SettingsManager.CanLoad())
                return;

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appData, OldSettingsFilename);
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                double fontSize;
                try
                {
                    fontSize = double.Parse(content);
                }
                catch
                {
                    fontSize = 12;
                }
                SettingsManager.Settings.FontSize = fontSize;
                SettingsManager.Save();
            }
        }

        private static void ConvertOldCharacters()
        {
            if (CharactersManager.CanLoad())
                return;

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appData, OldCharactersFilename);
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                var characters = JsonSerializer.Deserialize<List<Character>>(content, new JsonSerializerOptions { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });
                var convertedCharacters = new List<Character>();
                foreach (var oldCharacter in characters)
                {
                    var newCharacter = new Character(oldCharacter);
                    convertedCharacters.Add(newCharacter);
                }
                CharactersManager.Save(convertedCharacters);
            }
        }
    }
}
