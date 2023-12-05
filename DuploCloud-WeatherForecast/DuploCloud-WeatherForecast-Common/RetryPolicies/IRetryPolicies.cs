using Polly;

namespace DuploCloud_WeatherForecast_Common.RetryPolicies;

public interface IRetryPolicies
{
    /// <summary>
    /// Retry policy for a void return type.
    /// </summary>
    /// <returns>A completed task.</returns>
    IAsyncPolicy RetryPolicy();

    /// <summary>
    /// Retry policy for a generic return type.
    /// </summary>
    /// <typeparam name="TReturnType">The type to return.</typeparam>
    /// <returns>An object of the type specific.</returns>
    IAsyncPolicy<TReturnType> RetryPolicy<TReturnType>();

    /// <summary>
    /// Retry policy for a collection return type.
    /// </summary>
    /// <typeparam name="TReturnType">The generic type of the collection to return.</typeparam>
    /// <returns>A collection of the type specified.</returns>
    IAsyncPolicy<IEnumerable<TReturnType>?> RetryPolicyCollection<TReturnType>();
}
