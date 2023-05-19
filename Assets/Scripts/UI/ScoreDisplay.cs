using UnityEngine;
using UnityEngine.UI;

using FPS.Utilities;

namespace FPS.UI
{
    public class ScoreDisplay : MonoBehaviour, IVoidEventListener, IFloatEventListener
    {
        [SerializeField] private FloatEvent addScoreEvent;
        [SerializeField] private VoidEvent resetScoreEvent;

        [SerializeField] private float speedFactor = 3f;
        [SerializeField] private Text scoreText;

        private float currentlyDisplayedScore = 0f;
        private float scoreToDisplay = 0f;
        private float step = 0f;
        private bool countdown = false;

        public void OnEventRaised(VoidEvent e)
        {
            // reset score display
            currentlyDisplayedScore = scoreToDisplay = 0f;
            countdown = false;
            scoreText.text = string.Format("Score : {0} Pts", currentlyDisplayedScore);
        }

        public void OnEventRaised(FloatEvent e, float value)
        {
            scoreToDisplay = scoreToDisplay + value;
            step = (scoreToDisplay - currentlyDisplayedScore) * Time.deltaTime * speedFactor;
            countdown = (scoreToDisplay - currentlyDisplayedScore) > 0.01f;
        }

        private void Update()
        {
            if (countdown) {
                countdown = (scoreToDisplay - currentlyDisplayedScore) > 0.01f;
                currentlyDisplayedScore = countdown ? currentlyDisplayedScore + step : scoreToDisplay;

                scoreText.text = string.Format("Score : {0} Pts", (int)currentlyDisplayedScore);
            }
        }

        private void Awake() 
        {
            addScoreEvent.Register(this);
            resetScoreEvent.Register(this);

            scoreText = GetComponent<Text> ();
        }

        private void OnDestroy()
        {
            addScoreEvent.Unregister(this);
            resetScoreEvent.Unregister(this);
        }
    }
}