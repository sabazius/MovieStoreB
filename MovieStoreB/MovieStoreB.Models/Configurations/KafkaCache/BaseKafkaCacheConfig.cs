namespace MovieStoreB.Models.Configurations.KafkaCache
{
    public abstract class BaseKafkaCacheConfig
    {
        public string BootstrapServer { get; set; } = string.Empty;

        public string Topic { get; set; }
    }
}
