using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WebLibrary.DAL.Models;
using WebLibrary.DAL.Services.Contracts;

namespace WebLibrary.DAL.Services
{
    public class AuthorDalService : IAuthorDalService
    {
        private readonly string? _dbConnection;

        public AuthorDalService(IConfiguration configuration)
        {
            string connectionName = configuration.GetSection("ConnectionString").Value.ToString();

            _dbConnection = Environment.GetEnvironmentVariable(connectionName);

            if (string.IsNullOrEmpty(_dbConnection))
                throw new ArgumentNullException($"Connection string is not set. Please check variable with name: {connectionName}"); //add logging

        }

        /// <summary>
        /// Get all authors from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NpgsqlException"></exception>
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            try
            {
                using var connection = new NpgsqlConnection(_dbConnection);
                return await connection.QueryAsync<Author>("SELECT \"Id\", \"Name\", \"Description\" FROM \"Main\".\"Authors\";");
            }
            catch (Exception ex)
            {
                throw new NpgsqlException("Something went wrong when retriving the data.");
            }
        }

    }
}
