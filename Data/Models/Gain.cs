namespace TennisShopApp.Data.Models
{
    public class Gain
    {
        public int CodeT { get; set; }
        public int CodeJ { get; set; }
        public int CodePh { get; set; }
        public Tournoi? Tournoi { get; set; }
        public Joueur? Joueur { get; set; }
        public Phase? Phase { get; set; }
    }
}