using FPS.Utilities;
using UnityEditor;
using UnityEngine;

namespace FPS.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(IntEvent))]
    public class IntEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (IntEvent)target;

            if (GUILayout.Button("Raise", GUILayout.Height(20)))
            {
                script.Raise(script.defaultValue);
            }
        }
    }
}