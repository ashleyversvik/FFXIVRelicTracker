using FFXIVRelicTracker._02_ARR.ArrHelpers;
using FFXIVRelicTracker._03_HW.HWHelpers;
using FFXIVRelicTracker._04_SB.SBHelpers;
using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers;
using FFXIVRelicTracker._06_EW.EWHelpers;
using Newtonsoft.Json.Linq;
using System;

namespace FFXIVRelicTracker.Helpers
{
    public static class MigrateCharacterData
    {
        public static object ARRInfo { get; private set; }

        public static string TryMigrate(string content)
        {
            var data = JArray.Parse(content);
            var version = data.SelectToken("$[0].version")?.Value<string>();
            if (version != CharactersManager.CurrentCharacterVersion)
            {
                content = ConvertProgressData(content);
            }
            return content;
        }

        public static string ConvertProgressData(string content)
        {
            var data = JArray.Parse(content);

            foreach (var characterToken in data)
            {
                var character = characterToken as JObject;
                ConvertArrProgressData(character);
                ConvertHWProgressData(character);
                ConvertSBProgressData(character);
                ConvertSkysteelProgressData(character);
                ConvertShBProgressData(character);
                ConvertEWProgressData(character);
                character["version"] = CharactersManager.CurrentCharacterVersion;
            }

            return data.ToString();
        }

        private static void ConvertArrProgressData(JObject character)
        {
            var shbModel = character.GetValue("ARRModel", StringComparison.OrdinalIgnoreCase) as JObject;
            if (shbModel != null)
            {
                foreach (string Job in ArrInfo.JobListString)
                {
                    var job = shbModel.GetValue(Job, StringComparison.OrdinalIgnoreCase) as JObject;
                    if (job == null) continue;
                    foreach (string Stage in ArrInfo.StageListString)
                    {
                        var stage = job.GetValue(Stage, StringComparison.OrdinalIgnoreCase);
                        if (stage == null) continue;
                        if (stage is JObject)
                        {
                            var value = (stage as JObject)?.GetValue("Progress", StringComparison.OrdinalIgnoreCase)?.Value<int>();
                            stage.Replace(value == 2);
                        }
                    }
                }
            }
        }

        private static void ConvertHWProgressData(JObject character)
        {
            var shbModel = character.GetValue("HWModel", StringComparison.OrdinalIgnoreCase) as JObject;
            if (shbModel != null)
            {
                foreach (string Job in HWInfo.JobListString)
                {
                    var job = shbModel.GetValue(Job, StringComparison.OrdinalIgnoreCase) as JObject;
                    if (job == null) continue;
                    foreach (string Stage in HWInfo.StageListString)
                    {
                        var stage = job.GetValue(Stage, StringComparison.OrdinalIgnoreCase);
                        if (stage == null) continue;
                        if (stage is JObject)
                        {
                            var value = (stage as JObject)?.GetValue("Progress", StringComparison.OrdinalIgnoreCase)?.Value<int>();
                            stage.Replace(value == 2);
                        }
                    }
                }
            }
        }

        private static void ConvertSBProgressData(JObject character)
        {
            var shbModel = character.GetValue("SBModel", StringComparison.OrdinalIgnoreCase) as JObject;
            if (shbModel != null)
            {
                foreach (string Job in SBInfo.JobListString)
                {
                    var job = shbModel.GetValue(Job, StringComparison.OrdinalIgnoreCase) as JObject;
                    if (job == null) continue;
                    foreach (string Stage in SBInfo.StageListString)
                    {
                        var stage = job.GetValue(Stage, StringComparison.OrdinalIgnoreCase);
                        if (stage == null) continue;
                        if (stage is JObject)
                        {
                            var value = (stage as JObject)?.GetValue("Progress", StringComparison.OrdinalIgnoreCase)?.Value<int>();
                            stage.Replace(value == 2);
                        }
                    }
                }
            }
        }

        private static void ConvertSkysteelProgressData(JObject character)
        {
            var shbModel = character.GetValue("SkysteelModel", StringComparison.OrdinalIgnoreCase) as JObject;
            if (shbModel != null)
            {
                foreach (string Job in SkysteelInfo.JobListString)
                {
                    var job = shbModel.GetValue(Job, StringComparison.OrdinalIgnoreCase) as JObject;
                    if (job == null) continue;
                    foreach (string Stage in SkysteelInfo.StageListString)
                    {
                        var stage = job.GetValue(Stage, StringComparison.OrdinalIgnoreCase);
                        if (stage == null) continue;
                        if (stage is JObject)
                        {
                            var value = (stage as JObject)?.GetValue("Progress", StringComparison.OrdinalIgnoreCase)?.Value<int>();
                            stage.Replace(value == 2);
                        }
                    }
                }
            }
        }

        private static void ConvertShBProgressData(JObject character)
        {
            var shbModel = character.GetValue("ShBModel", StringComparison.OrdinalIgnoreCase) as JObject;
            if (shbModel != null)
            {
                foreach (string Job in ShBInfo.JobListString)
                {
                    var job = shbModel.GetValue(Job, StringComparison.OrdinalIgnoreCase) as JObject;
                    if (job == null) continue;
                    foreach (string Stage in ShBInfo.StageListString)
                    {
                        var stage = job.GetValue(Stage, StringComparison.OrdinalIgnoreCase);
                        if (stage == null) continue;
                        if (stage is JObject)
                        {
                            var value = (stage as JObject)?.GetValue("Progress", StringComparison.OrdinalIgnoreCase)?.Value<int>();
                            stage.Replace(value == 2);
                        }
                    }
                }
            }
        }

        private static void ConvertEWProgressData(JObject character)
        {
            var ewModel = character.GetValue("EWModel", StringComparison.OrdinalIgnoreCase) as JObject;
            if (ewModel != null)
            {
                foreach (string Job in EWInfo.JobListString)
                {
                    var job = ewModel.GetValue(Job, StringComparison.OrdinalIgnoreCase) as JObject;
                    if (job == null) continue;
                    foreach (string Stage in EWInfo.StageListString)
                    {
                        var stage = job.GetValue(Stage, StringComparison.OrdinalIgnoreCase);
                        if (stage == null) continue;
                        if (stage is JObject)
                        {
                            var value = (stage as JObject)?.GetValue("Progress", StringComparison.OrdinalIgnoreCase)?.Value<int>();
                            stage.Replace(value == 2);
                        }
                    }
                }
            }
        }
    }
}
