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

        public void SetPosition(Vector3 position)
        {
            var shape = MainParticleSystem.shape;
            shape.position = position;
        }

        public void AddRotation(float rotation)
        {
            var shape = MainParticleSystem.shape;
            var shapeRotation = shape.rotation;
            shapeRotation.z += rotation;
            shape.rotation = shapeRotation;
        }

        public void SetColor(Color color)
        {
            var main = MainParticleSystem.main;
            main.startColor = color;
        }

        public void SetSize(float size)
        {
            var main = MainParticleSystem.main;
            main.startSize = new ParticleSystem.MinMaxCurve(size);
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