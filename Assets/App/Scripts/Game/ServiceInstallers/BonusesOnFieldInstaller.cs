using Game.Bonuses;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class BonusesOnFieldInstaller : ServiceInstaller
    {
        [SerializeField] private BonusesOnField _bonusesOnField;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_bonusesOnField);
        }
    }
}