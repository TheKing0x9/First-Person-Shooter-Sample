using UnityEngine;

namespace FPS.Utilities
{
    [CreateAssetMenu(menuName = "Events/Float Game Event", fileName = "New Float Event")]
    public class FloatEvent : BaseEvent<IFloatEventListener>
    {
        public float defaultValue = 100f;

        public void Raise(float value)
        {
            foreach (IFloatEventListener e in m_eventListeners)
                e.OnEventRaised((FloatEvent)this, value);
        }
    }
}