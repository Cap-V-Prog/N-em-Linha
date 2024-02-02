using Newtonsoft.Json;

namespace NemLinha_Projeto
{
    public class Player
    {
        public string Name { get; set; }
        public int GamesPlayed { get; set; }
        public int Victories { get; set; }
        
        public Player(string name, int gamesPlayed, int victories)
        {
            Name = name;
            GamesPlayed = gamesPlayed;
            Victories = victories;
        }

        // Method to display player information
        public string DisplayPlayerInfo()
        {
            // Define a fixed width for the labels
            const int labelWidth = -20;

            // Use the fixed width for alignment
            return $"{Program.LanguageManager.Translate("player")+":",labelWidth} {Name}\n" +
                   $"{Program.LanguageManager.Translate("played_games")+":",labelWidth} {GamesPlayed}\n" +
                   $"{Program.LanguageManager.Translate("wins")+":",labelWidth} {Victories}";
        }


        // Method to convert player data to JSON string
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        // Method to create a Player object from JSON string
        public static Player FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Player>(json);
        }
    }
}