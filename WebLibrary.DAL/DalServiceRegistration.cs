using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebLibrary.DAL.Services;
using WebLibrary.DAL.Services.Contracts;

namespace WebLibrary.DAL
{
    public static class DalServiceRegistration
    {
        public static IServiceCollection AddDalService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IBookDalService, BookDalService>();
            service.AddTransient<IAuthorDalService, AuthorDalService>();

            var dbConnection = Environment.GetEnvironmentVariable(configuration.GetSection("ConnectionString").Value.ToString());
            if (string.IsNullOrEmpty(dbConnection))
                throw new ArgumentNullException($"Connection string is not set. Please check variable with name: {dbConnection}"); //add logging


            service.AddDbContext<DbContext, MainDbContext>(option => option.UseNpgsql(dbConnection,
            option => option.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

            return service;
        }
    }
}
