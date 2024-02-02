using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace NemLinha_Projeto
{
    public class SfxPlayerManager : IDisposable
    {
        private readonly Dictionary<string, string> _sfxUrls = new Dictionary<string, string>();
        private const string CacheFolder = "SfxCache";
        private IWavePlayer _sfxWaveOutEvent;
        private DynamicVolumeSampleProvider _dynamicVolumeSampleProvider;
        private float _sfxVolume = 1f;

        public SfxPlayerManager(float sfxDefaultVolume)
        {
            _sfxVolume = sfxDefaultVolume;
            Directory.CreateDirectory(CacheFolder);
        }

        public void AddSfx(string sfxName, string sfxUrl)
        {
            _sfxUrls[sfxName] = sfxUrl;
        }
        
        public void DownloadSfx(string sfxName)
        {
            if (!_sfxUrls.ContainsKey(sfxName))
            {
                Console.WriteLine($"No music URL found for menu: {sfxName}");
                return;
            }

            string localFilePath = GetLocalFilePath(sfxName);

            if (!File.Exists(localFilePath))
            {
                using (WebClient webClient = new WebClient())
                {
                    Uri sfxUri = new Uri(_sfxUrls[sfxName]);

                    try
                    {
                        // Adjust file extension based on the original extension of the downloaded file
                        string originalExtension = Path.GetExtension(sfxUri.LocalPath);
                        localFilePath = Path.ChangeExtension(localFilePath, originalExtension);

                        webClient.DownloadFile(sfxUri, localFilePath);
                    }
                    catch (WebException ex)
                    {
                        // Handle web exceptions
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error downloading music for menu {sfxName}: {ex.Message}");
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
                        Console.WriteLine($"Error downloading music for menu {sfxName}: {ex.Message}");
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


        public void PlaySoundEffect(string sfxName)
        {
            if (!_sfxUrls.ContainsKey(sfxName))
            {
                Console.WriteLine($"No music URL found for menu: {sfxName}");
                return;
            }

            string localFilePath = GetLocalFilePath(sfxName);

            if (!File.Exists(localFilePath))
            {
                Console.WriteLine($"SFX file not found for menu: {sfxName}. Please download it first.");
                return;
            }

            StopSfx();

            _sfxWaveOutEvent = new WaveOutEvent();
            var originalSampleProvider = GetSampleProvider(localFilePath);
            _dynamicVolumeSampleProvider = new DynamicVolumeSampleProvider(originalSampleProvider);
            _dynamicVolumeSampleProvider.Volume = _sfxVolume;
            if (_dynamicVolumeSampleProvider != null)
            {
                _sfxWaveOutEvent.Init(_dynamicVolumeSampleProvider);
                _sfxWaveOutEvent.Play();
            }
            else
            {
                Console.WriteLine($"Failed to create SampleProvider for SFX file: {localFilePath}");
            }
        }

        public void SetSFXVolume(float volume)
        {
            if (_dynamicVolumeSampleProvider != null)
            {
                _sfxVolume = volume;
            }
            else
            {
                Console.WriteLine("No sound effect is currently playing.");
            }
            
        }

        public void StopSfx()
        {
            _sfxWaveOutEvent?.Stop();
            _sfxWaveOutEvent?.Dispose();
            
            _dynamicVolumeSampleProvider = null;  // Set to null after disposing
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

        private string GetLocalFilePath(string sfxName)
        {
            if (!_sfxUrls.ContainsKey(sfxName))
            {
                Console.WriteLine($"No music URL found for menu: {sfxName}");
                return null;
            }

            Uri musicUri = new Uri(_sfxUrls[sfxName]);
            string originalExtension = Path.GetExtension(musicUri.LocalPath);
            string fileNameWithoutQuery = Path.GetFileNameWithoutExtension(musicUri.LocalPath);

            return Path.Combine(CacheFolder, $"{fileNameWithoutQuery}_background_music{originalExtension}");
        }

        public void Dispose()
        {
            StopSfx();
        }
    }
}
