using UnityEngine;

using FPS.Utilities;

namespace FPS.Pickups
{
    public class ExperiencePickup : MonoBehaviour
    {
        public int experienceToAdd;
        [SerializeField] private MeshRenderer sphereMeshRenderer;
        [SerializeField] private IntEvent experienceCollectedEvent;

        public void Setup(int exp, Material expMaterial)
        {
            experienceToAdd = exp;
            sphereMeshRenderer.material = expMaterial;
        }

        private void OnDestroy()
        {
            experienceCollectedEvent.Raise(experienceToAdd);
        }
    }
}