using UnityEditor;
using UnityEngine;
using System;
using XMonoNode;

namespace XMonoNodeEditor {
    [UnityEditor.CustomEditor(typeof(MonoNodeGraph), true)]
    public class MonoNodeInspector : Editor
    {
        public MonoNodeGraph MonoGraph => target as MonoNodeGraph;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Open graph", GUILayout.Height(40)))
            {
                OpenGraph();
            }
        }

        public void OpenGraph()
        {
            NodeEditorWindow.Open(MonoGraph);
        }
    }
}
