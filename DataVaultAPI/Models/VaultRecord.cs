namespace DataVaultAPI.Models
{
    public class VaultRecord
    {
        public int Id { get; set; }
        public string RecordKey { get; set; }= string.Empty;
        public string Category { get; set; }= string.Empty;
        public string DataContent { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}