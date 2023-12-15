using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NemLinha_Projeto
{
    public class LanguageManager
    {
        private readonly object _lockObject = new object();
        private Dictionary<string, string> _currentLanguageTranslations;
        private string _currentLanguage;
        private readonly string _fallbackLanguage = "en";

        public LanguageManager(string defaultLanguage)
        {
            _currentLanguage = defaultLanguage;
            LoadTranslations();
        }

        private void LoadTranslations()
        {
            string filePath = $"lang\\{_currentLanguage}.json";

            try
            {
                string json = File.ReadAllText(filePath);
                _currentLanguageTranslations = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Warning: Translation file '{filePath}' not found. Defaulting to fallback language.");
                ChangeLanguage(_fallbackLanguage); // Fallback to the default language
            }
            catch (JsonException jsonException)
            {
                Console.WriteLine($"Error loading translations from '{filePath}': {jsonException.Message}");
                _currentLanguageTranslations = new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error loading translations: {ex.Message}");
                _currentLanguageTranslations = new Dictionary<string, string>();
            }
        }

        public string Translate(string key, params object[] args)
        {
            lock (_lockObject)
            {
                if (_currentLanguageTranslations.TryGetValue(key, out var translation))
                {
                    return string.Format(translation, args);
                }

                // Fallback to key if translation is not found
                return key;
            }
        }

        public int GetLanguageIndex(string languageCode)
        {
            // Helper method to get the index of the specified language code in the languageOptions array
            switch (languageCode)
            {
                case "en": return 0;
                case "pt": return 1;
                case "es": return 2;
                case "it": return 3;
                case "d": return 4;
                case "fr": return 5;
                default: return -1; // Handle the default case or return an appropriate default index
            }
        }

        public void ChangeLanguage(string newLanguage)
        {
            lock (_lockObject)
            {
                if (_currentLanguage.Equals(newLanguage, StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nLanguage is already set to {_currentLanguage}");
                    Console.ResetColor();
                    return; // Skip loading if the language is the same
                }

                try
                {
                    _currentLanguage = newLanguage;
                    LoadTranslations();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\nLanguage changed to {_currentLanguage}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nError changing language: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }
    }
}

