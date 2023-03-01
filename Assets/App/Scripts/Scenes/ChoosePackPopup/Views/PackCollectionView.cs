using System.Collections.Generic;
using Scenes.MainGameScene.Configurations.Packs;
using UnityEngine;

namespace Scenes.ChoosePackPopup.Views
{
    public class PackCollectionView : MonoBehaviour
    {
        //ObjectPool for PackPreview
        [SerializeField] private RectTransform _viewsTransform;
        [SerializeField] private PackPreview _packPreview;

        private readonly List<PackPreview> _previews = new List<PackPreview>();

        public void ShowPacks(IEnumerable<PackConfiguration> packConfigurations)
        {
            foreach (var packConfiguration in packConfigurations)
            {
                var packPreview = Instantiate(_packPreview, _viewsTransform);
                packPreview.UpdateView(packConfiguration);
                _previews.Add(packPreview);
            }
        }

        public void Clear()
        {
            foreach (var packPreview in _previews)
            {
                Destroy(packPreview.gameObject);
            }
            _previews.Clear();
        }
    }
}