using SQLite;

namespace Assignment9.Models
{
    public class ProfileModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
    }
}