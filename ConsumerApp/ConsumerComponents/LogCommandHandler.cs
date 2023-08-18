using MediatR;
using Microsoft.Extensions.Logging;

namespace ConsumerApp.ConsumerComponents
{
    public class LogCommandHandler : IRequestHandler<LogCommand, Unit>
    {
        private readonly ILogger<LogCommandHandler> _logger;

        public LogCommandHandler(ILogger<LogCommandHandler> logger) => _logger = logger;

       
        Task<Unit> IRequestHandler<LogCommand, Unit>.Handle(LogCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("---- Received message: {Message} ----", request.Message);
            return Task.FromResult(Unit.Value);
        }
    }
}
