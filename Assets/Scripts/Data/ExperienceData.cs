using UnityEngine;

namespace FPS.Data
{
    [CreateAssetMenu(menuName = "Data/Experience", fileName = "ExperienceData")]
    public class ExperienceData : ScriptableObject
    {
        [SerializeField] private int expPerLevel = 6;
        [SerializeField, ReadOnly] private int[] levels = new int[9];

        [ContextMenu("Populate Levels")]
        public void PopulateLevelData() 
        {
            for (int i = 0; i < maxLevels; i++)
            {
                levels[i] = expPerLevel * (1 + i);
            }
        }

        public int this[int index] => levels[index];
        public int maxLevels => 9;
    }
}