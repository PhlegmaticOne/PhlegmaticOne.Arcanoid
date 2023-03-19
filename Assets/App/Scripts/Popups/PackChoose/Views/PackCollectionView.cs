using System.Collections.Generic;
using System.Linq;
using Common.Packs.Data.Models;
using Common.Positioning;
using Libs.Localization.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Popups.PackChoose.Views
{
    public class PackCollectionView : MonoBehaviour, ILocalizable
    {
        //ObjectPool for PackPreview
        [SerializeField] [Range(0f, 1f)] private float _marginSide;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _viewsTransform;
        [SerializeField] private PackPreview _packPreview;
        [SerializeField] private ViewportResizer _viewportResizer;
        private bool _isEnabled = true;

        private readonly List<PackPreview> _previews = new List<PackPreview>();
        private readonly List<PackGameData> _packConfigurations = new List<PackGameData>();
        public event UnityAction<PackGameData> PackClicked; 
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => 
            _previews.Select(x => x.PackNameTextBindableComponent);

        public void ShowPacks(IEnumerable<PackGameData> packGameData)
        {
            var i = 0;
            foreach (var packData in packGameData)
            {
                var packPreview = Instantiate(_packPreview, _viewsTransform);
                packPreview.Clicked += PackPreviewOnClicked;
                packPreview.UpdateView(i, packData);
                packPreview.SetWidth((1 - 2 * _marginSide) * _viewportResizer.Width);
                _previews.Add(packPreview);
                _packConfigurations.Add(packData);
                ++i;
            }
        }

        public void Disable()
        {
            _isEnabled = false;
            _scrollRect.vertical = false;
        }

        public void Enable()
        {
            _isEnabled = true;
            _scrollRect.vertical = true;
        }

        private void PackPreviewOnClicked(int index)
        {
            if (_isEnabled == false)
            {
                return;
            }
            PackClicked?.Invoke(_packConfigurations[index]);
        }

        public void Clear()
        {
            foreach (var packPreview in _previews)
            {
                packPreview.Clicked -= PackPreviewOnClicked;
                Destroy(packPreview.gameObject);
            }
            
            _packConfigurations.Clear();
            _previews.Clear();
        }
    }
}