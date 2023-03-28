using System.Collections.Generic;
using UnityEngine;

namespace Game.Composites
{
    public class WinParticles : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _winParticles;

        public void Show()
        {
            foreach (var particle in _winParticles)
            {
                particle.gameObject.SetActive(true);
                particle.Play();
            }
        }
        
        
        public void Hide()
        {
            foreach (var particle in _winParticles)
            {
                if (particle.gameObject.activeSelf)
                {
                    particle.Stop();
                    particle.gameObject.SetActive(false);
                }
            }
        }
    }
}