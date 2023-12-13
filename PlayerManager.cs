using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NemLinha_Projeto
{
    public class PlayerManager
    {
        private const string FilePath = "players.json";
        static LanguageManager languageManager;

        static PlayerManager()
        {
            languageManager = new LanguageManager("pt"); // Replace with your default language
        }

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

        public static string AddPlayerStringOut(bool result, string pname)
        {
            string key = result ? "add_player_success" : "add_player_exists";
            return languageManager.Translate(key, pname);
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
                    return $"O jogador '{playerName}' foi atualizado com sucesso.";
                }
                return $"O jogador '{playerName}' não foi encontrado na lista.";
            }
            return $"O jogador '{playerName}' não foi encontrado.";
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
            return $"Jogador '{playerName}' não encontrado.";
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
            return $"Jogador '{playerName}' não encontrado.";
        }
        
        public static void ClearAllPlayers()
        {
            // Clear all players by overwriting the file with an empty list
            List<Player> allPlayers = LoadPlayers();
            Console.WriteLine();
            if(allPlayers.Count>0)
            {
                Menu menu = new Menu();
                bool deleteConfirmation = menu.ConfirmAction($"Tem certeza que deseja apagar todos os jogadores?");

                if (deleteConfirmation)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    SavePlayers(new List<Player>());
                    Console.WriteLine($"Todos os '{allPlayers.Count}' jogadores foram apagados.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Não há jogadores resgistados");
                Console.ResetColor();
            }
            Console.ResetColor();
            Console.WriteLine($"Pressione qualquer tecla para continuar.");
        }
    }
}