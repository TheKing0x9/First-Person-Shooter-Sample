using UnityEngine;

using FPS.Managers;
using FPS.Utilities;

namespace FPS.Health
{
    public class EnemyHealth : BaseHealth, IVoidEventListener
    {
        [Header("Text Effects")]
        [SerializeField] private GameObject floatingTextPrefab;
        [SerializeField] private Vector3 floatingTextOffset = new Vector3(0, 2, 0);
        [SerializeField] private Vector3 floatingTextRandomize = new Vector3(0.5f, 0, 0);
        [SerializeField] private float floatingTextLifetime = 1f;
        [SerializeField] private Color floatingTextColor;

        [Header("Experience")]
        [SerializeField] private GameObject experiencePrefab;
        [SerializeField] private Vector3 experienceOffset = new Vector3(0, 0.75f, 0);
        [SerializeField] private int experienceToAdd = 3;
        [SerializeField] private Material experienceMaterial;

        [Space(20)]
        [SerializeField] private VoidEvent killAllEvent;

        protected override void Awake()
        {
            base.Awake();
            killAllEvent.Register(this);
        }

        protected virtual void OnDestroy() => killAllEvent.Unregister(this);

        public void OnEventRaised(VoidEvent e)
        {
            TakeDamage(maxHealth);
        }

        protected override void OnHit()
        {
            base.OnHit();
            SpawnFloatingText();
        }

        protected void SpawnFloatingText() 
        {
            GameObject text = Instantiate(floatingTextPrefab, transform.position + floatingTextOffset, transform.rotation * Quaternion.Euler(0, 180f, 0), transform);
            TextMesh mesh = text.GetComponent<TextMesh> ();

            text.transform.localPosition += new Vector3(Random.Range(-floatingTextRandomize.x, floatingTextRandomize.x),
                                                        Random.Range(-floatingTextRandomize.y, floatingTextRandomize.y), 
                                                        Random.Range(-floatingTextRandomize.z, floatingTextRandomize.z));
            mesh.text = currentHealth.ToString();
            mesh.color = floatingTextColor;

            Destroy(text, floatingTextLifetime);
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            SpawnExperiencePickup();
            GetComponent<Collider>().enabled = false;
        }

        protected void SpawnExperiencePickup()
        {
            GameObject pickup = Instantiate(experiencePrefab, transform.position + experienceOffset, Quaternion.identity, GameManager.instance.experienceParent);

            FPS.Pickups.ExperiencePickup exp = pickup.GetComponent<FPS.Pickups.ExperiencePickup> ();
            exp.Setup(experienceToAdd, experienceMaterial);
        }
    }
}