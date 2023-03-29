using System;
using UnityEngine;

namespace Game.ObjectParticles.Particles
{
    public class WallParticle : ParticleBase
    {
        private Vector3 _startRotation;
        private Color _startColor;
        private ParticleSystem.MinMaxCurve _startMinMaxCurve;
        private void Awake()
        {
            var main = MainParticleSystem.main;
            var shape = MainParticleSystem.shape;

            _startRotation = shape.rotation;
            _startColor = main.startColor.color;
            _startMinMaxCurve = main.startSize;
        }

        public override void Reset()
        {
            var shapeModule = MainParticleSystem.shape;
            var mainModule = MainParticleSystem.main;
            
            shapeModule.rotation = _startRotation;
            mainModule.startColor = _startColor;
            mainModule.startSize = _startMinMaxCurve;
        }
    }
}