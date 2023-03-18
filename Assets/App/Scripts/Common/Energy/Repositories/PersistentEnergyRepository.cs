using System;
using System.IO;
using Common.Energy.Configurations;
using Common.Energy.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Common.Energy.Repositories
{
    public class PersistentEnergyRepository : IEnergyRepository
    {
        private const string Format = ".json";
        private readonly EnergyConfiguration _energyConfiguration;

        public PersistentEnergyRepository(EnergyConfiguration energyConfiguration)
        {
            _energyConfiguration = energyConfiguration;
            
            var directoryPath = GetPersistentPath();

            if (Directory.Exists(directoryPath) == false)
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public EnergyModel GetEnergyModel()
        {
            var file = GetFilePath();
            
            if (File.Exists(file) == false)
            {
                return CreateDefaultEnergyModel();
            }
            
            var energyModel = LoadEnergyModel(file);
            Synchronize(energyModel);
            return energyModel;
        }

        public void Save(EnergyModel energyModel)
        {
            var path = GetFilePath();
            energyModel.lastModifiedTime = DateTime.Now;
            var json = JsonConvert.SerializeObject(energyModel);
            File.WriteAllText(path, json);
        }

        private static void Synchronize(EnergyModel energyModel)
        {
            if (energyModel.currentEnergy >= energyModel.maxEnergy)
            {
                return;
            }
            
            var lastModified = energyModel.lastModifiedTime;
            var currentTime = DateTime.Now;
            var minutesPassed = (currentTime - lastModified).TotalMinutes;

            if (minutesPassed >= (energyModel.maxEnergy - energyModel.currentEnergy) *
                energyModel.regenerationTimeInMinutes)
            {
                energyModel.currentEnergy = energyModel.maxEnergy;
                return;
            }

            var toAdd = (int)(minutesPassed / energyModel.regenerationTimeInMinutes);

            energyModel.currentEnergy += toAdd;

            if (energyModel.currentEnergy > energyModel.maxEnergy)
            {
                energyModel.currentEnergy = energyModel.maxEnergy;
            }
        }

        private EnergyModel CreateDefaultEnergyModel()
        {
            var energyModel = new EnergyModel
            {
                currentEnergy = _energyConfiguration.StartEnergy,
                maxEnergy = _energyConfiguration.MaxEnergy,
                regenerationTimeInMinutes = _energyConfiguration.RegenerationTimeInMinutes,
                lastModifiedTime = DateTime.Now,
                increaseEnergyFromTimeCount = _energyConfiguration.IncreaseEnergyFromTimeCount
            };
            
            Save(energyModel);
            return energyModel;
        }

        private string GetFilePath() => 
            Combine(GetPersistentPath(), _energyConfiguration.FileName + Format);

        public static string GetEnergySaveFilePath(EnergyConfiguration energyConfiguration)
        {
            return Combine(Application.persistentDataPath,
                energyConfiguration.DirectoryPersistentPath,
                energyConfiguration.FileName + Format);
        }

        private string GetPersistentPath() => 
            Combine(Application.persistentDataPath, _energyConfiguration.DirectoryPersistentPath);

        private static EnergyModel LoadEnergyModel(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<EnergyModel>(json);
        }
        private static string Combine(params string[] values) => string.Join("/", values);
    }
}