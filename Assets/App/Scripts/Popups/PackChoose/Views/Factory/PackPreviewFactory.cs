using Common.Packs.Data.Models;
using Popups.PackChoose.Views.Configurations;
using UnityEngine;

namespace Popups.PackChoose.Views.Factory
{
    public class PackPreviewFactory : IPackPreviewFactory
    {
        private readonly PackPreview _previewPrefab;
        private readonly PackPreviewConfiguration _notOpenedConfiguration;
        private readonly PackPreviewConfiguration _passedConfiguration;
        private readonly Sprite _notOpenedSprite;
        private readonly string _notFoundLocalizationKey;

        public PackPreviewFactory(
            PackPreview previewPrefab,
            PackPreviewConfiguration notOpenedConfiguration,
            PackPreviewConfiguration passedConfiguration,
            Sprite notOpenedSprite,
            string notFoundLocalizationKey)
        {
            _previewPrefab = previewPrefab;
            _notOpenedConfiguration = notOpenedConfiguration;
            _passedConfiguration = passedConfiguration;
            _notOpenedSprite = notOpenedSprite;
            _notFoundLocalizationKey = notFoundLocalizationKey;
        }
        
        public PackPreview CreatePackPreview(PackGameData packGameData, PackPreviewCreationContext context)
        {
            var packPreview = Object.Instantiate(_previewPrefab, context.Transform);
            packPreview.SetWidth(context.Width);
            
            if (packGameData.PackPersistentData.isOpened == false)
            {
                InitAsNotOpened(packPreview, packGameData);
            }
            else if(packGameData.PackPersistentData.isPassed)
            {
                InitAsPassed(packPreview, packGameData);
            }
            else
            {
                InitDefault(packPreview, packGameData);
            }

            return packPreview;
        }

        private void InitAsNotOpened(PackPreview packPreview, PackGameData packGameData)
        {
            packPreview.SetPackIconSprite(_notOpenedSprite);
            packPreview.HideEnergyInfo();
            packPreview.ApplyPackPreviewConfiguration(_notOpenedConfiguration);
            packPreview.UpdateLevelsInfo(packGameData.PackPersistentData);
            packPreview.SetLocalizationKey(_notFoundLocalizationKey);
        }
        
        private void InitAsPassed(PackPreview packPreview, PackGameData packGameData)
        {
            packPreview.ApplyPackPreviewConfiguration(_passedConfiguration);
            packPreview.SetPackIconSprite(packGameData.PackConfiguration.PackImage);
            packPreview.UpdateLevelsInfo(packGameData.PackPersistentData);
            packPreview.SetLocalizationKey(packGameData.PackConfiguration.Name);
            packPreview.SetEnergyInfo(packGameData.PackConfiguration);
        }
        
        private void InitDefault(PackPreview packPreview, PackGameData packGameData)
        {
            packPreview.ApplyPackGameData(packGameData);
        }
    }
}