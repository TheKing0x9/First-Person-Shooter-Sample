using UnityEngine;

using FPS.Utilities;

namespace FPS.Player
{
    public class ExperienceManager : MonoBehaviour, IIntEventListener
    {
        [Header("Collider Parameters")]
        [SerializeField] private float experienceRadius = 10f;
        [SerializeField] private FPS.Data.ExperienceData experienceData;

        [Header("Events")]
        [SerializeField] private IntEvent experienceCollectedEvent;
        [SerializeField] private IntEvent levelUpEvent;
        [SerializeField] private FloatEvent expUpdateEvent;

        [Header("Debug")]
        [SerializeField, ReadOnly] private int collectedExperience;
        [SerializeField, ReadOnly] private int currentLevel;

        private SphereCollider sphereCollider;

        private void Awake()
        {
            experienceCollectedEvent.Register(this);

            sphereCollider = GetComponent<SphereCollider> ();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = experienceRadius;

            collectedExperience = 0;
            currentLevel = 0;
        }

        private void Start()
        {
            levelUpEvent.Raise(currentLevel + 1);
            expUpdateEvent.Raise(collectedExperience / experienceData[currentLevel]);
        }

        private void OnDestroy()
        {
            experienceCollectedEvent.Unregister(this);
        }

        public void OnEventRaised(IntEvent e, int i)
        {
            if (currentLevel >= experienceData.maxLevels - 1) 
            {
                expUpdateEvent.Raise(1f);
                return;
            }

            collectedExperience += i;
            int expRequired = experienceData[currentLevel];

            if (collectedExperience >= expRequired)
            {
                collectedExperience -= expRequired;
                levelUpEvent.Raise(++currentLevel + 1);
                expRequired = experienceData[currentLevel];
            }

            float percent = (float)collectedExperience / (float)expRequired;
            expUpdateEvent.Raise(percent);
        }

        public void UpdateExperienceRadius(float newRadius)
        {
            experienceRadius = newRadius;
            sphereCollider.radius = newRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Experience"))
            {
                var follow = other.gameObject.GetComponent<SimpleFollow> ();
                follow.SetTarget(transform.parent);

                other.gameObject.tag = "Untagged";
            }
        }
    }
}