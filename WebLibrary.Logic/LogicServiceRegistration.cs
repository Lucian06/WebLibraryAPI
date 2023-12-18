using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebLibrary.DAL;

namespace WebLibrary.Logic
{
    public static class LogicServiceRegistration
    {
        public static IServiceCollection AddLogicService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddMediatR(Assembly.GetExecutingAssembly());
            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            service.AddDalService(configuration);
            return service;
        }
    }
}
