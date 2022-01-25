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


        private FlowNodeGraph   graph = null;

        public bool IsEmpty
        {
            get => string.IsNullOrEmpty(graphId) || string.IsNullOrEmpty(FullPath);
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

        private FlowNodeGraphContainer instanciatedContainer = null;

        private static Dictionary<FlowNodeGraphContainer, FlowNodeGraphContainer> instances = new Dictionary<FlowNodeGraphContainer, FlowNodeGraphContainer>();

        protected virtual Transform GetContainerParent()
        {
            return null;
        }

        public FlowNodeGraphContainer GetContainer(Transform parent = null)
        {
            if (instanciatedContainer == null)
            {
                FlowNodeGraphContainer loadedContainer = ResourcesLoader.Load<FlowNodeGraphContainer>(FullPath);
                if (loadedContainer != null)
                {
                    instanciatedContainer = InstanciateContainer(loadedContainer);
#if UNITY_EDITOR
                    if (Application.isEditor)
                    {
                        instanciatedContainer.gameObject.hideFlags = HideFlags.DontSave;
                        if (!Application.isPlaying)
                        {
                            instanciatedContainer.CreatePoolRoot();
                        }
                    }
#endif
                    if (parent == null)
                    {
                        parent = GetContainerParent();
                    }
                    if (parent != null)
                    {
                        instanciatedContainer.transform.SetParent(parent);
                    }
                    instanciatedContainer.transform.localPosition = Vector3.zero;
                }
            }
            return instanciatedContainer;
        }

        private FlowNodeGraphContainer InstanciateContainer(FlowNodeGraphContainer loadedContainer)
        {
            if (instances.TryGetValue(loadedContainer, out FlowNodeGraphContainer cachedContainer))
            {
                return cachedContainer;
            }
            else
            {
                FlowNodeGraphContainer newContainer = GameObject.Instantiate(loadedContainer);
                instances.Add(loadedContainer, newContainer);
                return newContainer;
            }
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
            graph = GetGraph(parameters.Get<Transform>());
            if (graph != null)
            {
                graph.Flow(onEndAction, state, parameters);
            }
        }

        public void Flow(Transform parent, Dictionary<string, object> parameters)
        {
            graph = GetGraph(parent);
            if (graph != null)
            {
                graph.Flow(parameters);
            }
        }

        public void Flow(Transform parent, System.Action<string> onEndAction, string state, Dictionary<string, object> parameters)
        {
            graph = GetGraph(parent);
            if (graph != null)
            {
                graph.Flow(onEndAction, state, parameters);
            }
        }

        public void Flow(params object[] parameters)
        {
            graph = GetGraph(parameters.Get<Transform>());
            if (graph != null)
            {
                graph.Flow(parameters);
            }
        }

        public void Flow(Dictionary<string, object> parameters)
        {
            graph = GetGraph();
            if (graph != null)
            {
                graph.Flow(graphId, parameters);
            }
        }

        public void Flow(System.Action<string> onEndAction, string state, Dictionary<string, object> parameters)
        {
            graph = GetGraphFromPool();
            if (graph != null)
            {
                graph.Flow(graphId, onEndAction, state, parameters);
            }
        }

        public void CustomEvent(string eventName)
        {
            graph = GetGraph();
            if (graph != null)
            {
                graph.CustomEvent(eventName);
            }
        }

        public void UpdateInputParameters(Transform parent = null, params object[] parameters)
        {
            graph = GetGraph(parent);
            if (graph != null)
            {
                graph.UpdateInputParameters(parameters);
            }
        }

        [Obsolete]
        public void UpdateInputParameters(params object[] parameters)
        {
            Transform parent = parameters.Get<Transform>();
            graph = GetGraph(parent);
            if (graph != null)
            {
                graph.UpdateInputParameters(parameters);
            }
        }

        public void UpdateInputParameters(Dictionary<string, object> parameters)
        {
            graph = GetGraph();
            if (graph != null)
            {
                graph.UpdateInputParameters(parameters);
            }
        }

        public void GetOutputParameters(out Dictionary<string, object> parameters)
        {
            graph = GetGraph();
            if (graph != null)
            {
                graph.GetOutputParameters(out parameters);
            }
            else
            {
                parameters = new Dictionary<string, object>();
            }
        }

        public void Stop()
        {
            graph = GetGraph();
            if (graph != null)
            {
                GetContainer().Stop(graph);
            }           
        }

        private FlowNodeGraph GetGraphFromPool(Transform graphParent = null)
        {
            if (CheckContainer())
            {
                FlowNodeGraphContainer container = GetContainer();
                graph = container.Get(graphId, graphParent);
                if (graph != null)
                {
                    graph.FlowId++;
                    flowId = graph.FlowId;
                    if (graph.transform.parent != graphParent)
                    {
                        graph.transform.SetParent(graphParent);
                    }
                }
                else
                {
                    flowId = -1;
                }
            }
            return graph;
        }

        private FlowNodeGraph GetGraph(Transform graphParent = null)
        {
            if (graph != null && graph.gameObject.activeInHierarchy && graph.FlowId == flowId)
            {
                if (graphParent != null && graph.transform.parent != graphParent)
                {
                    graph.transform.SetParent(graphParent);
                }
                return graph;
            }
            else
            {
                return GetGraphFromPool(graphParent);
            }

        }

        private long            flowId = -1;

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