namespace PetShop.Web.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using PetShop.Sevices.Data;
    using PetShop.Sevices.Data.Contracts;
    using System.Reflection;

    public static  class WebApplicationBuilderExtensions
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            //Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            //if (serviceAssembly == null)
            //{
            //    throw new InvalidOperationException("Invalid service type provided!");
            //}

            //Type[] implementationTypes = serviceAssembly
            //    .GetTypes()
            //    .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
            //    .ToArray();
            //foreach (Type implementationType in implementationTypes)
            //{
            //    Type? interfaceType = implementationType
            //        .GetInterface($"I{implementationType.Name}");
            //    if (interfaceType == null)
            //    {
            //        throw new InvalidOperationException(
            //            $"No interface is provided for the service with name: {implementationType.Name}");
            //    }

            //    services.AddScoped(interfaceType, implementationType);
            //}

            services.AddScoped<IAnimalTypeService, AnimalTypeService>();
            services.AddScoped<ISellerService, SellerService>();
        }
    }
}
