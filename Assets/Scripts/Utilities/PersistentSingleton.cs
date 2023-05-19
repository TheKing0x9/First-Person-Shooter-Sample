using UnityEngine;

namespace FPS.Utilities
{
    public class PersistentSingleton<T> : Singleton<T> where T : PersistentSingleton<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}