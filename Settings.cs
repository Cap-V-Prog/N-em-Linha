using System;
using System.IO;
using Newtonsoft.Json;

namespace NemLinha_Projeto
{
    public class Settings
    {
        // Properties for user preferences
        public string Language { get; set; }
        // Add other properties for settings as needed

        // Static property to hold the loaded settings
        private static Settings cachedSettings;

        // Save settings to a JSON file
        public void SaveSettings()
        {
            string settingsFilePath = "settings.json";

            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(settingsFilePath, json);

                // Update the cached settings after saving
                cachedSettings = this;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving settings: {ex.Message}");
            }
        }

        // Load settings from a JSON file
        public static Settings LoadSettings()
        {
            // If settings are already loaded, return the cached settings
            if (cachedSettings != null)
            {
                return cachedSettings;
            }

            string settingsFilePath = "settings.json";

            try
            {
                if (File.Exists(settingsFilePath))
                {
                    string json = File.ReadAllText(settingsFilePath);
                    cachedSettings = JsonConvert.DeserializeObject<Settings>(json);
                }
                else
                {
                    // If settings file doesn't exist, create a new instance with defaults
                    cachedSettings = new Settings();
                    cachedSettings.SaveSettings(); // Save the defaults to file
                }

                return cachedSettings;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}");
                return new Settings(); // Return a new instance if there's an error
            }
        }

        // Clear the cached settings (force a reload)
        public static void ClearCachedSettings()
        {
            cachedSettings = null;
        }
    }
}
