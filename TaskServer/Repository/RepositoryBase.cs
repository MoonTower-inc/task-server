using Npgsql;
using NpgsqlTypes;

namespace TaskServer.Repository;

public class RepositoryBase
{
    protected static NpgsqlParameter safeNpgsqlParameter<T>(string parameterName, T? data)  =>
            data switch
            {
                long => new NpgsqlParameter() { ParameterName = parameterName, NpgsqlDbType = NpgsqlDbType.Bigint, Value = data },
                int => new NpgsqlParameter() { ParameterName = parameterName, NpgsqlDbType = NpgsqlDbType.Integer, Value = data },
                string => new NpgsqlParameter() { ParameterName = parameterName,NpgsqlDbType = NpgsqlDbType.Text, Value = data },
                null => new NpgsqlParameter() { ParameterName = parameterName, Value = DBNull.Value },
                _ => throw new Exception()
            };
}