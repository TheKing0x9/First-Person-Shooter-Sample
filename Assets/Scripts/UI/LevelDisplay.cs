using UnityEngine;
using UnityEngine.UI;

using FPS.Utilities;

namespace FPS.UI
{
    public class LevelDisplay : MonoBehaviour, IIntEventListener
    {
        [SerializeField] private IntEvent levelUpEvent;

        private Text levelText;

        private void Awake()
        {
            levelUpEvent.Register(this);
            levelText = GetComponent<Text> ();
        }

        private void OnDestroy() => levelUpEvent.Unregister(this);
        public void OnEventRaised(IntEvent e, int level) => levelText.text = level.ToString();
    }
}