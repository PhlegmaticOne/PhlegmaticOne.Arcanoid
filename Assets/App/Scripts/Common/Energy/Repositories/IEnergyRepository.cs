using Common.Energy.Models;

namespace Common.Energy.Repositories
{
    public interface IEnergyRepository
    {
        EnergyModel GetEnergyModel();
        void Save(EnergyModel energyModel);
        void Clear();
    }
}