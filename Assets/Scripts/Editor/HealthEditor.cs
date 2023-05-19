using UnityEditor;
using UnityEngine;

using FPS.Health;

namespace FPS.Editor
{
    [CustomEditor(typeof(BaseHealth), true)]
    public class HealthEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (BaseHealth)target;

            GUILayout.Space(20f);
            if (GUILayout.Button("Take Damage", GUILayout.Height(20)))
            {
                script.TakeDamage(20);
            }
        }
    }
}