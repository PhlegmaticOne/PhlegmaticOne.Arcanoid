using DG.Tweening;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.ShipObject
{
    public class ActiveShipPart : MonoBehaviour
    {
        public void ScaleBy(float scaleBy, float time)
        {
            var endValue = transform.localScale.x * scaleBy;
            transform.DOScaleX(endValue, time);
        }
    }
}