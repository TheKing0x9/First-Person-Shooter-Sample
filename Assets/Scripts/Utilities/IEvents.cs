using UnityEngine;

namespace FPS.Utilities
{
    public interface IVoidEventListener
    {
        public void OnEventRaised(VoidEvent e);
    }

    public interface IFloatEventListener
    {
        public void OnEventRaised(FloatEvent e, float f);
    }

    public interface IIntEventListener
    {
        public void OnEventRaised(IntEvent e, int i);
    }
}