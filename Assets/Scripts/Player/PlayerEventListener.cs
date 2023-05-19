using UnityEngine;

using FPS.Utilities;

namespace FPS.Player
{
    public class PlayerEventListener : MonoBehaviour, IVoidEventListener
    {
        [SerializeField] private VoidEvent gameStartEvent;
        [SerializeField] private VoidEvent gameOverEvent;

        private FirstPersonController controller;
        private PlayerShooting shooting;
        private StarterAssetsInputs inputs;

        private void Awake()
        {
            controller = GetComponent<FirstPersonController> ();
            shooting = GetComponent<PlayerShooting> ();
            inputs = GetComponent<StarterAssetsInputs> ();

            SetScriptState();

            gameOverEvent.Register(this);
            gameStartEvent.Register(this);
        }
        
        private void SetScriptState(bool state = false)
        {
            controller.enabled = state;
            shooting.enabled = state;

            inputs.cursorLocked = state;
        }

        private void OnDestroy()
        {
            gameOverEvent.Unregister(this);
            gameStartEvent.Unregister(this);
        }

        public void OnEventRaised(VoidEvent e)
        {
            if (e == gameOverEvent) SetScriptState(false);
            else SetScriptState(true);
        }
    }
}