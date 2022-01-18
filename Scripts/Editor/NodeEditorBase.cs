using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
#endif

namespace XMonoNodeEditor.Internal {
    /// <summary> Handles caching of custom editor classes and their target types. Accessible with GetEditor(Type type) </summary>
    /// <typeparam name="T">Editor Type. Should be the type of the deriving script itself (eg. NodeEditor) </typeparam>
    /// <typeparam name="A">Attribute Type. The attribute used to connect with the runtime type (eg. CustomNodeEditorAttribute) </typeparam>
    /// <typeparam name="K">Runtime Type. The UnityEngine.Object this can be an editor for (eg. Node) </typeparam>
    public abstract class NodeEditorBase<T, A, K> where A : Attribute, NodeEditorBase<T, A, K>.INodeEditorAttrib where T : NodeEditorBase<T, A, K> where K : class {
		/// <summary> Custom editors defined with [CustomNodeEditor] </summary>
		private static Dictionary<Type, Type> editorTypes;
		private static Dictionary<K, T> editors = new Dictionary<K, T>();

        public K Target
        {
            get; set;
        }

        public NodeEditorWindow window;
		public SerializedObject serializedObject;
#if ODIN_INSPECTOR
		private PropertyTree _objectTree;
		public PropertyTree objectTree {
			get {
                if (this._objectTree == null){
					try {
						bool wasInEditor = NodeEditor.inNodeEditor;
						NodeEditor.inNodeEditor = true;
						this._objectTree = PropertyTree.Create(this.serializedObject);
						NodeEditor.inNodeEditor = wasInEditor;
					} catch (ArgumentException ex) {
						Debug.Log(ex);
					}
				}
				return this._objectTree;
			}
		}
#endif

		public static T GetEditor(K target, NodeEditorWindow window) {
			if ((target as UnityEngine.Object) == null) return null;
			T editor;
            UnityEngine.Object obj = target as UnityEngine.Object;
            
            if (!editors.TryGetValue(target, out editor)) {
				Type type = target.GetType();
                Type editorType = GetEditorType(type);
				editor = Activator.CreateInstance(editorType) as T;
				editor.Target = target;
				editor.serializedObject = obj != null ? new SerializedObject(obj) : null;
				editor.window = window;
				editor.OnCreate();
				editors.Add(target, editor);
			}
			if ((editor.Target as UnityEngine.Object) == null) editor.Target = target;
			if (editor.window != window) editor.window = window;
			if (editor.serializedObject == null) editor.serializedObject = obj != null ? new SerializedObject(obj) : null;
			return editor;
		}

        public static void DestroyEditor( K target )
        {
            if ((target as UnityEngine.Object) == null ) return;
            T editor;
            if ( editors.TryGetValue( target, out editor ) )
            {
                editors.Remove( target );
            }
        }

		private static Type GetEditorType(Type type) {
            if (type == null) return null;
            if (editorTypes == null) CacheCustomEditors();
            Type result;
			if (editorTypes.TryGetValue(type, out result)) return result;
            //If type isn't found, try base type
            var baseTypeEditor = GetEditorType(type.BaseType);
            if (baseTypeEditor != null)
            {
                return baseTypeEditor;
            }

            //If base type isn't found, try interfaces
            var interfaces = type.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                var editorType = GetEditorType(interfaces[i]);
                if (editorType != null)
                {
                    return editorType;
                }
            }

            return null;
        }

		private static void CacheCustomEditors()
        {
			editorTypes = new Dictionary<Type, Type>();

            //Get all classes deriving from NodeEditor via reflection
            Type[] nodeEditors = typeof(T).GetDerivedTypes();
            for (int i = 0; i < nodeEditors.Length; i++)
            {
                if (nodeEditors[i].IsAbstract)
                    continue;
                var attribs = nodeEditors[i].GetCustomAttributes(typeof(A), false);
                if (attribs == null || attribs.Length == 0)
                    continue;
                A attrib = attribs[0] as A;
                editorTypes[attrib.GetInspectedType()] = nodeEditors[i];
            }
        }

        /// <summary> Called on creation, after references have been set </summary>
        public virtual void OnCreate() { }

		public interface INodeEditorAttrib {
			Type GetInspectedType();
		}
	}
}