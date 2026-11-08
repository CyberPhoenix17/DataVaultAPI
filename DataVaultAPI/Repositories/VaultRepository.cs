using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using DataVaultAPI.Models;

namespace DataVaultAPI.Repositories;

public interface IVaultRepository
{
    Task<IEnumerable<VaultRecord>> GetAllAsync();
    Task<VaultRecord?> GetByIdAsync(int id);
    Task<int> CreateAsync(VaultRecord record);
    Task<int> UpdateAsync(VaultRecord record);
    Task<int> DeleteAsync(int id);
}

public class VaultRepository : IVaultRepository
{
    private readonly string? _connectionString;

    public VaultRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<VaultRecord>> GetAllAsync()
    {
        using var connection = CreateConnection();
        var sql = "SELECT * FROM VaultRecords";
        return await connection.QueryAsync<VaultRecord>(sql);
    }

    public async Task<VaultRecord?> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();
        var sql = "SELECT * FROM VaultRecords WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<VaultRecord>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(VaultRecord record)
    {
        using var connection = CreateConnection();
        var sql = @"INSERT INTO VaultRecords (RecordKey, Category, DataContent, CreatedAt) 
                    VALUES (@RecordKey, @Category, @DataContent, @CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
        
        return await connection.ExecuteScalarAsync<int>(sql, record);
    }

    public async Task<int> UpdateAsync(VaultRecord record)
    {
        using var connection = CreateConnection();
        var sql = @"UPDATE VaultRecords 
                    SET RecordKey = @RecordKey, Category = @Category, DataContent = @DataContent 
                    WHERE Id = @Id";
        
        return await connection.ExecuteAsync(sql, record);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = CreateConnection();
        var sql = "DELETE FROM VaultRecords WHERE Id = @Id";
        return await connection.ExecuteAsync(sql, new { Id = id });
    }
}