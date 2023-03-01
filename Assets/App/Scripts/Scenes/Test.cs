using Abstracts.Popups.Base;
using Scenes.MainGameScene.Configurations.Packs;
using Scenes.MainGameScene.Data.Repositories.ResourcesImplementation;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private PackCollectionConfiguration _packCollectionConfiguration;

        private IPopupManager _popupManager;
        private void Start()
        {
            var packsRepository = new ResourcesPackRepository(_packCollectionConfiguration);
            var packLevels = packsRepository.GetLevels("Tutorial");
            packLevels.PassLevel(0);
            packsRepository.Save(packLevels);
        }
    }
}