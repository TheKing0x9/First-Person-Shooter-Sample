using UnityEngine;

namespace FPS.Enemies.Decisions
{
    [CreateAssetMenu(menuName = "Enemies/Decisions/PlayerDead")]
    public class PlayerDeadDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.playerDead;
        }
    }
}