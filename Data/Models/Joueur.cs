namespace TennisShopApp.Data.Models
{
    public class Joueur
    {
        public int CodeJ { get; set; }
        public string NomPrenom { get; set; } = string.Empty;
        public int Score { get; set; }
        public List<Gain> Gains { get; set; } = new List<Gain>();
    }
}