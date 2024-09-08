using UnityEngine;

namespace Libs.Popups.Configurations
{
    [CreateAssetMenu(menuName = "Popups/Create popup configuration", order = 0)]
    public class PopupConfiguration : ScriptableObject
    {
        [SerializeField] private string _sortingLayerName;
        [SerializeField] private Popup _popup;

        public string SortingLayerName => _sortingLayerName;
        public Popup Popup => _popup;
    }
}