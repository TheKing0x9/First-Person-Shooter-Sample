using UnityEngine;
using System.Collections;

using FPS.Utilities;
using FPS.Data;

namespace FPS.Managers
{
    public class GameManager : Singleton<GameManager>, IVoidEventListener
    {
        [Header("Timer")]
        [SerializeField] private int m_gameTime = 120;
        [SerializeField, ReadOnly] private int elapsedTime;

        [Header("Audio")]
        [SerializeField] private AudioClip m_victoryClip;
        [SerializeField] private AudioClip m_defeatClip;

        [Header("References")]
        [SerializeField] private GameObject m_player;
        [SerializeField] private Transform m_experienceParent;
        [SerializeField] private GameObject m_gameOverUI;
        [SerializeField] private GameObject m_fpsCamera;
        [SerializeField] private GameObject m_menuCamera;
        [SerializeField] private GameObject m_hudUI;
        [SerializeField] private GameStats stats;
        // the only hard reference in the game
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private SaveData saveData;
        [SerializeField] private GameObject weapon;

        [Header("Events")]
        [SerializeField] private VoidEvent resetScoreEvent;
        [SerializeField] private VoidEvent playerDeadEvent;
        [SerializeField] private VoidEvent timeElapsedEvent;
        [SerializeField] private VoidEvent gameOverEvent;
        [SerializeField] private VoidEvent gameStartEvent;
        [SerializeField] private VoidEvent killAllEvent;
        [SerializeField] private IntEvent countdownEvent;

        [Header("Camera")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask fpsMask;
        [SerializeField] private LayerMask normalMask = 1;

        public GameObject player { get => m_player; }
        public Transform experienceParent { get => m_experienceParent; }

        private Coroutine timerRoutine;
        private AudioSource audioSource;
        private JsonSaver saver = new JsonSaver();

        protected override void Awake()
        {
            // Initialize Singleton
            base.Awake();

            // load save data
            saver.Load(saveData);

            // Store a reference to the player gameobject if not already assigned
            if (m_player == null) m_player = GameObject.FindWithTag("Player");

            // reset elapsed time
            elapsedTime = m_gameTime;

            // subscribe to player dead event;
            playerDeadEvent.Register(this);

            // subscribe to game start event
            gameStartEvent.Register(this);

            // disable gameover ui
            m_gameOverUI.SetActive(false);

            // set max game time
            stats.SetGameTime(m_gameTime);

            // disable the fps camera
            m_fpsCamera.SetActive(false);

            // grab reference to audio source
            audioSource = GetComponent<AudioSource> ();

            // set camera to see all layers
            mainCamera.cullingMask = normalMask;

            // switch weapon off
            weapon.SetActive(m_fpsCamera.activeSelf);
        }

        private void Start()
        {
            // Fire the reset score event
            resetScoreEvent.Raise();
        }

        private IEnumerator Countdown()
        {
            countdownEvent.Raise((int)elapsedTime);

            while (true)
            {
                yield return new WaitForSeconds(1f);
                countdownEvent.Raise((int)--elapsedTime);

                if (elapsedTime <= 0f)
                {
                    GameOver();
                    timeElapsedEvent.Raise();
                    break;
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            playerDeadEvent.Unregister(this);
            gameStartEvent.Unregister(this);

            // check for best run
            if (stats.thisRun.bestScore > stats.bestRun.bestScore)
                stats.bestRun = stats.GetScoredStruct();

            Save();
        }

        public void OnEventRaised(VoidEvent e)
        {
            if (e == playerDeadEvent)
            {
                GameOver(false);
                StopCoroutine(timerRoutine);
            }
            else if (e == gameStartEvent)
            {
                GameStart();
            }
        }

        private void GameOver(bool won = true)
        {
            gameOverEvent.Raise();

            StartCoroutine(OnGameOver(won));
        }

        private void GameStart()
        {
            timerRoutine = StartCoroutine(Countdown());
            m_hudUI.SetActive(true);
            m_fpsCamera.SetActive(true);
            m_menuCamera.SetActive(false);
            mainCamera.cullingMask = fpsMask;
            weapon.SetActive(m_fpsCamera.activeSelf);
        }

        private IEnumerator OnGameOver(bool won = true)
        {
            m_hudUI.SetActive(false);

            yield return new WaitForSeconds(0.1f);

            // Disable the fps camera so that the scene switches to iso view
            m_fpsCamera.SetActive(false);
            weapon.SetActive(m_fpsCamera.activeSelf);
            mainCamera.cullingMask = normalMask;
            
            yield return new WaitForSeconds(1f);

            if (won)
            {
                // raise the kill all event
                killAllEvent.Raise();
                audioSource.PlayOneShot(m_victoryClip);
            } else {
                playerAnimator.SetTrigger("Die");
                audioSource.PlayOneShot(m_defeatClip);
            }

            yield return new WaitForSeconds(4f);

            m_gameOverUI.SetActive(true);
        }

        public void Save()
        {
            saver.Save(saveData);
        }
    }
}