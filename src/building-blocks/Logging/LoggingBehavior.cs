using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull, IRequest<TResponse>
     where TResponse : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {X-RequestData}", typeof(TRequest).Name);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3)
            {
                _logger.LogWarning("The request {X-RequestData} took {TimeTaken} seconds.", typeof(TRequest).Name, timeTaken.Seconds);
            }

            _logger.LogInformation("Handled {X-RequestData}", typeof(TRequest).Name);
            return response;
        }
    }
}