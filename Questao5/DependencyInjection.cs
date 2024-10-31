using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.CommandStore.Interfaces;
using Questao5.Infrastructure.Database.QueryStore;
using Questao5.Infrastructure.Database.QueryStore.Interfaces;

namespace Questao5
{
    public class DependencyInjection
    {
        public static void Register(IServiceCollection services)
        {
            RepositoryDependence(services);
        }

        private static void RepositoryDependence(IServiceCollection services)
        {
            services.AddScoped<ICommandStore, CommandStore>();
            services.AddScoped<IQueryStore, QueryStore>();

        }
    }
}
