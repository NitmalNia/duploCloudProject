using System.Data;
using Dapper;
using DuploCloud_WeatherForecast_Common.RetryPolicies;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DuploCloud_WeatherForecast_Data;

/// <summary>
/// DataProviderBase for all data providers.
/// </summary>
public abstract class DataProviderBase
{
    protected readonly ILogger Logger;
    protected readonly IRetryPolicies _retryPolicies;
    protected readonly string _connectionString;
    protected const string ConnectionStringName = "WeatherForecast";

    /// <summary>
    /// Default .ctor
    /// </summary>
    /// <param name="configuration">The appsettings configuration to retrieve the database connection string.</param>
    /// <param name="logger">The default logger.</param>
    /// <param name="retryPolicies">The Polly retry configuration.</param>
    public DataProviderBase(IConfiguration configuration, ILogger logger, IRetryPolicies retryPolicies)
    {
        Logger = logger;
        _retryPolicies = retryPolicies;

        _connectionString = configuration.GetConnectionString(ConnectionStringName);

        if (Logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug($"ConnectionString is null = {string.IsNullOrEmpty(_connectionString)}");
        }
    }


    /// <summary>
    /// Creates a SqlConnection and passes a <see cref="CancellationToken" /> provided by the client application.
    /// </summary>
    /// <param name="cancellationToken">The token for cancellation.</param>
    /// <returns>An instantiated, open IDbConnection.</returns>
    protected async Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        if (Logger.IsEnabled(LogLevel.Debug))
        {
            Logger.LogDebug("SqlClient type is {SqlClientType}", connection.GetType().FullName);
            Logger.LogDebug($"IDbConnection state is '{connection.State}'");
        }

