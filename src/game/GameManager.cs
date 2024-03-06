using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace NemLinha_Projeto
{
    public class GameManager
    {
        private const string SettingsFolderPath = "data";
        private const string SettingsFileName = "games.json";
        private static readonly string FilePath = Path.Combine(SettingsFolderPath, SettingsFileName);

        public static void SaveGames(List<Game> games)
        {
            string json = JsonConvert.SerializeObject(games, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public static List<Game> LoadGames()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Game>>(json);
            }

            return new List<Game>();
        }

        public static bool AddGame(Game newGame)
        {
            List<Game> allGames = LoadGames();

            if (allGames.Exists(game => game.GameId == newGame.GameId))
            {
                return false;
            }

            int highestId = allGames.Count > 0 ? allGames.Max(game => game.GameId) : 0;
            newGame.GameId = highestId + 1;

            allGames.Add(newGame);
            SaveGames(allGames);

            return true;
        }

        public static Game LoadGameById(int gameId)
        {
            List<Game> allGames = LoadGames();
            return allGames.Find(game => game.GameId == gameId);
        }

        // New method to update and save a game
        public static bool UpdateGame(Game updatedGame)
        {
            List<Game> allGames = LoadGames();

            // Find the index of the game to update
            int index = allGames.FindIndex(game => game.GameId == updatedGame.GameId);
            
            if (index != -1)
            {
                
                updatedGame.LastModified=DateTime.Now;
                
                // Update the game and save the changes
                allGames[index] = updatedGame;
                SaveGames(allGames);
                return true;
            }

            return false;
        }
        
        public static void ResetBoard(int gameId)
        {
            List<Game> allGames = LoadGames();
            Game gameToReset = allGames.Find(game => game.GameId == gameId);

            if (gameToReset != null)
            {
                gameToReset.ResetBoard();
                gameToReset.LastModified = DateTime.Now;
                SaveGames(allGames);
            }
        }
    }
}
