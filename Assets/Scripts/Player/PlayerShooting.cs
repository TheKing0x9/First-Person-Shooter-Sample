using UnityEngine;

using FPS.Health;

namespace FPS.Player 
{
    
    public class PlayerShooting : MonoBehaviour
    {
        [Header("Weapon Parameters")]
        [SerializeField] private int damagePerShot = 20;
        [SerializeField] private float fireRate = 0.15f;
        [SerializeField] private float range = 100f;

        [Header("Ray Fields")]
        [SerializeField] private LayerMask shootableMask;
        [SerializeField] private Transform muzzleTransform;
        [SerializeField] private Transform cameraTransform;

        [Header("Effects")]
        [SerializeField] private LineRenderer gunLine;
        [SerializeField] private AudioSource gunAudio;
        [SerializeField] private AudioClip shotClip;
        [SerializeField] private ParticleSystem gunParticles;
        [SerializeField] private Light gunLight;
        [SerializeField] private float effectsDisplayTime = 0.2f;
        [SerializeField] private GameObject hitEffects;

        private float timer;
        private Ray shootRay = new Ray();
        private RaycastHit hit;
        private StarterAssetsInputs inputs;

        private void Awake()
        {
            inputs = GetComponent<StarterAssetsInputs> ();
        }

        private void Update() 
        {
            timer += Time.deltaTime;

            if (timer >= fireRate && Time.timeScale != 0f && inputs.fire)
            {
                Shoot();
            }

            if (timer >= effectsDisplayTime)
            {
                DisableEffects();
            }

            gunLine.SetPosition (0, muzzleTransform.position);
        }

        private void DisableEffects() 
        {
            // Do Nothing for now
            gunLight.enabled = false;
            gunLine.enabled = false;
        }

        private void Shoot()
        {
            timer = 0f;

            gunLight.enabled = true;

            gunAudio.PlayOneShot(shotClip);

            gunParticles.Stop();
            gunParticles.Play();

            gunLine.enabled = true;
            
            shootRay.origin = cameraTransform.position;
            shootRay.direction = cameraTransform.forward;

            if (Physics.Raycast(shootRay, out hit, range, shootableMask))
            {
                BaseHealth health = hit.collider.GetComponent<BaseHealth> ();
                if (health != null)
                {
                    health.TakeDamage(damagePerShot);
                }

                gunLine.SetPosition (1, hit.point);
                Instantiate(hitEffects, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            }
        }

    }
}