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

        // Method to load player data from a JSON file
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
    }
}