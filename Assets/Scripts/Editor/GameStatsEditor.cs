using UnityEditor;
using UnityEngine;

using FPS.Data;

namespace FPS.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(GameStats))]
    [InitializeOnLoad]
    public class GameStatsEditor : Editor
    {
        private GameStats script;

        public GameStatsEditor()
        {
            EditorApplication.playModeStateChanged += OnExitPlayMode;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            script = (GameStats)target;

            GUILayout.Space(20);
            if (GUILayout.Button("Calculate Score", GUILayout.Height(20)))
            {
                script.CalculateBestScore(ref script.thisRun);
                script.bestRun = script.GetScoredStruct();
            }
        }

        private void OnExitPlayMode(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
                script.Reset();
        }
    }
}