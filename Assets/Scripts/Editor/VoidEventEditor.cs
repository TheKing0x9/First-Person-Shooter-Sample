using FPS.Utilities;
using UnityEditor;
using UnityEngine;

namespace FPS.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(VoidEvent))]
    public class VoidEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (VoidEvent)target;

            if (GUILayout.Button("Raise", GUILayout.Height(20)))
            {
                script.Raise();
            }
        }
    }
}