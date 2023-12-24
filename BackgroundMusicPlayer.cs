using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NAudio.Wave;

namespace NemLinha_Projeto
{
    public class BackgroundMusicPlayer : IDisposable
    {
        // Dictionary to store menu names and their corresponding music URLs
        private readonly Dictionary<string, string> _musicUrls = new Dictionary<string, string>();
        
        // Folder to cache downloaded music files
        private const string CacheFolder = "MusicCache";
        
        // NAudio objects for playing audio
        private IWavePlayer _waveOutEvent;
        private WaveStream _audioFileReader;

        // Constructor to ensure the cache folder exists
        public BackgroundMusicPlayer()
        {
            // Ensure the cache folder exists
            Directory.CreateDirectory(CacheFolder);
        }

        // Add a menu and its associated music URL to the dictionary
        public void AddMusic(string menuName, string musicUrl)
        {
            _musicUrls[menuName] = musicUrl;
        }

        // Download music file for a specific menu
        public void DownloadMusic(string menuName)
        {
            if (!_musicUrls.ContainsKey(menuName))
            {
                Console.WriteLine($"No music URL found for menu: {menuName}");
                return;
            }

            string localFilePath = GetLocalFilePath(menuName);

            if (!File.Exists(localFilePath))
            {
                using (WebClient webClient = new WebClient())
                {
                    Uri musicUri = new Uri(_musicUrls[menuName]);

                    try
                    {
                        // Adjust file extension based on the original extension of the downloaded file
                        string originalExtension = Path.GetExtension(musicUri.LocalPath);
                        localFilePath = Path.ChangeExtension(localFilePath, originalExtension);

                        webClient.DownloadFile(musicUri, localFilePath);
                    }
                    catch (WebException ex)
                    {
                        // Handle web exceptions
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error downloading music for menu {menuName}: {ex.Message}");
                        Console.ResetColor();

                        // Delete the file if it exists
                        if (File.Exists(localFilePath))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Deleting the file: {localFilePath}");
                            Console.ResetColor();
                            File.Delete(localFilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle general exceptions
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error downloading music for menu {menuName}: {ex.Message}");
                        Console.ResetColor();

                        // Delete the file if it exists
                        if (File.Exists(localFilePath))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Deleting the file: {localFilePath}");
                            Console.ResetColor();
                            File.Delete(localFilePath);
                        }
                    }
                }
            }
        }

        // Play the music for a specific menu
        public void PlayMusic(string menuName)
        {
            if (!_musicUrls.ContainsKey(menuName))
            {
                Console.WriteLine($"No music URL found for menu: {menuName}");
                return;
            }

            string localFilePath = GetLocalFilePath(menuName);

            if (!File.Exists(localFilePath))
            {
                Console.WriteLine($"Music file not found for menu: {menuName}. Please download it first.");
                return;
            }

            _waveOutEvent = new WaveOutEvent();

            // Determine the WaveStream based on the file extension
            WaveStream waveStream = GetWaveStream(localFilePath);

            if (waveStream != null)
            {
                _audioFileReader = waveStream;
                _waveOutEvent.Init(_audioFileReader);
                _waveOutEvent.Play();
            }
            else
            {
                Console.WriteLine($"Failed to create WaveStream for file: {localFilePath}");
            }
        }

        // Get the WaveStream based on the file extension
        private WaveStream GetWaveStream(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            switch (fileExtension.ToLower())
            {
                case ".mp3":
                    return new Mp3FileReader(filePath);
                case ".wav":
                    return new WaveFileReader(filePath);
                default:
                    Console.WriteLine($"Unsupported file format: {fileExtension}");
                    return null;
            }
        }

        // Stop playing the currently playing music
        public void StopMusic()
        {
            _waveOutEvent?.Stop();
            _waveOutEvent?.Dispose();

            _audioFileReader?.Dispose();
        }

        // Get the local file path for a menu's background music
        private string GetLocalFilePath(string menuName)
        {
            if (!_musicUrls.ContainsKey(menuName))
            {
                Console.WriteLine($"No music URL found for menu: {menuName}");
                return null;
            }

            Uri musicUri = new Uri(_musicUrls[menuName]);
            string originalExtension = Path.GetExtension(musicUri.LocalPath);

            // Remove query parameters from the file name
            string fileNameWithoutQuery = Path.GetFileNameWithoutExtension(musicUri.LocalPath);

            return Path.Combine(CacheFolder, $"{fileNameWithoutQuery}_background_music{originalExtension}");
        }
        
        // Implement IDisposable to clean up resources
        public void Dispose()
        {
            StopMusic();
        }
    }
}
