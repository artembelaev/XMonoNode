using UnityEditor;
using UnityEngine;

namespace XMonoNodeEditor {
    /// <summary> Utility for renaming assets </summary>
    public class RenamePopup : EditorWindow {
        private const string inputControlName = "nameInput";

        public static RenamePopup current { get; private set; }
        public Object target;
        public string input;

        private bool firstFrame = true;

        /// <summary> Show a rename popup for an asset at mouse position. Will trigger reimport of the asset on apply.
        public static RenamePopup Show(Object target, float width = 200) {
            RenamePopup window = EditorWindow.GetWindow<RenamePopup>(true, "Rename " + target.name, true);
            if (current != null) current.Close();
            current = window;
            window.target = target;
            XMonoNode.INode node = target as XMonoNode.INode;
            window.input = node.Name;
            window.minSize = new Vector2(100, 44);
            window.position = new Rect(0, 0, width, 44);
            window.UpdatePositionToMouse();
            return window;
        }

        private void UpdatePositionToMouse() {
            if (Event.current == null) return;
            Vector3 mousePoint = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            Rect pos = position;
            pos.x = mousePoint.x - position.width * 0.5f;
            pos.y = mousePoint.y - 10;
            position = pos;
        }

        private void OnLostFocus() {
            // Make the popup close on lose focus
            Close();
        }

        private void OnGUI() {
            if (firstFrame) {
                UpdatePositionToMouse();
                firstFrame = false;
            }
            GUI.SetNextControlName(inputControlName);
            input = EditorGUILayout.TextField(input);
            EditorGUI.FocusTextInControl(inputControlName);
            Event e = Event.current;
            // If input is empty, revert name to default instead
            if (input == null || input.Trim() == "")
            {
                if (GUILayout.Button("Revert to default") || (e.isKey && e.keyCode == KeyCode.Return))
                {
                    RenameNode(NodeEditorUtilities.NodeDefaultName(target.GetType()));
                }
            }
            else // Rename asset to input text
            {
                if (GUILayout.Button("Apply") || (e.isKey && e.keyCode == KeyCode.Return))
                {
                    RenameNode(input);
                }
            }

            if (e.isKey && e.keyCode == KeyCode.Escape) {
                Close();
            }
        }

        private void RenameNode(string name)
        {
            Undo.RecordObject(target, "Rename node");
            XMonoNode.INode node = target as XMonoNode.INode;
            node.Name = name;
            NodeEditor.GetEditor(node, NodeEditorWindow.current).OnRename();
            string assetPath = AssetDatabase.GetAssetPath(target);
            if (assetPath.Length != 0 && EditorUtility.IsPersistent(node.Graph as Object) == false)
            {
                AssetDatabase.SetMainObject(node.Graph as Object, assetPath);
                AssetDatabase.ImportAsset(assetPath);
            }
            Close();
            target.TriggerOnValidate();
        }

        private void OnDestroy() {
            EditorGUIUtility.editingTextField = false;
        }
    }
}