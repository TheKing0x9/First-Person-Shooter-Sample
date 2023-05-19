using UnityEngine;

namespace FPS.Enemies
{
    [CreateAssetMenu(menuName = "Enemies/State")]
    public class State : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;

        public void OnStateEnter(StateController controller) 
        {
            foreach(Action a in actions) a.OnStateEnter(controller);
        }

        public void OnStateExit(StateController controller) 
        {
            foreach(Action a in actions) a.OnStateExit(controller);
        }

        public void OnStateUpdate(StateController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(StateController controller) 
        {
            foreach(Action a in actions) a.OnStateUpdate(controller);
        }

        private void CheckTransitions(StateController controller)
        {
            foreach(Transition t in transitions)
                if (t.decision.Decide(controller))
                    controller.TransitionToState(t.trueState);
                else
                    controller.TransitionToState(t.falseState);
        }
    }
}