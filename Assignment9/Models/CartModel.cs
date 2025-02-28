using SQLite;

namespace Assignment9.Models
{
    public class CartModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ProfileID { get; set; }
        public int MusicItemID { get; set; }      
        public int Quantity { get; set; }
        public string CartItemTitle { get; set; }
        public string CartItemArtist { get; set; }
        public decimal CartItemPrice { get; set; }
        public decimal TotalPrice => CartItemPrice * Quantity;
    }
}