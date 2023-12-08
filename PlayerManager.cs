using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NemLinha_Projeto
{
    public class PlayerManager
    {
        private const string FilePath = "players.json";

        // Method to save player data to a JSON file
        public static void SavePlayers(List<Player> players)
        {
            string json = JsonConvert.SerializeObject(players, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        // Method to load players' data from a JSON file for a specific player
        public static Player LoadPlayer(string playerName)
        {
            List<Player> players = LoadPlayers();
            return players.Find(player => player.Name == playerName);
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

        public static string AddPlayerStringOut(bool result,string pname)
        {
            if(result)
            {
                return $"O jogador '{pname}' foi adicionado com sucesso.";
            }
            else
            {
                return $"O jogador '{pname}' já existe.";
            }
        }
        
        public static string UpdatePlayerStats(string playerName, Action<Player> updateAction)
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
                    return $"Player '{playerName}' statistics updated successfully.";
                }
                else
                {
                    return $"Player '{playerName}' not found in the list.";
                }
            }
            else
            {
                return $"Player '{playerName}' not found.";
            }
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
            Player player=LoadPlayer(playerName);
            
            if (player != null)
            {
                return player.DisplayPlayerInfo();
            }
            else
            {
                return $"Player '{playerName}' not found.";
            }
        }
        
        public static string DeletePlayer(string playerName)
        {
            List<Player> allPlayers = LoadPlayers();
            Player playerToRemove = allPlayers.Find(player => player.Name == playerName);

            if (playerToRemove != null)
            {
                allPlayers.Remove(playerToRemove);
                SavePlayers(allPlayers);
                return $"Jogador '{playerName}' apagado com sucesso.";
            }
            else
            {
                return $"Jogador '{playerName}' não encontrado.";
            }
        }
        
        public static void ClearAllPlayers()
        {
            // Clear all players by overwriting the file with an empty list
            SavePlayers(new List<Player>());
            Console.WriteLine("All players cleared.");
        }
    }
}