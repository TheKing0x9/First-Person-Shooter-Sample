using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

using FPS.Data;
using FPS.Utilities;

namespace FPS.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private VoidEvent gameStartEvent;
        [SerializeField] private SaveData saveData;
        [SerializeField] private AudioMixer masterMixer;
        [SerializeField] private Text soundText;

        private Animator menuAnimator;

        private void Awake()
        {
            menuAnimator = GetComponent<Animator> ();
        }

        public void OnPlayClicked()
        {
            StartCoroutine(InitializePlayMode());
        }

        private IEnumerator InitializePlayMode()
        {
            menuAnimator.SetTrigger("Exit");
            
            yield return new WaitForSeconds(1.25f);

            gameStartEvent.Raise();
            Destroy(gameObject);
        }

        public void ToggleSoundState()
        {
            saveData.soundEnabled = !saveData.soundEnabled;
            masterMixer.SetFloat("masterVol", saveData.soundEnabled ? 0f : -60f);
            soundText.text = "Sound : " + (saveData.soundEnabled ? "On" : "Off");
        }

        public void ShowCredits()
        {
            // Add Credits here
        }

        public void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}