using UnityEngine;
using System.Collections.Generic;

namespace FPS.Utilities
{
    public abstract class BaseEvent<T> : ScriptableObject
    {
        protected readonly List<T> m_eventListeners = new List<T>();

        public void Register(T e)
        {
            if (!m_eventListeners.Contains(e))
                m_eventListeners.Add(e);
        }

        public void Unregister(T e)
        {
            if (m_eventListeners.Contains(e))
                m_eventListeners.Remove(e);
        }
    }
}