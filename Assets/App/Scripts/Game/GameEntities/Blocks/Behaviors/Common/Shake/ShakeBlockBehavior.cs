using DG.Tweening;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Common.Shake
{
    public class ShakeBlockBehavior : IObjectBehavior<Block>
    {
        public bool IsDefault => false;

        private float _time;
        private Vector3 _strength;
        private int _vibrato;

        public void SetBehaviorParameters(float time, Vector3 strength, int vibrato)
        {
            _time = time;
            _strength = strength;
            _vibrato = vibrato;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            if (DOTween.IsTweening(entity.transform))
            {
                return;
            }

            entity.transform.DOShakeScale(_time, _strength, _vibrato).SetUpdate(true).Play();
        }
    }
}