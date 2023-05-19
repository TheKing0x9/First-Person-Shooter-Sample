using UnityEngine;

namespace FPS.Enemies
{
    public abstract class Action : ScriptableObject
    {
        public virtual void OnStateEnter(StateController controller) {}
        public virtual void OnStateExit(StateController controller) {}
        public virtual void OnStateUpdate(StateController controller) {}
    }
}