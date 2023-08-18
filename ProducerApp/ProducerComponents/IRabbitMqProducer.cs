namespace ProducerApp.ProducerComponents
{
    public interface IRabbitMqProducer<in T>
    {
        void Publish(T @event);
    }
}
