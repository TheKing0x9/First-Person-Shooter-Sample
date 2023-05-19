using UnityEngine;

namespace FPS.Enemies.Decisions
{
    [CreateAssetMenu(menuName = "Enemies/Decisions/DeadDecision")]
    public class DeadDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return !controller.enemyHealth.isAlive;
        }
    }
}