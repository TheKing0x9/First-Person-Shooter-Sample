using UnityEngine;

namespace FPS.Enemies.Decisions
{
    [CreateAssetMenu(menuName = "Enemies/Decisions/TimeOutDecision")]
    public class TimeOutDecision : Decision
    {
        [SerializeField] protected float waitTime = 2f;

        public override bool Decide(StateController controller)
        {
            return controller.stateTimeElapsed > waitTime;
        }
    }
}