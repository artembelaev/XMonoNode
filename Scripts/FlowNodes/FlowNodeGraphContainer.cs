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
        private string                  id = "";

        [SerializeField]
        private FlowNodeGraph           graph = null;

        public string Id
        {
            get => id;
            set => id = value;
        }

        public Object                   Prefab => graph;

        private Queue<FlowNodeGraph>    pool = new Queue<FlowNodeGraph>();

        private Queue<FlowNodeGraph>    allInstanciated = new Queue<FlowNodeGraph>();

        public FlowNodeGraph Get(Transform parent = null)
        {
            if (graph == null)
            {
                Debug.LogErrorFormat("FlowNodeGraphContainer: graph not set to Id: \"{0}\"", id);
                return null;
            }

            FlowNodeGraph instanciated = null;

            if (pool.Count > 0)
            {
                instanciated = pool.Dequeue();
            }

            if (instanciated == null)
            {

                instanciated = GameObject.Instantiate(graph);

                MonoBehaviour.DontDestroyOnLoad(instanciated.gameObject);
                allInstanciated.Enqueue(instanciated);

#if UNITY_EDITOR
                if (Application.isEditor)
                {
                    instanciated.gameObject.hideFlags = HideFlags.DontSave;
                }
                instanciated.name = $"(Node Graph id=\"{id}\")";
#endif

            }


            instanciated.transform.SetParent(parent);
            instanciated.transform.localPosition = Vector3.zero;
            return instanciated;
        }

        public void PutIntoPool(FlowNodeGraph graph, Transform poolRoot)
        {
            pool.Enqueue(graph);
            graph.transform.SetParent(poolRoot);
        }

        public void SetGraphPrefab(FlowNodeGraph graph)
        {
            this.graph = graph;
        }

        public void StopAll()
        {
            foreach (FlowNodeGraph graph in allInstanciated)
            {
                if (graph != null && graph.gameObject.activeInHierarchy && graph.Container != null)
                {
                    graph.Stop();
                    PutIntoPool(graph, graph.Container.PoolRoot);
                }
            }
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

        private Transform                                       poolRoot = null;

        public bool IsStatic => isStatic;

        public List<FlowNodeGraphContainerItem>                 ItemsList => itemsList;

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

        public Transform PoolRoot => poolRoot;

        public FlowNodeGraph Get(string id, Transform parent = null)
        {
            if (Items.TryGetValue(id, out FlowNodeGraphContainerItem item))
            {
                FlowNodeGraph graph = item.Get(parent);
                if (graph != null)
                {
                    graph.Container = this;
                    graph.ContainerId = id;
                }
                return graph;
            }
            else
            {
                Debug.LogErrorFormat("FlowNode Graph Container {0} hasn't Id \"{1}\"", name, id);
                return null;
            }
        }

        public void PutIntoPool(FlowNodeGraph graph)
        {
            if (graph == null)
            {
                return;
            }

            if (Items.TryGetValue(graph.ContainerId, out FlowNodeGraphContainerItem item))
            {
                item.PutIntoPool(graph, poolRoot);
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

        public void Stop(FlowNodeGraph graph)
        {
            if (graph != null)
            {
                graph.Stop();
                PutIntoPool(graph);
            }
        }

        public void StopAll()
        {
            itemsList.ForEach(item => item.StopAll());
        }

        private void Start()
        {
            CreatePoolRoot();
        }

        public void CreatePoolRoot()
        {
            if (poolRoot != null)
            {
                return;
            }

            poolRoot = new GameObject("pool").transform;
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                poolRoot.gameObject.hideFlags = HideFlags.DontSave;
            }
#endif
            poolRoot.SetParent(transform);
            poolRoot.gameObject.SetActive(false);
        }
    }
}
