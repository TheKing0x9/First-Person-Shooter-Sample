using FPS.Utilities;
using UnityEditor;
using UnityEngine;

namespace FPS.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(FloatEvent))]
    public class FloatEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (FloatEvent)target;

            if (GUILayout.Button("Raise", GUILayout.Height(20)))
            {
                script.Raise(script.defaultValue);
            }
        }
    }
}