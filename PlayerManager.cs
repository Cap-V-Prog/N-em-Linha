using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NemLinha_Projeto
{
    public class PlayerManager
    {
        // Constant for the file path
        private const string SettingsFolderPath = "data";
        private const string SettingsFileName = "players.json";
        private static readonly string FilePath = Path.Combine(SettingsFolderPath, SettingsFileName);

        // Method to save player data to a JSON file
        public static void SavePlayers(List<Player> players)
        {
            string json = JsonConvert.SerializeObject(players, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        // Method to load players' data from a JSON file
        public static List<Player> LoadPlayers()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Player>>(json);
            }
            else
            {
                return new List<Player>();
            }
        }
        
        public static bool AddPlayer(string playerName)
        {
            List<Player> allPlayers = LoadPlayers();

            // Check if the player with the given username already exists
            if (allPlayers.Exists(player => player.Name == playerName))
            {
                return false;
            }

            // If the username doesn't exist, add the new player
            Player newPlayer = new Player(playerName, 0, 0);
            allPlayers.Add(newPlayer);
            SavePlayers(allPlayers);

            return true;
        }

        public static string AddPlayerStringOut(bool result, string pname, LanguageManager languageManager)
        {
            string key = result ? "add_player_success" : "add_player_exists";
            return languageManager.Translate(key, pname);
        }
        
        public static string UpdatePlayerStats(string playerName, Action<Player> updateAction, LanguageManager languageManager)
        {
            List<Player> allPlayers = LoadPlayers();
            Player currentPlayer = allPlayers.Find(player => player.Name == playerName);

            if (currentPlayer != null)
            {
                updateAction(currentPlayer);

                int index = allPlayers.FindIndex(player => player.Name == playerName);
                if (index != -1)
                {
                    allPlayers[index] = currentPlayer;
                    SavePlayers(allPlayers);
                    return languageManager.Translate("player_updated_success",playerName);
                }
                return languageManager.Translate("player_not_found",playerName);
            }
            return languageManager.Translate("player_not_found",playerName);
        }
        
        public static string[] ListAllPlayerNames()
        {
            List<Player> allPlayers = LoadPlayers();

            // Extract player names into a string array
            string[] playerNames = new string[allPlayers.Count];
            for (int i = 0; i < allPlayers.Count; i++)
            {
                playerNames[i] = allPlayers[i].Name;
            }

            return playerNames;
        }

        public static string DisplayPlayerInfo(string playerName)
        {
            List<Player> allPlayers = LoadPlayers();
            Player currentPlayer = allPlayers.Find(player => player.Name == playerName);
            
            if (currentPlayer != null)
            {
                return currentPlayer.DisplayPlayerInfo();
            }
            return Program.LanguageManager.Translate("player_not_found",playerName);
        }
        
        public static string DeletePlayer(string playerName)
        {
            List<Player> allPlayers = LoadPlayers();
            Player playerToRemove = allPlayers.Find(player => player.Name == playerName);

            if (playerToRemove != null)
            {
                allPlayers.Remove(playerToRemove);
                SavePlayers(allPlayers);
                return Program.LanguageManager.Translate("delete_player_success",playerName);
            }
            return Program.LanguageManager.Translate("player_not_found",playerName);
        }
        
        public static void ClearAllPlayers()
        {
            // Clear all players by overwriting the file with an empty list
            List<Player> allPlayers = LoadPlayers();
            Console.WriteLine();
            if(allPlayers.Count>0)
            {
                Menus menus = new Menus();
                bool deleteConfirmation = menus.ConfirmAction( Program.LanguageManager.Translate("confirm_delete_all_players"));

                if (deleteConfirmation)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    SavePlayers(new List<Player>());
                    Console.WriteLine(Program.LanguageManager.Translate("clear_all_players",allPlayers.Count));
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(Program.LanguageManager.Translate("no_players_registered"));
                Console.ResetColor();
            }
            Console.ResetColor();
            Console.WriteLine(Program.LanguageManager.Translate("press_any_key_to_continue"));
        }
    }
}