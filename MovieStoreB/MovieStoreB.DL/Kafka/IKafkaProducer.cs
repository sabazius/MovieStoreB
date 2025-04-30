namespace MovieStoreB.DL.Kafka
{
    public interface IKafkaProducer<TData>
    {
        Task ProduceAll(IEnumerable<TData> messages);

        Task Produce(TData message);

        Task ProduceBatches(IEnumerable<TData> messages);
    }
}