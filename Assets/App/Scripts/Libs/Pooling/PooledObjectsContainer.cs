using UnityEngine;

namespace Libs.Pooling
{
    public class PooledObjectsContainer : MonoBehaviour
    {
        private static bool _initialized;
        private void Awake()
        {
            if (_initialized)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            _initialized = true;
        }
    }
}