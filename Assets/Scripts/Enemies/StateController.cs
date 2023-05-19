using UnityEngine;
using UnityEngine.AI;

using FPS.Managers;
using FPS.Utilities;
using FPS.Health;

namespace FPS.Enemies
{
    public class StateController : MonoBehaviour, IVoidEventListener
    {
        [Header("States")]
        [SerializeField] private State remainState;
        [SerializeField] private State currentState;

        [Header("Events")]
        [SerializeField] private VoidEvent playerDeathEvent;
        public FloatEvent addScoreEvent;

        [Header("Fields")]
        public float sinkSpeed = 2.5f;
        public float scoreToAdd = 100f;

        [Header("Debug")]
        [ReadOnly] public bool dead;
        [ReadOnly] public bool playerDead;
        [ReadOnly] public float stateTimeElapsed;
        [ReadOnly] public GameObject chaseTarget;

        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Animator animator;
        [HideInInspector] public BaseHealth enemyHealth;

        private void Start()
        {
            stateTimeElapsed = 0f;

            playerDeathEvent.Register(this);

            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            enemyHealth = GetComponent<BaseHealth>();

            chaseTarget = GameManager.instance.player;
        }

        private void OnDestroy()
        {
            playerDeathEvent.Unregister(this);
        }

        private void Update()
        {
            currentState.OnStateUpdate(this);

            stateTimeElapsed += Time.deltaTime;
        }

        public void TransitionToState(State newState)
        {
            if (newState == remainState) return;

            currentState.OnStateExit(this);
            currentState = newState;
            currentState.OnStateEnter(this);

            stateTimeElapsed = 0f;
        }

        public void OnEventRaised(VoidEvent e)
        {
            playerDead = true;
        }
    }
}