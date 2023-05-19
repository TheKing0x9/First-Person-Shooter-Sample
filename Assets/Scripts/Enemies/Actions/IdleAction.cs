using UnityEngine;

namespace FPS.Enemies.Actions
{
    [CreateAssetMenu(menuName = "Enemies/Actions/Idle")]
    public class IdleAction : Action
    {
        public override void OnStateEnter(StateController controller)
        {
            controller.animator.SetTrigger("PlayerDead");
        }

        public override void OnStateUpdate(StateController controller)
        {
            Idle(controller);
        }

        private void Idle(StateController controller)
        {
            controller.navMeshAgent.isStopped = true;
        }
    }
}