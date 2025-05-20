namespace TennisShopApp.Data.Models
{
    public class Phase
    {
        public int CodePh { get; set; }
        public string LibellePh { get; set; } = string.Empty; // Initialize to avoid CS8618
        public List<Gain> Gains { get; set; } = new List<Gain>();
    }
}