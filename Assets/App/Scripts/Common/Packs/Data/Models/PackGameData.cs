﻿using Common.Packs.Configurations;

namespace Common.Packs.Data.Models
{
    public class PackGameData
    {
        public PackGameData(PackConfiguration packConfiguration, PackPersistentData packPersistentData)
        {
            PackPersistentData = packPersistentData;
            PackConfiguration = packConfiguration;
        }

        public PackConfiguration PackConfiguration { get; }
        public PackPersistentData PackPersistentData { get; }
    }
}