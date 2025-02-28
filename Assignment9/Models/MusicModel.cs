using SQLite;

namespace Assignment9.Models
{
    public class MusicModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Album { get; set; }

        public static implicit operator string(MusicModel v)
        {
            throw new NotImplementedException();
        }
    }
}