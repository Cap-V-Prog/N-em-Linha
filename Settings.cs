using System;
using System.IO;
using Newtonsoft.Json;

namespace NemLinha_Projeto
{
    public class Settings
    {
        // Properties for user preferences
        public string Language { get; set; }
        
        // Constant for the file path
        private const string SettingsFolderPath = "data";
        private const string SettingsFileName = "settings.json";
        private static readonly string SettingsFilePath = Path.Combine(SettingsFolderPath, SettingsFileName);
        
        // Default language
        private const string DefaultLanguage = "en";

        // Static property to hold the loaded settings
        private static Settings _cachedSettings;

        // Save settings to a JSON file
        public void SaveSettings()
        {
            try
            {
                if (!Directory.Exists(SettingsFolderPath))
                {
                    Directory.CreateDirectory(SettingsFolderPath);
                }

                // Set default language if not already set
                if (string.IsNullOrEmpty(Language))
                {
                    Language = DefaultLanguage;
                }

                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(SettingsFilePath, json);

                // Update the cached settings after saving
                _cachedSettings = this;
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
            if (_cachedSettings != null)
            {
                return _cachedSettings;
            }

            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    _cachedSettings = JsonConvert.DeserializeObject<Settings>(json);
                }
                else
                {
                    // If settings file doesn't exist, create a new instance with defaults
                    _cachedSettings = new Settings();

                    // Save the defaults to file
                    _cachedSettings.SaveSettings();
                }

                return _cachedSettings;
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
            _cachedSettings = null;
        }
    }
}
