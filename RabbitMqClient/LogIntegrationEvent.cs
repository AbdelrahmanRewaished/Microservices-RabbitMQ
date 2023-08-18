using System.Diagnostics.CodeAnalysis;

namespace RabbitMqClient
{
    public class LogIntegrationEvent
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
