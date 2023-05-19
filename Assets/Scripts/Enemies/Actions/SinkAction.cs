using UnityEngine;

namespace FPS.Enemies.Actions
{
    [CreateAssetMenu(menuName = "Enemies/Actions/Sink")]
    public class SinkAction : Action
    {
        public override void OnStateEnter(StateController controller)
        {
            controller.navMeshAgent.enabled = false;
            GameObject.Destroy(controller.gameObject, 2f);
        }

        public override void OnStateUpdate(StateController controller)
        {
            Sink(controller);
        }

        private void Sink(StateController controller)
        {
            controller.transform.Translate (-Vector3.up * controller.sinkSpeed * Time.deltaTime);
        }
    }
}