using UnityEngine;

namespace FPS.Data
{
    [CreateAssetMenu(menuName = "Data/SaveData", fileName = "Save Data")]
    [System.Serializable]
    public class SaveData : ScriptableObject
    {
        public bool soundEnabled = true;

        public StatStruct bestRun;
    }
}