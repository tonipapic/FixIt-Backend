namespace FixIt_Backend.Data.Models.Custom
{
    public class JWTModel
    {
        public string Key { get; set; }
        public string ExpiresMinute { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
