using UnityEngine;

namespace FPS.Health
{
    public class BaseHealth : MonoBehaviour
    {
        [Header("Health Values")]
        [SerializeField] protected int maxHealth = 100;

        [Header("Audio")]
        [SerializeField] protected AudioClip hitClip;
        [SerializeField] protected AudioClip deathClip;

        protected int currentHealth;
        protected AudioSource sfxSource;

        public bool isAlive
        {
            get => currentHealth > 0;
        }

        protected virtual void Awake()
        {
            currentHealth = maxHealth;
            sfxSource = GetComponent<AudioSource> ();
        }

        public void TakeDamage(int damage, float knockback = 0f)
        {
            if (currentHealth <= 0f) return;

            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);

            if (currentHealth > 0f)
                OnHit();
            else
                OnDeath();
        }

        protected virtual void OnHit() 
        {
            if (hitClip)
                sfxSource.PlayOneShot(hitClip);
        }

        protected virtual void OnDeath()
        {
            if (deathClip)
                sfxSource.PlayOneShot(deathClip);
        }

        protected void OnValidate()
        {
            currentHealth = maxHealth;
        }
    }
}