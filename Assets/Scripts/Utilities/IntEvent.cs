using UnityEngine;

namespace FPS.Utilities
{
    [CreateAssetMenu(menuName = "Events/Int Game Event", fileName = "New Int Event")]
    public class IntEvent : BaseEvent<IIntEventListener>
    {
        public int defaultValue = 100;

        public void Raise(int value)
        {
            foreach (IIntEventListener e in m_eventListeners)
                e.OnEventRaised((IntEvent)this, value);
        }
    }
}