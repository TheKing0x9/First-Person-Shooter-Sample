using UnityEngine;

namespace FPS.Utilities
{
    public class RandomAnimationPoint : MonoBehaviour
    {
        public bool randomize;
        [Range(0f, 1f)] public float normalizedTime;


        void OnValidate()
        {
            if (!gameObject.activeInHierarchy) return;

            GetComponent<Animator>().Update(0f);
            GetComponent<Animator>().Play("Move", 0, randomize ? Random.value : normalizedTime);
        }
    }
}
