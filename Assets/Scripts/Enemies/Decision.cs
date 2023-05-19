using UnityEngine;

namespace FPS.Enemies 
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(StateController controller);
    }
}