using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using FPS.Data;

namespace FPS.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private GameStats stats;

        [Header("Score Fields")]
        [SerializeField] private Text scoreThisRun;
        [SerializeField] private Text scoreBestRun;

        [Header("Level Fields")]
        [SerializeField] private Text levelThisRun;
        [SerializeField] private Text levelBestRun;

        [Header("Time Fields")]
        [SerializeField] private Text timeThisRun;
        [SerializeField] private Text timeBestRun;

        [Header("Health Fields")]
        [SerializeField] private Text healthThisRun;
        [SerializeField] private Text healthBestRun;

        [Header("Total Score Fields")]
        [SerializeField] private Text totalThisRun;
        [SerializeField] private Text totalBestRun;

        private void FormatAsText(Text t, int i) => t.text = string.Format("{0} Pts", i);

        private void OnEnable()
        {
            stats.CalculateBestScore(ref stats.thisRun);

            StatStruct run = stats.GetScoredStruct();

            FormatAsText(scoreThisRun, run.scoreGained);
            FormatAsText(healthThisRun, (int)run.healthRemaining);
            FormatAsText(levelThisRun, run.levelsAttained);
            FormatAsText(timeThisRun, run.timeElapsed);
            FormatAsText(totalThisRun, run.bestScore);

            run = stats.bestRun;

            FormatAsText(scoreBestRun, run.scoreGained);
            FormatAsText(healthBestRun, (int)run.healthRemaining);
            FormatAsText(levelBestRun, run.levelsAttained);
            FormatAsText(timeBestRun, run.timeElapsed);
            FormatAsText(totalBestRun, run.bestScore);
        }

        public void ReloadGame() 
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}