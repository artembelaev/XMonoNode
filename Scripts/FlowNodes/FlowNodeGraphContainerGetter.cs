using System;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    [System.Serializable]
    public class FlowNodeGraphContainerGetter
    {
        public static string NO_CONTAINER = "-: None";

        [SerializeField]
        private string          containerFileName = "";
        [SerializeField]
        private bool showButtons = true;

        [SerializeField]
        private string          pathToContainers = "";
        

        [SerializeField]
        protected bool          drawPathToContainers = true;

        public bool IsEmpty
        {
            get => containerFileName.Length == 0 || string.IsNullOrEmpty(FullPath);
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

        public FlowNodeGraphContainerGetter()
        {
            this.showButtons = true;
        }

        public FlowNodeGraphContainerGetter(bool showButtons = true)
        {
            this.showButtons = showButtons;
        }

        public FlowNodeGraphContainerGetter(bool showButtons, string pathToContainers, string containerFileName = "")
        {
            this.showButtons = showButtons;
            this.pathToContainers = pathToContainers;
            this.containerFileName = containerFileName;
        }

        public FlowNodeGraphContainerGetter(string pathToContainers, string containerFileName = "")
        {
            this.pathToContainers = pathToContainers;
            this.containerFileName = containerFileName;
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
                    }
#endif
                    instanciatedContainer.CreatePoolRoot();
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
            if (instances.TryGetValue(loadedContainer, out FlowNodeGraphContainer cachedContainer) &&
                cachedContainer != null)
            {
                return cachedContainer;
            }
            else
            {
                FlowNodeGraphContainer newContainer = GameObject.Instantiate(loadedContainer);
                MonoBehaviour.DontDestroyOnLoad(newContainer.gameObject);
                instances[loadedContainer] = newContainer;
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
    

    }

}