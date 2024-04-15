namespace Store
{
    public class StoreOptions
    {
        public const string ConfigSectionName = "Store";
        public string PostgresConnectionString { get; set; } = string.Empty;
    }
}