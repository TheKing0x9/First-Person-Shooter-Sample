using UnityEngine;

namespace FPS.Enemies
{
    [System.Serializable]
    public class Transition
    {
        public State trueState;
        public State falseState;
        public Decision decision;
    }
}