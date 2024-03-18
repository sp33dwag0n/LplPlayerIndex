namespace LPLWebApp.Models
{
    public class Player
    {
        public int id { get; set; }
        public string Ign { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Team { get; set; }

        public string? Position { get; set; }

        public Player()
        {
            
        }
    }
}
