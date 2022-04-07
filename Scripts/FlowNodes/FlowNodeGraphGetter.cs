using System;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    [System.Serializable]
    public class FlowNodeGraphGetter : FlowNodeGraphContainerGetter
    {
        [SerializeField]
        private string          graphId = "";
        public string GraphId => graphId;


        private FlowNodeGraph   graph = null;

        public new bool IsEmpty
        {
            get => graphId.Length == 0 || base.IsEmpty;
        }

        public FlowNodeGraphGetter() :
            base()
        {}

        public FlowNodeGraphGetter(bool showButtons = true) :
            base(showButtons)
        {}

        public FlowNodeGraphGetter(bool showButtons, string pathToContainers, string containerFileName = "", string graphId = "") :
            base(showButtons, pathToContainers, containerFileName)
        {
            this.graphId = graphId;
        }

        public FlowNodeGraphGetter(string pathToContainers, string containerFileName = "", string graphId = "") :
            base(pathToContainers, containerFileName)
        {
            this.graphId = graphId;
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

        public void Flow(Transform parent, System.Action<string> onEndAction, string state, params object[] parameters)
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

        public void Flow(Transform graphParent, params object[] parameters)
        {
            graph = GetGraph(graphParent);
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
            graph = GetGraph();
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
            if (!GraphIsFlowing())
            {
                return;
            }
            GetContainer().Stop(graph);
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

        public FlowNodeGraph GetGraph(Transform graphParent = null)
        {
            if (GraphIsFlowing())
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

        private bool GraphIsFlowing()
        {
            return graph != null && graph.gameObject.activeInHierarchy && graph.FlowId == flowId;
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