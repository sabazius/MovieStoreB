using Confluent.Kafka;
using MovieStoreB.Models.DTO;
using MovieStoreB.Models.Serialization;

namespace MovieStoreB.DL.Kafka
{
    internal class KafkaProducer<TKey, TData> : IKafkaProducer<TData> where TData : CacheItem<TKey> where TKey : notnull
    {
        private readonly ProducerConfig _config;
        private readonly IProducer<TKey, TData> _producer;

        public KafkaProducer()
        {
            _config = new ProducerConfig()
            {
                BootstrapServers = "kafka-193981-0.cloudclusters.net:10300",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "admin",
                SaslPassword = "CPxpKSRD",
                EnableSslCertificateVerification = false
            };

            _producer = new ProducerBuilder<TKey, TData>(_config)
                .SetValueSerializer(new MsgPackSerializer<TData>())
                .Build();
        }

        public async Task Produce(TData message)
        {
            await _producer.ProduceAsync("test", new Message<TKey, TData>
            {
                Key = message.GetKey(),
                Value = message
            });
        }

        public async Task ProduceAll(IEnumerable<TData> messages)
        {
            var tasks = messages.Select(message => Produce(message));

            await Task.WhenAll(tasks);
        }

        public async Task ProduceBatches(IEnumerable<TData> messages)
        {
            const int batchSize = 50;
            var batch = new List<Task>();

            foreach (var message in messages)
            {
                batch.Add(Produce(message));

                if (batch.Count == batchSize)
                {
                    await Task.WhenAll(batch);
                    batch.Clear();
                }
            }

            // Process any remaining messages
            if (batch.Count > 0)
            {
                await Task.WhenAll(batch);
            }
        }

    }
}
