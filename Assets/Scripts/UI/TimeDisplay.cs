using UnityEngine;
using UnityEngine.UI;

using FPS.Utilities;

namespace FPS.UI
{
    public class TimeDisplay : MonoBehaviour, IIntEventListener
    {
        [SerializeField] private IntEvent countdownEvent;
        
        private Text timeText;

        private void Awake()
        {
            countdownEvent.Register(this);
            timeText = GetComponent<Text> ();
        }

        private void OnDestroy() => countdownEvent.Unregister(this);

        public void OnEventRaised(IntEvent e, int i)
        {
            int m = i / 60;
            int s = i % 60;

            timeText.text = string.Format("{0:00}:{1:00}", m, s);
        }
    }
}