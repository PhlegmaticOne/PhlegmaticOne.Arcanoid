using System.Linq;
using Common.Data.Repositories.Base;
using Libs.Services;

namespace Composites.Seeding
{
    public static class PacksSeed
    {
        public static void TrySeedPacks()
        {
            var serviceProvider = ServiceProviderAccessor.Global;
            
            var packRepository = serviceProvider.GetRequiredService<IPackRepository>();
        
            if (packRepository.PacksInitialized)
            {
                return;
            }
        
            var packConfigurations = packRepository.GetAll().ToList();
        
            foreach (var packConfiguration in packConfigurations)
            {
                var levelsCount = packRepository.GetLevelsCount(packConfiguration.Name);
                packConfiguration.SetLevelsCount(levelsCount);
                packRepository.Save(packConfiguration);
            }
        
            packRepository.MarkAsInitialized();
            packRepository.Save();
        }
    }
}