using Mapster;
using ToDo.App.Mappings;
using ToDo.Domain;

namespace ToDo.API.Infrastructure.Mappings
{
    public static class MapsterConfig
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            /* some presentation level model mappings */

            CoreMapsterConfig.ConfigureCoreMappings();
        }
    }
}
