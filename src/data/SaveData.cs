using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace YTPPlusPlusPlus
{
    /// <summary>
    /// This class handles data saving and loading.
    /// All data stored here are default values.
    /// </summary>
    public static class SaveData
    {
        public static Dictionary<string, string> saveValues = new Dictionary<string, string>()
        {
            {"ScreenWidth", "320"},
            {"ScreenHeight", "240"},
            {"ScreenScale", "2"}, // 1 - 4 ONLY!
            {"BackgroundSaturation", "0"},
            {"ProjectTitle", "Result"},
            {"MinStreamDuration", "0.2"},
            {"MaxStreamDuration", "0.4"},
            {"MaxClipCount", "20"},
            {"VideoWidth", "640"},
            {"VideoHeight", "480"},
            {"TransitionsEnabled", "false"},
            {"IntrosEnabled", "false"},
            {"OutrosEnabled", "false"},
            {"OverlaysEnabled", "false"},
            {"AddToLibrary", "true"},
            {"AprilFoolsFlappyBirdScore", "0"},
            {"TennisScore", "0"},
            {"MusicVolume", "100"},
            {"SoundEffectVolume", "100"},
            {"ActiveMusic", "1"},
            {"ShuffleMusic", "true"},
            {"TransitionChance", "20"},
            {"ImageChance", "20"},
            {"OverlayChance", "20"},
            {"EffectChance", "60"},
            {"FirstBoot", "true"},
            {"FirstBootVersion", Global.productVersion},
            {"TennisMode", "false"},
            {"TransitionEffects", "false"},
            {"TransitionEffectChance", "30"}
        };
        public static string saveFileName = "Options.json";
        public static bool Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(saveValues, Formatting.Indented);
                File.WriteAllText(saveFileName, json);
                return true;
            }
            catch(Exception e)
            {
                ConsoleOutput.WriteLine(e.Message, Color.Red);
                return false;
            }
        }
        public static bool Load()
        {
            try
            {
                if (!File.Exists(saveFileName))
                {
                    ConsoleOutput.WriteLine("Save file not found. Creating new one.", Color.Yellow);
                    Save();
                }
                string json = File.ReadAllText(saveFileName);
                Dictionary<string, string>? loadedValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                if (loadedValues == null)
                {
                    ConsoleOutput.WriteLine("Save file is corrupted.", Color.Red);
                    loadedValues = new Dictionary<string, string>();
                }
                // Merge loaded values into save values.
                foreach (KeyValuePair<string, string> pair in saveValues)
                {
                    if (loadedValues.ContainsKey(pair.Key))
                    {
                        if (loadedValues[pair.Key] != pair.Value)
                        {
                            saveValues[pair.Key] = loadedValues[pair.Key];
                        }
                    }
                    else
                    {
                        saveValues[pair.Key] = pair.Value;
                    }
                }
                // Save the new values.
                Save();
                return true;
            }
            catch(Exception e)
            {
                ConsoleOutput.WriteLine(e.Message);
                return false;
            }
        }
    }
}
