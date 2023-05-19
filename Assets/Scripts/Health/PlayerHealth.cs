using UnityEngine;

using FPS.Utilities;

namespace FPS.Health
{
    public class PlayerHealth : BaseHealth
    {
        [Header("Events")]
        [SerializeField] private VoidEvent playerDeathEvent;
        [SerializeField] private FloatEvent playerHealthUpdateEvent;
        [SerializeField] private IntEvent maxHealthEvent;

        protected void Start()
        {
            maxHealthEvent.Raise(maxHealth);
            playerHealthUpdateEvent.Raise((float)currentHealth / (float)maxHealth);
        }

        protected override void OnHit()
        {
            base.OnHit();
            playerHealthUpdateEvent.Raise((float)currentHealth / (float)maxHealth);
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            playerDeathEvent.Raise();
            playerHealthUpdateEvent.Raise((float)currentHealth / (float)maxHealth);
        }
    }
}