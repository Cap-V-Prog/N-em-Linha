using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace NemLinha_Projeto
{
    public class MusicPlayerManager
    {
        // Dictionary to store menu names and their corresponding music URLs
        private readonly Dictionary<string, string> _musicUrls = new Dictionary<string, string>();

        // Folder to cache downloaded music files
        private const string CacheFolder = "MusicCache";
        
        // NAudio objects for playing audio
        private IWavePlayer _backgroundWaveOutEvent;
        private DynamicVolumeSampleProvider _dynamicVolumeSampleProvider;
        private float _backgroundMusicVolume; // Default volume is 100%
        
        // Constructor to ensure the cache folder exists
        public MusicPlayerManager(float backgroundMusicDefaultVolume)
        {
            // Ensure the cache folder exists
            _backgroundMusicVolume = backgroundMusicDefaultVolume;
            Directory.CreateDirectory(CacheFolder);
        }
        
        // Add a menu and its associated music URL to the dictionary
        public void AddMusic(string menuName, string musicUrl)
        {
            _musicUrls[menuName] = musicUrl;
        }
        
        // Play the music for a specific menu
        public void PlayMusic(string menuName)
        {
            // Stop the currently playing music before starting a new one
            StopMusic();

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

            _backgroundWaveOutEvent = new WaveOutEvent();
            var originalSampleProvider = GetSampleProvider(localFilePath);
            _dynamicVolumeSampleProvider = new DynamicVolumeSampleProvider(originalSampleProvider);
            _dynamicVolumeSampleProvider.Volume = _backgroundMusicVolume;
            
            if (_dynamicVolumeSampleProvider != null)
            {
                _backgroundWaveOutEvent.Init(_dynamicVolumeSampleProvider);
                _backgroundWaveOutEvent.Play();
            }
            else
            {
                Console.WriteLine($"Failed to create SampleProvider for SFX file: {localFilePath}");
            }
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
        
        private ISampleProvider GetSampleProvider(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            switch (fileExtension.ToLower())
            {
                case ".mp3":
                    return new SampleChannel(new Mp3FileReader(filePath));
                case ".wav":
                    return new SampleChannel(new WaveFileReader(filePath));
                // Add more cases for other supported file formats if needed
                default:
                    Console.WriteLine($"Unsupported file format: {fileExtension}");
                    return null;
            }
        }
        
        // Set the volume for background music without playing
        public void SetBackgroundMusicVolume(float volume)
        {
            if (_dynamicVolumeSampleProvider != null)
            {
                _dynamicVolumeSampleProvider.Volume = volume;
                _backgroundMusicVolume = volume;
            }
            else
            {
                Console.WriteLine("No music is currently playing.");
            }
        }
        
        // Stop playing the currently playing background music
        public void StopMusic()
        {
            _backgroundWaveOutEvent?.Stop();
            _backgroundWaveOutEvent?.Dispose();
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
    }
}