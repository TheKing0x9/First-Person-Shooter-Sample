using UnityEngine;

namespace FPS.Enemies.Actions
{
    [CreateAssetMenu(menuName = "Enemies/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void OnStateUpdate(StateController controller)
        {
            Chase(controller);
        }

        private void Chase(StateController controller)
        {
            controller.navMeshAgent.destination = controller.chaseTarget.transform.position;
            controller.navMeshAgent.isStopped = false;
        }
    }
}