using System.Linq;
using Core.Interfaces;
using Insfrastructure.Data.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), (typeof(BaseRepository<>)));

            services.Configure<ApiBehaviorOptions>(opt =>
           {
               opt.InvalidModelStateResponseFactory = actionContext =>
               {
                   var errors = actionContext.ModelState
                   .Where(e => e.Value.Errors.Count > 0)
                   .SelectMany(x => x.Value.Errors)
                   .Select(x => x.ErrorMessage).ToArray();

                   var errorResponse = new ApiValidationErrorResponse
                   {
                       Errors = errors
                   };
                   return new BadRequestObjectResult(errorResponse);
               };
           });

            return services;
        }
    }
}