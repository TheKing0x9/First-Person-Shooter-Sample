using UnityEngine;
using UnityEngine.UI;

using FPS.Utilities;

namespace FPS.UI
{
    public class SliderUpdate : MonoBehaviour, IFloatEventListener
    {
        [SerializeField] private FloatEvent updateEvent;

        private Slider slider;

        private void Awake ()
        {
            slider = GetComponent<Slider> ();

            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.wholeNumbers = false;

            updateEvent.Register(this);
        }

        private void OnDestroy()
        {
            updateEvent.Unregister(this);
        }

        public void OnEventRaised(FloatEvent e, float f)
        {   
            slider.value = f;
        }
    }
}