        return connection;
    }

    /// <summary>
    /// Executes a non-query.
    /// </summary>
    /// <param name="storedProcName">The stored procedure to be executed.</param>
    /// <param name="param">Query arguments.</param>
    /// <param name="cancellationToken">The token for cancellation.</param>
    /// <returns>True if successful</returns>
    protected async Task<bool> ExecuteAsync(
        string storedProcName,
        object param,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storedProcName);
        ArgumentNullException.ThrowIfNull(param);

        var rows = 0;

        rows = await Task<int>.Run(function: async () =>
        {
            return await _retryPolicies.RetryPolicy<int>().ExecuteAsync(async () =>
            {
                using var connection = await GetConnectionAsync(cancellationToken: cancellationToken);
                return await connection.ExecuteAsync(
                    sql: storedProcName,
                    param: param,
                    commandType: CommandType.StoredProcedure
                );
            });
        },
            cancellationToken: cancellationToken
        );

        return rows == 1;
    }

    /// <summary>
    /// Returns a single value with query arguments.
    /// </summary>
    /// <param name="storedProcName">The stored procedure to be executed.</param>
    /// <param name="param">Query arguments.</param>
    /// <param name="cancellationToken">The token for cancellation.</param>
    /// <returns>The return type of TScalarType for the query.</returns>
    protected async Task<TScalarType> ExecuteScalarAsync<TScalarType>(
        string storedProcName,
        object param,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storedProcName);
        ArgumentNullException.ThrowIfNull(param);

        TScalarType value = default;

        value = await Task.Run(async () =>
        {
            return await _retryPolicies.RetryPolicy<TScalarType>().ExecuteAsync(async () =>
            {
                var connection = await GetConnectionAsync(cancellationToken);
                return value = await connection.ExecuteScalarAsync<TScalarType>(
                    sql: storedProcName,
                    param: param,
                    commandType: CommandType.StoredProcedure
                );
            });
        },
            cancellationToken: cancellationToken
        );

        return value;
    }

    /// <summary>
    /// Returns a collection of <typeparam name="TReturnType"/> with query arguments.
    /// </summary>
    /// <param name="storedProcName">The stored procedure to be executed.</param>
    /// <param name="param">Query arguments.</param>
    /// <param name="cancellationToken">The token for cancellation.</param>
    /// <typeparam name="TReturnType">The return type of the collection.</typeparam>
    /// <returns>A collection of generic type TReturnType.</returns>
    protected async Task<IEnumerable<TReturnType>> QueryAsync<TReturnType>(
        string storedProcName,
        object param,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storedProcName);

        IEnumerable<TReturnType> list = default;

        list = await Task.Run(async () =>
        {
            return await _retryPolicies.RetryPolicy<IEnumerable<TReturnType>>().ExecuteAsync(async () =>
            {
                using var connection = await GetConnectionAsync(cancellationToken);
                return await connection.QueryAsync<TReturnType>(
                    sql: storedProcName,
                    param: param,
                    commandType: CommandType.StoredProcedure
                );
            });
        },
            cancellationToken: cancellationToken
        );

        return list;
    }

    /// <summary>
    /// Returns a collection of <typeparam name="TReturnType"/> with custom object mapping no query argument.
    /// </summary>
    /// <param name="storedProcName">The stored procedure to be executed.</param>
    /// <param name="types">The types to be mapped from the query.</param>
    /// <param name="map">The custom mapping to those <paramref name="types"/> from the <paramref name="sql" />.</param>
    /// <param name="cancellationToken">The token for cancellation.</param>
    /// <typeparam name="TReturnType">The return type of the collection.</typeparam>
    /// <returns>A collection of generic type TReturnType.</returns>
    protected async Task<IEnumerable<TReturnType>> QueryAsync<TReturnType>(
        string storedProcName,
        Type[] types,
        Func<object[], TReturnType> map,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storedProcName);
        ArgumentNullException.ThrowIfNull(types);
        ArgumentNullException.ThrowIfNull(map);

        IEnumerable<TReturnType> list = default;

        list = await Task.Run(async () =>
        {
            return await _retryPolicies.RetryPolicy<IEnumerable<TReturnType>>().ExecuteAsync(async () =>
            {
                using var connection = await GetConnectionAsync(cancellationToken);
                return await connection.QueryAsync(
                    sql: storedProcName,
                    types: types,
                    map: map,
                    commandType: CommandType.StoredProcedure
                );
            });
        },
            cancellationToken: cancellationToken
        );

        return list;
    }

    protected async Task<IEnumerable<TReturnType>> QueryAsync<TReturnType>(
        string storedProcName,
        object param,
        Type[] types,
        Func<object[], TReturnType> map,
        string splitOn,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storedProcName);
        ArgumentNullException.ThrowIfNull(types);
        ArgumentNullException.ThrowIfNull(map);

        IEnumerable<TReturnType> list = default;

        list = await Task.Run(async () =>
        {
            return await _retryPolicies.RetryPolicy<IEnumerable<TReturnType>>().ExecuteAsync(async () =>
            {
                using var connection = await GetConnectionAsync(cancellationToken);
                return await connection.QueryAsync(
                    sql: storedProcName,
                    types: types,
                    map: map,
                    param: param,
                    splitOn: splitOn,
                    commandType: CommandType.StoredProcedure
                );
            });
        },
            cancellationToken: cancellationToken
        );

        return list;
    }

    /// <summary>
    /// Returns a single entity of <typeparamref name="TReturnType"/> that takes parameters.
    /// </summary>
    /// <param name="storedProcName">The stored procedure to be executed.</param>
    /// <param name="cancellationToken">The token for cancellation.</param>
    /// <typeparam name="TReturnType">An entity of type <typeparamref name="TReturnType"/>.</typeparam>
    /// <returns></returns>
    protected async Task<TReturnType> QueryFirstAsync<TReturnType>(
        string storedProcName,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storedProcName);

        TReturnType entity = default;

        if (Logger.IsEnabled(LogLevel.Debug))
        {
            Logger.LogDebug("DataProviderBase.QueryFirstAsync(): Attempting Run...");
        }

        try
        {
            entity = await Task.Run(async () =>
            {
                return await _retryPolicies.RetryPolicy<TReturnType>().ExecuteAsync(async () =>
                {
                    using var connection = await GetConnectionAsync(cancellationToken: cancellationToken);

                    return await connection.QueryFirstAsync<TReturnType>(
                        sql: storedProcName,
                        commandType: CommandType.StoredProcedure
                    );
                });
            },
                cancellationToken: cancellationToken
            );
        }
        catch (Exception e)
        {
            Logger.LogError(e, null);
            throw;
        }

        return entity;
    }

    /// <summary>
    /// Returns a single entity of <typeparamref name="TReturnType"/> that takes parameters.
    /// </summary>
    /// <param name="storedProcName">The stored procedure to be executed.</param>
    /// <param name="param">Query arguments.</param>
    /// <param name="cancellationToken">The token for cancellation.</param>
    /// <typeparam name="TReturnType">An entity of type <typeparamref name="TReturnType"/>.</typeparam>
    /// <returns></returns>
    protected async Task<TReturnType> QueryFirstAsync<TReturnType>(
        string storedProcName,
        object param,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storedProcName);
        ArgumentNullException.ThrowIfNull(param);

        IEnumerable<TReturnType> list = default;
        Logger.LogDebug("DataProviderBase.QueryFirstAsync(): Attempting Run...");
        list = await Task.Run(async () =>
        {
            return await _retryPolicies.RetryPolicy<IEnumerable<TReturnType>>().ExecuteAsync(async () =>
            {
                using var connection = await GetConnectionAsync(cancellationToken: cancellationToken);
                return await connection.QueryAsync<TReturnType>(
                    sql: storedProcName,
                    param: param,
                    commandType: CommandType.StoredProcedure
                );
            });
        },
            cancellationToken: cancellationToken
        );
        Logger.LogDebug($"Can return FirstOrDefault(): {list?.Count() > 0}");

        return list.FirstOrDefault();
    }

    /// <summary>
    /// Returns a single entity of <typeparamref name="TReturnType"/> with custom object mapping and no query arguments.
    /// </summary>
    /// <param name="storedProcName">The stored procedure to be executed.</param>
    /// <param name="types">The types to be mapped from the query.</param>
    /// <param name="map">The custom mapping to those <paramref name="types"/> from the <paramref name="sql" />.</param>
    /// <param name="cancellationToken">The token for cancellation.</param>
    /// <typeparam name="TReturnType">The entity type to be returned.</typeparam>
    /// <returns>A record of type TReturnType.</returns>
    protected async Task<TReturnType> QueryFirstAsync<TReturnType>(
        string storedProcName,
        Type[] types,
        Func<object[], TReturnType> map,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storedProcName);
        ArgumentNullException.ThrowIfNull(types);
        ArgumentNullException.ThrowIfNull(map);

        IEnumerable<TReturnType> list = default;

        Logger.LogDebug("DataProviderBase.QueryFirstAsync(): Attempting Run...");

        list = await Task.Run(async () =>
        {
            return await _retryPolicies.RetryPolicy<IEnumerable<TReturnType>>().ExecuteAsync(async () =>
            {
                using var connection = await GetConnectionAsync(cancellationToken: cancellationToken);
                return await connection.QueryAsync(
                    sql: storedProcName,
                    types: types,
                    map: map,
                    commandType: CommandType.StoredProcedure
                );
            });
        },
            cancellationToken: cancellationToken
        );

        Logger.LogDebug($"Can return FirstOrDefault(): {list?.Count() > 0}");

        return list.FirstOrDefault();
    }

    /// <summary>
    /// Gets a single record of type <typeparamref name="TReturnType"/> with custom object mapping and query arguments.
    /// </summary>
    /// <param name="storedProcName">The stored procedure to be executed.</param>
    /// <param name="param">Query arguments.</param>
    /// <param name="types">The types to be mapped from the query.</param>
    /// <param name="map">The custom mapping to those <paramref name="types"/> from the <paramref name="storedProcName" />.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the request.</param>
    /// <typeparam name="TReturnType">The entity type to be returned.</typeparam>
    /// <returns>An entity of type <typeparamref name="TReturnType"/>.</returns>
    protected async Task<TReturnType> QueryFirstAsync<TReturnType>(
        string storedProcName,
        object param,
        Type[] types,
        Func<object[], TReturnType> map,
                string splitOn,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storedProcName);
        ArgumentNullException.ThrowIfNull(param);
        ArgumentNullException.ThrowIfNull(types);
        ArgumentNullException.ThrowIfNull(map);

        IEnumerable<TReturnType> list = default;

        list = await Task.Run(async () =>
        {
            return await _retryPolicies.RetryPolicy<IEnumerable<TReturnType>>().ExecuteAsync(async () =>
            {
                using var connection = await GetConnectionAsync(cancellationToken);
                return await connection.QueryAsync(
                    sql: storedProcName,
                    types: types,
                    map: map,
                    param: param,
                    splitOn: splitOn,
                    commandType: CommandType.StoredProcedure
                );
            });
        },
            cancellationToken: cancellationToken
        );
        Logger.LogDebug($"Can return FirstOrDefault(): {list?.Count() > 0}");

        return list.FirstOrDefault();
    }
}