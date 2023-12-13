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

        public void ChangeLanguage(string newLanguage)
        {
            lock (_lockObject)
            {
                _currentLanguage = newLanguage;
                LoadTranslations();
            }
        }
    }
}

