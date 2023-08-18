using MediatR;

namespace ConsumerApp.ConsumerComponents
{
    public class LogCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
