using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using NAudio.Wave;


namespace NemLinha_Projeto
{
    public class BackgroundMusicPlayer
    {
        private Dictionary<string, string> musicUrls = new Dictionary<string, string>();
        private const string CacheFolder = "MusicCache";

        private WaveOutEvent waveOutEvent;
        private WaveFileReader waveFileReader;

        public BackgroundMusicPlayer()
        {
            // Ensure the cache folder exists
            Directory.CreateDirectory(CacheFolder);
        }

        public void AddMusic(string menuName, string musicUrl)
        {
            musicUrls[menuName] = musicUrl;
        }

        public void DownloadMusicAsync(string menuName)
        {
            if (!musicUrls.ContainsKey(menuName))
            {
                Console.WriteLine($"No music URL found for menu: {menuName}");
                return;
            }

            string localFilePath = GetLocalFilePath(menuName);

            if (!File.Exists(localFilePath))
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFileTaskAsync(new Uri(musicUrls[menuName]), localFilePath);
                }
            }
        }

        public void PlayMusic(string menuName)
        {
            if (!musicUrls.ContainsKey(menuName))
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

            waveOutEvent = new WaveOutEvent();
            waveFileReader = new WaveFileReader(localFilePath);

            waveOutEvent.Init(waveFileReader);
            waveOutEvent.Play();
        }

        public void StopMusic()
        {
            waveOutEvent?.Stop();
            waveOutEvent?.Dispose();

            waveFileReader?.Dispose();
        }

        private string GetLocalFilePath(string menuName)
        {
            return Path.Combine(CacheFolder, $"{menuName}_background_music.wav");
        }
    }
}