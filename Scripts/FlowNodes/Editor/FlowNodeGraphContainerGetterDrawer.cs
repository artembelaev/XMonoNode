using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XMonoNode;

namespace FlowNodesEditor
{
    [CustomPropertyDrawer(typeof(FlowNodeGraphContainerGetter), true)]
    public class FlowNodeGraphContainerGetterDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property == null)
            {
                return;
            }
            
            EditorGUI.BeginProperty(position, label, property);

            // ¬ключаем/выключаем показ кнопок
            bool buttonsShown = property.FindPropertyRelative("showButtons").boolValue;

            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 2 && position.Contains(e.mousePosition))
            {
                buttonsShown = !buttonsShown;
                property.FindPropertyRelative("showButtons").boolValue = buttonsShown;
            }

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            int pinButtonWidth = buttonsShown ? 30 : 0;
            
            position.width -= pinButtonWidth;

            // Draw path to containers
            string pathToContainers = property.FindPropertyRelative("pathToContainers").stringValue;
            bool drawPathToContainers = property.FindPropertyRelative("drawPathToContainers").boolValue;

            if (drawPathToContainers)
            {
                position.width /= 2; // чтобы влезли: путь к контейнерам, контейнер, Id
                pathToContainers = EditorGUI.TextField(position, new GUIContent("", "Path to containers"), pathToContainers);
                property.FindPropertyRelative("pathToContainers").stringValue = pathToContainers;
                position.x += position.width;
            }
 

            // Draw containers popup
            string[] containersNames = GetContainersNames(pathToContainers);
            
            string containerFileName = property.FindPropertyRelative("containerFileName").stringValue;
            int currentContainerIndex = System.Array.IndexOf(containersNames, containerFileName);
            currentContainerIndex = EditorGUI.Popup(position, currentContainerIndex, containersNames);
            if (currentContainerIndex < 0)
            {
                currentContainerIndex = 0;
            }
            containerFileName = containersNames[currentContainerIndex];
            property.FindPropertyRelative("containerFileName").stringValue = containerFileName;

            // Draw graphId popup
            string slash = pathToContainers.Length > 0 && pathToContainers[pathToContainers.Length - 1] != '/' ? "/" : "";
            string[] graphIds = GetGraphIds(pathToContainers + slash + containerFileName, out FlowNodeGraphContainer container);

            if (container != null && buttonsShown)
            {
                position.x += position.width;
                position.width = pinButtonWidth;
                if (GUI.Button(position, new GUIContent("=>", "Open")))
                {
                    Selection.SetActiveObjectWithContext(container, container);
                    EditorGUIUtility.PingObject(container);
                }
            }

            //int indent = EditorGUI.indentLevel;
            //EditorGUI.indentLevel = 0;

            //EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        protected string[] GetGraphIds(string containerFullName, out FlowNodeGraphContainer container)
        {
            container = ResourcesLoader.Load<FlowNodeGraphContainer>(containerFullName);
            
            if (container == null)
            {
                return new string[1];
            }

            string[] graphIds = new string[container.ItemsList.Count + 1];

            graphIds[0] = "NULL";
            for (int i = 0; i < container.ItemsList.Count; ++i)
            {
                graphIds[i + 1] = container.ItemsList[i].Id;
            }

            return graphIds;
        }

        protected string[] GetContainersNames(string pathToContainers)
        {
            FlowNodeGraphContainer[] containers = Resources.LoadAll<FlowNodeGraphContainer>(pathToContainers);
            string[] containersNames = new string[containers.Length + 1];
            containersNames[0] = FlowNodeGraphContainerGetter.NO_CONTAINER;
            for (int i = 0; i < containers.Length; ++i)
            {
                containersNames[i + 1] = containers[i].name;
            }
            return containersNames;
        }
    }

}
