using UnityEngine;

namespace FPS.Enemies.Actions
{
    [CreateAssetMenu(menuName = "Enemies/Actions/Die")]
    public class DieAction : IdleAction {
        public override void OnStateEnter(StateController controller) {
            controller.animator.SetTrigger("Dead");

            controller.addScoreEvent.Raise(controller.scoreToAdd);
        }
    }
}