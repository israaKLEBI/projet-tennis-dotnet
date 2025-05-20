namespace TennisShopApp.Data.Models
{
    public class Tournoi
    {
        public int CodeT { get; set; }
        public string NomT { get; set; } = string.Empty;
        public string Ville { get; set; } = string.Empty;
        public List<Gain> Gains { get; set; } = new List<Gain>();
    }
}