using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Packs.Configurations
{
    [CreateAssetMenu(menuName = "Packs/Create packs configuration", order = 0)]
    public class PacksConfiguration : SerializedScriptableObject
    {
        [SerializeField] private PacksFileAttributes _packsFileAttributes;
        [SerializeField] private DefaultPackConfiguration _defaultPackConfiguration;
        [SerializeField] private bool _isUpdatePacksInEditor;
        [SerializeField] private bool _isUpdateAllPacks;
        [SerializeField] private List<UpdatePackInfo> _packConfigurations;
        public PacksFileAttributes PacksFileAttributes => _packsFileAttributes;
        public bool IsUpdatePacksInEditor => _isUpdatePacksInEditor;
        public bool IsUpdateAllPacks => _isUpdateAllPacks;
        public List<UpdatePackInfo> PackConfigurations => _packConfigurations;
        public List<PackConfiguration> RegisteredPackConfigurations { get; private set; }
        public DefaultPackConfiguration DefaultPackConfiguration => _defaultPackConfiguration;
        public bool IsInitialized { get; private set; }

        private void OnEnable()
        {
            if (RegisteredPackConfigurations == null ||
                RegisteredPackConfigurations.Count != _packConfigurations.Count)
            {
                RegisteredPackConfigurations = PackConfigurations.Select(x => x.PackConfiguration).ToList();
            }
        }

        public void MarkAsInitialized()
        {
            _isUpdateAllPacks = false;
            _isUpdatePacksInEditor = false;
            
            foreach (var key in _packConfigurations.ToList())
            {
                key.IsUpdate = false;
            }

            IsInitialized = true;
        }
        
        public void MarkToUpdateAllPacks()
        {
            _isUpdateAllPacks = true;
            _isUpdatePacksInEditor = true;
            IsInitialized = false;
        }
    }

    [Serializable]
    public class PacksFileAttributes
    {
        [SerializeField] private string _packsInResourcesDirectoryPath;
        [SerializeField] private string _packsInPersistentDirectoryPath;
        [SerializeField] private string _levelsSubfolderName;
        [SerializeField] private string _dataSubfolderName;
        [SerializeField] private string _packDataFilePostfix;
        [SerializeField] private string _packPreviewFilePostfix;
        
        public string PacksInResourcesDirectoryPath => _packsInResourcesDirectoryPath;
        public string PacksInPersistentDirectoryPath => _packsInPersistentDirectoryPath;
        public string LevelsSubfolderName => _levelsSubfolderName;
        public string DataSubfolderName => _dataSubfolderName;
        public string PackDataFilePostfix => _packDataFilePostfix;
        public string PackPreviewFilePostfix => _packPreviewFilePostfix;
    }

    [Serializable]
    public class DefaultPackConfiguration
    {
        [SerializeField] private PackConfiguration _defaultPack;
        [SerializeField] private int _defaultLevelId;

        public PackConfiguration DefaultPack => _defaultPack;
        public int DefaultLevelId => _defaultLevelId;
    }

    [Serializable]
    public class UpdatePackInfo
    {
        [SerializeField] private PackConfiguration _packConfiguration;
        [SerializeField] private bool _isUpdate;
        public PackConfiguration PackConfiguration => _packConfiguration;

        public bool IsUpdate
        {
            get => _isUpdate;
            set => _isUpdate = value;
        }
    }
}