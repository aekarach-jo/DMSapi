namespace DMSapi.Models
{
    public class DatabaseSetting : IDatabaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string RoomCollection { get; set; }
        public string UserCollection { get; set; }
        public string MeterCollection { get; set; }

    }

    public interface IDatabaseSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string UserCollection { get; set; }
        string MeterCollection { get; set; }

    }
}