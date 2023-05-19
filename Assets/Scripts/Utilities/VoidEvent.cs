using UnityEngine;

namespace FPS.Utilities {
    [CreateAssetMenu(menuName = "Events/Void Game Event", fileName = "New Void Event")]
    public class VoidEvent : BaseEvent<IVoidEventListener> {
        public void Raise()
        {
            foreach (IVoidEventListener e in m_eventListeners)
                e.OnEventRaised((VoidEvent)this);
        }
    }
}