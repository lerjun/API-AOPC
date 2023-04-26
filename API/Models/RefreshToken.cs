namespace AuthSystem.Models
{
    public class RefreshToken
    {

        public string NewToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

    }
}
