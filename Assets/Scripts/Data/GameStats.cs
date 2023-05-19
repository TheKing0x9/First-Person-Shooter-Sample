using UnityEngine;

using FPS.Utilities;
using FPS.Managers;

namespace FPS.Data
{
    [System.Serializable]
    public struct StatStruct
    {
        public int timeElapsed;
        public int scoreGained;
        public int levelsAttained;
        public float healthRemaining;
        public int bestScore;
    }

    [CreateAssetMenu(menuName = "Data/GameStats", fileName = "GameStats")]
    public class GameStats : ScriptableObject, IVoidEventListener, IFloatEventListener, IIntEventListener
    {
        [Header("Scores")]
        [SerializeField] private int scoreMultiplier = 1;
        [SerializeField] private int timeMultipler = 15;
        [SerializeField] private int levelMultiplier = 100;
        [SerializeField] private int healthMultiplier = 1000;

        [Header("Data")]
        public StatStruct thisRun;
        [SerializeField] private SaveData saveData;

        public StatStruct bestRun
        {
            get => saveData.bestRun;
            set {
                saveData.bestRun = value;
            }
        }

        [SerializeField, ReadOnly] private int maxGameTime;

        [Header("Events")]
        [SerializeField] private VoidEvent resetEvent;
        [SerializeField] private IntEvent countdownEvent;
        [SerializeField] private IntEvent levelUpEvent;
        [SerializeField] private FloatEvent addScoreEvent;
        [SerializeField] private FloatEvent healthUpdateEvent;

        private void OnEnable()
        {
            addScoreEvent.Register(this);
            resetEvent.Register(this);
            countdownEvent.Register(this);
            levelUpEvent.Register(this);
            healthUpdateEvent.Register(this);
        }

        private void OnDisable()
        {
            addScoreEvent.Unregister(this);
            resetEvent.Unregister(this);
            countdownEvent.Unregister(this);
            levelUpEvent.Unregister(this);
            healthUpdateEvent.Unregister(this);
        }

        public void OnEventRaised(VoidEvent e) => Reset();
        public void SetGameTime(int time) => maxGameTime = time;
        public void OnEventRaised(IntEvent e, int i) {
            if (e == countdownEvent) thisRun.timeElapsed = maxGameTime - i;
            else if (e == levelUpEvent) thisRun.levelsAttained = i;
        }
        public void OnEventRaised(FloatEvent e, float f) 
        {
            if (e == addScoreEvent) thisRun.scoreGained += (int)f;
            if (e == healthUpdateEvent) thisRun.healthRemaining = f;
        }

        public void Reset()
        {
            thisRun.timeElapsed = 0;
            thisRun.scoreGained = 0;
            thisRun.levelsAttained = 1;
            thisRun.healthRemaining = 1f;
            thisRun.bestScore = 0;
        }

        public StatStruct GetScoredStruct()
        {
            StatStruct s = new StatStruct();

            s.healthRemaining = (int) (thisRun.healthRemaining * healthMultiplier);
            s.levelsAttained = thisRun.levelsAttained * levelMultiplier;
            s.scoreGained = thisRun.scoreGained * scoreMultiplier;
            s.timeElapsed = (thisRun.timeElapsed) * timeMultipler;

            CalculateBestScore(ref s, false);
            return s;
        }

        public void CalculateBestScore(ref StatStruct s, bool multiply = true)
        {
            s.bestScore = s.timeElapsed * (multiply ? timeMultipler : 1)
                        + s.scoreGained * (multiply ? scoreMultiplier : 1)
                        + (int)(s.healthRemaining * (multiply ? healthMultiplier : 1))
                        + s.levelsAttained * (multiply ? levelMultiplier : 1);
        }
    }
}