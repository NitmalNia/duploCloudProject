using Polly.Contrib.WaitAndRetry;
using Polly;
using Microsoft.Extensions.Logging;

namespace DuploCloud_WeatherForecast_Common.RetryPolicies;

public class RetryPolicies : IRetryPolicies
{
    private readonly ILogger<RetryPolicies> _logger;
    private const int StartSeconds = 1;
    private const int RetryLimit = 5;

    /// <summary>
    /// Default .ctor
    /// </summary>
    /// <param name="logger">The Serilog logger.</param>
    public RetryPolicies(ILogger<RetryPolicies> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Retry policy for a collection return type.
    /// </summary>
    /// <typeparam name="TReturnType">The generic type of the collection to return.</typeparam>
    /// <returns>A collection of the type specified.</returns>
    public IAsyncPolicy<IEnumerable<TReturnType>?> RetryPolicyCollection<TReturnType>() =>
        Policy<IEnumerable<TReturnType>?>
            .Handle<Exception>()
            .WaitAndRetryAsync(
                sleepDurations: Backoff.DecorrelatedJitterBackoffV2(
                    medianFirstRetryDelay: TimeSpan.FromSeconds(StartSeconds),
                    retryCount: RetryLimit
                ),
                onRetry: (result, timespan, retryCount, context) =>
                {
                    if (result.Exception != null)
                    {
                        _logger.LogError(result.Exception,
                            "An exception occurred on retry {RetryAttempt} for {Source}", retryCount.ToString(),
                            result.Exception.Source);
                    }
                    else
                    {
                        _logger.LogError("A non success was received on retry {RetryAttempt} for {PolicyKey}",
                            retryCount.ToString(), context.PolicyKey);
                    }
                });

    /// <summary>
    /// Retry policy for a void return type.
    /// </summary>
    /// <returns>A completed task.</returns>
    public IAsyncPolicy RetryPolicy() => Policy
        .Handle<Exception>()
        .WaitAndRetryAsync(
            sleepDurations: Backoff.DecorrelatedJitterBackoffV2(
                medianFirstRetryDelay: TimeSpan.FromSeconds(StartSeconds),
                retryCount: RetryLimit
            ),
            onRetry: (exception, timespan, retryCount, context) =>
            {
                if (exception != null)
                {
                    _logger.LogError(exception, "An exception occurred on retry {RetryAttempt} for {Source}",
                        retryCount.ToString(), exception.Source);
                }
                else
                {
                    _logger.LogError("A non success was received on retry {RetryAttempt} for {PolicyKey}",
                        retryCount.ToString(), context.PolicyKey);
                }
            });

    /// <summary>
    /// Retry policy for a generic return type.
    /// </summary>
    /// <typeparam name="TReturnType">The type to return.</typeparam>
    /// <returns>An object of the type specific.</returns>
    public IAsyncPolicy<TReturnType> RetryPolicy<TReturnType>() => Policy<TReturnType>
        .Handle<Exception>()
        .WaitAndRetryAsync(
            sleepDurations: Backoff.DecorrelatedJitterBackoffV2(
                medianFirstRetryDelay: TimeSpan.FromSeconds(StartSeconds),
                retryCount: RetryLimit
            ),
            onRetry: (result, span, retryCount, context) =>
            {
                if (result.Exception != null)
                {
                    _logger.LogError(result.Exception, "An exception occurred on retry {RetryAttempt} for {Source}",
                        retryCount.ToString(), result.Exception.Source);
                }
                else
                {
                    _logger.LogError("A non success was received on retry {RetryAttempt} for {PolicyKey}",
                        retryCount.ToString(), context.PolicyKey);
                }
            });
}
