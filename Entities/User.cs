namespace WebApplication1.Entities
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string emailAdress { get; set; }
        public int administratorLevel { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}
