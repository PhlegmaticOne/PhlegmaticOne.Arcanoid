using TMPro;
using UnityEngine;

namespace Popups.PackChoose.Views
{
    public class EnergyCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _energyText;

        public void SetEnergy(int energy) => _energyText.text = energy.ToString();
        public void Hide() => gameObject.SetActive(false);
    }
}