using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    /// <summary>
    /// Instanciates a concrete graph object
    /// </summary>
    [System.Serializable]
    public class FlowNodeGraphContainerItem
    {
        public FlowNodeGraphContainerItem()
        {}

        public FlowNodeGraphContainerItem(FlowNodeGraph graphPrefab, string id)
        {
            graph = graphPrefab;
            this.id = id;
        }

        [SerializeField]
        [Header("graph id")]
        private string          id = "";

        [SerializeField]
        [Header("Graph prefab")]
        private FlowNodeGraph   graph = null;

        public string Id
        {
            get => id;
            set => id = value;
        }


        public Object           Prefab => graph;

        private FlowNodeGraph   instanciated = null;

        public FlowNodeGraph Get(Transform parent = null)
        {
            if (graph == null)
            {
                Debug.LogErrorFormat("FlowNodeGraphContainer: graph not set to Id: \"{0}\"", id);
                return null;
            }

            if (instanciated == null)
            {
                if (parent == null)
                {
                    instanciated = GameObject.Instantiate(graph);
                }
                else
                {
                    instanciated = GameObject.Instantiate(graph, parent);
                }
#if UNITY_EDITOR
                if (Application.isEditor)
                {
                    instanciated.gameObject.hideFlags = HideFlags.DontSave;
                }

                instanciated.name = $"(Node Graph id=\"{id}\")";
#endif
            }

            if (parent != null)
            {
                instanciated.transform.parent = parent;
            }
            instanciated.transform.localPosition = Vector3.zero;
            return instanciated;
        }

        public void SetGraphPrefab(FlowNodeGraph graph)
        {
            this.graph = graph;
        }
    }

    /// <summary>
    ///Allows you to manage and instanciate flow node graphs
    /// </summary>
    [AddComponentMenu("X Mono Node/FlowNodeGraphContainer", 701)]
    public class FlowNodeGraphContainer : MonoBehaviour
    {
        [SerializeField, Tooltip("Use for optimisation static sound. Co")]
        private bool                                            isStatic = false;
        [SerializeField]
        private List<FlowNodeGraphContainerItem>                itemsList = null;

        public bool IsStatic => isStatic;

        public List<FlowNodeGraphContainerItem>                 ItemsList => itemsList;

        public Transform GraphParent
        {
            get;
            set;
        } = null;


        private Dictionary<string, FlowNodeGraphContainerItem>  items = null;
        private Dictionary<string, FlowNodeGraphContainerItem>  Items
        {
            get
            {
                if (items == null)
                {
                    items = new Dictionary<string, FlowNodeGraphContainerItem>();
                    foreach (var setting in itemsList)
                    {
                        items[setting.Id] = setting;
                    }
                }
                return items;
            }
        }

        private List<FlowNodeGraph>         instanciated = new List<FlowNodeGraph>();

        public FlowNodeGraph Flow(string id, params object[] parameters)
        {
            FlowNodeGraph graph = Get(id, GraphParent);
            if (graph != null)
            {
                graph.Flow(parameters);
            }
            return graph;
        }

        public FlowNodeGraph Flow(string id, System.Action<string> onEndAction, string state, params object[] parameters)
        {
            FlowNodeGraph graph = Get(id, GraphParent);
            if (graph != null)
            {
                graph.Flow(onEndAction, state, parameters);
            }
            return graph;
        }

        public FlowNodeGraph Flow(string id, Dictionary<string, object> parameters)
        {
            FlowNodeGraph graph = Get(id, GraphParent);
            if (graph != null)
            {
                graph.Flow(parameters);
            }
            return graph;
        }

        public FlowNodeGraph Flow(string id, System.Action<string> onEndAction, string state, Dictionary<string, object> parameters)
        {
            FlowNodeGraph graph = Get(id, GraphParent);
            if (graph != null)
            {
                graph.Flow(onEndAction, state, parameters);
            }
            return graph;
        }

        public virtual FlowNodeGraph CustomEvent(string id, string eventName, Transform parent = null)
        {
            FlowNodeGraph graph = Get(id, parent);
            if (graph != null)
            {
                graph.CustomEvent(eventName);
            }
            return graph;
        }

        public FlowNodeGraph Get(string id, Transform parent = null)
        {
            if (Items.TryGetValue(id, out FlowNodeGraphContainerItem item))
            {
                return item.Get(parent);
            }
            else
            {
                Debug.LogErrorFormat("FlowNode Graph Container {0} hasn't Id \"{1}\"", name, id);
                return null;
            }
        }

        public Object GetPrefab(string id)
        {
            if (Items.TryGetValue(id, out FlowNodeGraphContainerItem item))
            {
                return item.Prefab;
            }
            else
            {
                return null;
            }
        }


        public void UpdateInputParameters(string id, Transform transform, params object[] parameters)
        {
            FlowNodeGraph graph = Get(id, transform);
            if (graph != null)
            {
                graph.UpdateInputParameters(parameters);
            }
        }

        public void UpdateInputParameters(string id, Dictionary<string, object> parameters)
        {
            FlowNodeGraph graph = Get(id);
            if (graph != null)
            {
                graph.UpdateInputParameters(parameters);
            }
        }

        public void GetOutputParameters(string id, out Dictionary<string, object> parameters)
        {
            FlowNodeGraph graph = Get(id);
            if (graph != null)
            {
                graph.GetOutputParameters(out parameters);
            }
            else
            {
                parameters = new Dictionary<string, object>();
            }
        }

        public void Stop(string id)
        {
            FlowNodeGraph graph = Get(id);
            if (graph != null)
            {
                graph.Stop();
            }
        }

        public void StopAll()
        {
            itemsList.ForEach(item => item.Get()?.Stop());
        }

    }
}
