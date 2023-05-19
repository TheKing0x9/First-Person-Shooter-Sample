using UnityEngine;

namespace FPS.Utilities
{
    public class SimpleFollow : MonoBehaviour
    {
        [Header("Speed Parameters")]
        [SerializeField] private float minModifier = 7f;
        [SerializeField] private float maxModifier = 11f;
        [SerializeField] private bool autoDestroy = false;
        [SerializeField] private float closeUpDistance = 0.1f;
        
        [Header("Debug")]
        [SerializeField, ReadOnly] private bool canFollow = false;
        [SerializeField, ReadOnly] private Transform target;
        
        private Vector3 velocity = Vector3.zero;

        private void Awake()
        {
            target = null;
            canFollow = false;
        }

        private void Update()
        {
            if (canFollow && target)
            {  
                transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));

                if (autoDestroy)
                {
                    float distance = Vector3.SqrMagnitude(transform.position - target.position);
                    if (distance < closeUpDistance * closeUpDistance)
                        Destroy(gameObject);
                }
            }
        }

        public void EnableFollow()
        {
            canFollow = true;
        }

        public void SetTarget(Transform t)
        {
            target = t;
            GetComponent<Collider>().isTrigger = true;
        }
    }
}