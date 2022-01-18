using System;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    [System.Serializable]
    public class FlowNodeGraphGetter
    {
        public static string NO_CONTAINER = "-: None";

        [SerializeField]
        private bool showButtons = true;

        // Common usage: link to FlowNodeGraph container item by graphId
        [SerializeField]
        private string          graphId = "";
        [SerializeField]
        private string          pathToContainers = "";
        [SerializeField]
        private string          containerFileName = "";
        

        [SerializeField]
        protected bool          drawPathToContainers = true;

        public string GraphId => graphId;

        // alternative usage - link to prefab directly
        [SerializeField]
        private FlowNodeGraph   graphPrefab = null;

        public bool IsEmpty
        {
            get => (string.IsNullOrEmpty(containerFileName) || containerFileName == NO_CONTAINER) ? graphPrefab == null : (string.IsNullOrEmpty(graphId) || string.IsNullOrEmpty(FullPath));
        }

        public string PathToContainers
        {
            get => pathToContainers;
            set
            {
                pathToContainers = value;
            }
        }

        private string FullPath
        {
            get
            {
              return pathToContainers +
                    (pathToContainers.EndsWith("/") ? "" : "/") +
                    containerFileName;
            }
        }

        public bool ShowButtons
        {
            get => showButtons;
            set => showButtons = value;
        }
        public string ContainerFileName
        {
            get => containerFileName;
            set => containerFileName = value;
        }

        public FlowNodeGraphGetter()
        {
            this.showButtons = true;
        }

        public FlowNodeGraphGetter(bool showButtons = true)
        {
            this.showButtons = showButtons;
        }

        public FlowNodeGraphGetter(bool showButtons, string pathToContainers, string containerFileName = "", string graphId = "")
        {
            this.showButtons = showButtons;
            this.pathToContainers = pathToContainers;
            this.containerFileName = containerFileName;
            this.graphId = graphId;
        }

        public FlowNodeGraphGetter(string pathToContainers, string containerFileName = "", string graphId = "")
        {
            this.pathToContainers = pathToContainers;
            this.containerFileName = containerFileName;
            this.graphId = graphId;
        }

        public FlowNodeGraphGetter(FlowNodeGraph graphPrefab)
        {
            this.graphPrefab = graphPrefab;
        }

        private FlowNodeGraphContainer instanciatedContainer = null;



        public FlowNodeGraphContainer GetContainer(Transform parent = null)
        {
            if (instanciatedContainer == null)
            {
                var resourceContainer = ResourcesLoader.Load<FlowNodeGraphContainer>(FullPath);
                if (resourceContainer != null)
                {
                    if (Application.isPlaying && !resourceContainer.IsStatic)
                    {
                        instanciatedContainer = InstanciateContainer(parent, resourceContainer);
#if UNITY_EDITOR
                        if (Application.isEditor)
                        {
                            instanciatedContainer.gameObject.hideFlags = HideFlags.HideAndDontSave;
                        }
#endif
                    }
                    else
                    {
                        instanciatedContainer = resourceContainer;
                    }
                }
            }
            return instanciatedContainer;
        }

        private FlowNodeGraphContainer InstanciateContainer(Transform parent, FlowNodeGraphContainer res)
        {
            FlowNodeGraphContainer result = null;
            if (res.IsStatic)
            {

            }
            else
            {
                if (parent != null)
                {
                    result = GameObject.Instantiate(res, parent);
                    result.transform.localPosition = Vector3.zero;
                }
                else
                {
                    result = GameObject.Instantiate(res);
                }
            }
            return result;
        }

        protected bool CheckContainer()
        {
            if (GetContainer() != null)
            {
                return true;
            }
            Debug.LogErrorFormat("Container is null, {0}", FullPath);
            return false;
        }

        public void Flow(System.Action<string> onEndAction, string state, params object[] parameters)
        {
            if (CheckContainer())
            {
                FlowNodeGraphContainer container = GetContainer();
                container.GraphParent = parameters.Get<Transform>();
                container.Flow(graphId, onEndAction, state, parameters);
            }
        }

        public void Flow(Transform parent, Dictionary<string, object> parameters)
        {
            if (CheckContainer())
            {
                FlowNodeGraphContainer container = GetContainer();
                container.GraphParent = parent;
                container.Flow(graphId, parameters);
            }
        }

        public void Flow(Transform parent, System.Action<string> onEndAction, string state, Dictionary<string, object> parameters)
        {
            if (CheckContainer())
            {
                FlowNodeGraphContainer container = GetContainer();
                container.GraphParent = parent;
                container.Flow(graphId, onEndAction, state, parameters);
            }
        }

        public void Flow(params object[] parameters)
        {
            if (CheckContainer())
            {
                FlowNodeGraphContainer container = GetContainer();
                container.GraphParent = parameters.Get<Transform>();
                container.Flow(graphId, parameters);
            }
        }

        public void Flow(Dictionary<string, object> parameters)
        {
            if (CheckContainer())
            {
                GetContainer().Flow(graphId, parameters);
            }
        }

        public void Flow(System.Action<string> onEndAction, string state, Dictionary<string, object> parameters)
        {
            if (CheckContainer())
            {
                GetContainer().Flow(graphId, onEndAction, state, parameters);
            }
        }

        public void CustomEvent(string eventName)
        {
            if (CheckContainer())
            {
                GetContainer().CustomEvent(graphId, eventName);
            }
        }

        public void UpdateInputParameters(Transform parent = null, params object[] parameters)
        {
            if (CheckContainer())
            {
                FlowNodeGraphContainer container = GetContainer();
                container.GraphParent = parent;
                container.UpdateInputParameters(graphId, parent, parameters);
            }
        }

        [Obsolete]
        public void UpdateInputParameters(params object[] parameters)
        {
            if (CheckContainer())
            {
                FlowNodeGraphContainer container = GetContainer();
                container.GraphParent = parameters.Get<Transform>();
                container.UpdateInputParameters(graphId, container.GraphParent, parameters);
            }
        }

        public void UpdateInputParameters(Dictionary<string, object> parameters)
        {
            if (CheckContainer())
            {
                GetContainer().UpdateInputParameters(graphId, parameters);
            }
        }

        public void GetOutputParameters(out Dictionary<string, object> parameters)
        {
            if (CheckContainer())
            {
                GetContainer().GetOutputParameters(graphId, out parameters);
            }
            else
            {
                parameters = new Dictionary<string, object>();
            }
        }

        public void Stop()
        {
            if (GetContainer() == null)
            {
                Debug.LogErrorFormat("Container is null, {0}", FullPath);
                return;
            }

            GetContainer().Stop(graphId);
        }

    }

    public static class FlowNodeGetterExtensionMethods
    {
        public static void SafeFlow(this FlowNodeGraphGetter getter, params object[] parameters)
        {
            if (getter != null && !getter.IsEmpty)
            {
                getter.Flow(parameters);
            }
        }

        public static void SafeFlow(this FlowNodeGraphGetter getter, System.Action<string> onEndAction, string state, params object[] parameters)
        {
            if (getter != null && !getter.IsEmpty)
            {
                getter.Flow(onEndAction, state, parameters);
            }
        }

        public static void SafeFlow(this FlowNodeGraphGetter getter, Dictionary<string, object> parameters)
        {
            if (getter != null && !getter.IsEmpty)
            {
                getter.Flow(parameters);
            }
        }

        public static void SafeFlow(this FlowNodeGraphGetter getter, System.Action<string> onEndAction, string state, Dictionary<string, object> parameters)
        {
            if (getter != null && !getter.IsEmpty)
            {
                getter.Flow(onEndAction, state, parameters);
            }
        }
    }
}