using System.Collections.Generic;
using Common.Configurations.Packs;
using Common.Data.Models;
using Popups.LevelChoose.Views;
using UnityEngine;
using UnityEngine.Events;

namespace SPopups.LevelChoose.Views
{
    public class LevelsCollectionView : MonoBehaviour
    {
        [SerializeField] private RectTransform _spawnTransform;
        [SerializeField] private LevelPreview _levelPreview;
        
        private readonly List<LevelPreview> _previews = new List<LevelPreview>();
        private readonly List<LevelPreviewData> _levelPreviewList = new List<LevelPreviewData>();
        
        public event UnityAction<LevelPreviewData> LevelClicked; 

        public void ShowLevels(PackLevelCollection packLevelCollection, PackConfiguration packConfiguration)
        {
            var i = 0;
            foreach (var levelPreviewData in packLevelCollection.LevelPreviews)
            {
                var levelPreview = Instantiate(_levelPreview, _spawnTransform);
                levelPreview.LevelClicked += LevelPreviewOnClicked;
                levelPreview.UpdateView(i, packConfiguration.PackColor);
                _previews.Add(levelPreview);
                _levelPreviewList.Add(levelPreviewData);
                ++i;
            }
        }
        
        private void LevelPreviewOnClicked(int index)
        {
            LevelClicked?.Invoke(_levelPreviewList[index]);
        }

        public void DisableLevels()
        {
            foreach (var levelPreview in _previews)
            {
                levelPreview.Disable();
            }
        }
        
        public void EnableLevels()
        {
            foreach (var levelPreview in _previews)
            {
                levelPreview.Enable();
            }
        }

        public void Clear()
        {
            foreach (var levelPreview in _previews)
            {
                levelPreview.LevelClicked -= LevelPreviewOnClicked;
                Destroy(levelPreview.gameObject);
            }
            
            _levelPreviewList.Clear();
            _previews.Clear();
        }
    }
}