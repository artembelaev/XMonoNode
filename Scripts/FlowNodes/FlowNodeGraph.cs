using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// A convenient graph for use with flow nodes
    /// </summary>
    [AddComponentMenu("Flow Nodes/FlowNodeGraph", 1)]
    //[ExecuteInEditMode]
    [RequireComponent(/*typeof(OnFlowEventNode), */typeof(FlowEnd))]
    [RequireNode(typeof(OnFlowEventNode), typeof(FlowEnd))]
    public class FlowNodeGraph : MonoNodeGraph
    {
        public void DestroySelf()
        {
            Stop();
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
#endif
                Destroy(gameObject);
#if UNITY_EDITOR
            }
#endif
        }

        /// <summary>
        /// state parameter of Flow() methods
        /// </summary>
        public string State
        {
            get;
            set;
        }

        /// <summary>
        /// Параметры, передаваемые в метод FlowNodeGraph.Flow()
        /// </summary>
        public object[] FlowParametersArray
        {
            get
            {
                return outputFlowParametersArray;
            }
            set
            {
                if (outputFlowParametersArray != value)
                {
                    outputFlowParametersArray = value;
                    OutputFlowParametersDict.Clear();
                }
            }

        }

        public long UpdateParametersTimes
        {
            get;
            private set;
        } = 0;

        private object[]     outputFlowParametersArray = new object[0];

        /// <summary>
        /// Параметры, передаваемые в метод FlowNodeGraph.Flow()
        /// </summary>
        public Dictionary<string, object> OutputFlowParametersDict
        {
            get => outputFlowParametersDict;
            private set
            {
                outputFlowParametersDict = value;
                outputFlowParametersArray = new object[value.Count];
                int i = 0;
                foreach (var pair in value)
                {
                    outputFlowParametersArray[i] = pair.Value;
                    ++i;
                }
            }
        }

        private Dictionary<string, object> outputFlowParametersDict = new Dictionary<string, object>();

        private void Reset()
        {
#if UNITY_EDITOR
            // OnFlowStart добавлен автоматически
            OnFlowEventNode start = gameObject.AddComponent<OnFlowEventNode>();
            if (start != null)
            {
                start.graph = this;
                if (start.Name == null || start.Name.Trim() == "")
                {
                    start.Name = "OnFlowStart";
                }
                start.Position = new Vector2(-300.0f, -100.0f);
            }

            // OnFlowStart добавлен автоматически
            FlowEnd end = GetComponent<FlowEnd>();
            if (end != null)
            {
                end.graph = this;
                if (end.Name == null || end.Name.Trim() == "")
                {
                    end.Name = "FlowEnd";
                }
                end.Position = new Vector2(450.0f, -100.0f);

            }
#endif
        }

        [SerializeField, HideInInspector] private OnUpdateParametersNode[]  updateParametersNodes = null;
        [SerializeField, HideInInspector] private FlowEnd[]                 endNodes = null;
        private IFlowNode[]               eventNodes = null;

        private IFlowNode[] EventNodes
        {
            get
            {
                if (eventNodes == null)
                {
                    eventNodes = GetFlowEventNodes();
                }
                return eventNodes;
            }
        }

        protected override void Init()
        {
            base.Init();

            updateParametersNodes = GetComponents<OnUpdateParametersNode>();
            endNodes = GetComponents<FlowEnd>();
        }

        private void OnUpdateInputParametersNodes()
        {
            //OnUpdateParametersNode[] nodes = GetComponents<OnUpdateParametersNode>();
            ++UpdateParametersTimes;

            foreach (var node in updateParametersNodes)
            {
                node.TriggerFlow();
            }
        }

        public virtual void UpdateInputParameters(params object[] parameters)
        {
            FlowParametersArray = parameters;
            OnUpdateInputParametersNodes();
        }

        private static Dictionary<TKey, TValue> Merge<TKey, TValue>(params Dictionary<TKey, TValue>[] dictionaries)
        {
            var result = new Dictionary<TKey, TValue>();
            foreach (var dict in dictionaries)
                foreach (var x in dict)
                    result[x.Key] = x.Value;
            return result;
        }

        public virtual void UpdateInputParameters(Dictionary<string, object> parameters)
        {
            OutputFlowParametersDict = parameters;
            OnUpdateInputParametersNodes();
        }

        public virtual void UpdateInputParameter(string name, object value)
        {
            outputFlowParametersDict[name] = value;
            OnUpdateInputParametersNodes();
        }

        public void GetOutputParameters(out Dictionary<string, object> parameters)
        {
            parameters = new Dictionary<string, object>();
            OutputFlowParameter[] paramNodes = GetComponents<OutputFlowParameter>();
            foreach (var node in paramNodes)
            {
                parameters[node.Name] = node.ValueAsObject;
            }
        }

        /// <summary>
        /// Обновляет значения параметров в режиме редактора. Значения берет из нодов параметров (синего цвета)
        /// </summary>
        public virtual void UpdateTestParameters()
        {
            InputFlowParameter[] paramNodes = GetComponents<InputFlowParameter>();
            FlowParametersArray = new object[paramNodes.Length];

            Dictionary<string, object> dict = new Dictionary<string, object>();
            

            for (int i = 0; i < paramNodes.Length; ++i)
            {
                //FlowParametersArray[i] = paramNodes[i].GetTestValue();
                dict[paramNodes[i].Name] = paramNodes[i].GetDefaultValue();
            }
            OutputFlowParametersDict = dict;
            OnUpdateInputParametersNodes();
        }

        public const string ALL_EXECUTE_NODES = ":- all execute nodes";

        private void InitEndNodes(Action<string> onEndAction, string state)
        {
            State = state;

            foreach (var node in endNodes)
            {
                node.Action = onEndAction;
            }
        }

        /// <summary>
        /// Starts flow of the graph
        /// </summary>
        /// <param name="parameters">Custom graph parameters<seealso cref="InputFlowParameter"/></param>
        public virtual void Flow(params object[] parameters)
        {
            UpdateInputParameters(parameters);
            Flow();
        }

        /// <summary>
        /// Starts flow of the graph
        /// </summary>
        /// <param name="parameters">Custom graph parameters<seealso cref="InputFlowParameter"/></param>
        /// <param name="onEndAction">Action that invokes when flow reachs the FlowEnd node</param>
        /// <param name="state">value of action's parameter</param>
        public virtual void Flow(Action<string> onEndAction, string state, params object[] parameters)
        {
            UpdateInputParameters(parameters);
            Flow(onEndAction, state);
        }

        /// <summary>
        /// Starts flow of the graph
        /// </summary>
        /// <param name="parameters">Custom graph parameters<seealso cref="InputFlowParameter"/></param>
        public virtual void Flow(Dictionary<string, object> parameters)
        {
            UpdateInputParameters(parameters);
            Flow();
        }

        /// <summary>
        /// Starts flow of the graph
        /// </summary>
        /// <param name="onEndAction">Action that invokes when flow reachs the FlowEnd node</param>
        /// <param name="state">value of action's parameter</param>
        /// <param name="parameters">Custom graph parameters<seealso cref="InputFlowParameter"/></param>
        public virtual void Flow(Action<string> onEndAction, string state, Dictionary<string, object> parameters)
        {
            UpdateInputParameters(parameters);
            Flow(onEndAction, state);
        }

        protected virtual IFlowNode[] GetFlowEventNodes()
        {
            return GetComponents<OnFlowEventNode>();
        }

        [ContextMenu("Flow")]
        public virtual void Flow()
        {
            Flow(null, "");
        }

        public void FlowAnimationFix()
        {
            Flow(null, "");
        }

        public virtual void Flow(Action<string> onEndAction, string state = "")
        {
            IFlowNode[] flowNodes = EventNodes;
            if (flowNodes.Length == 0)
            {
                Debug.LogError(gameObject.name + ": FlowNodeGraph hasn't OnExecute nodes");
            }

            InitEndNodes(onEndAction, state);

            foreach (var node in flowNodes)
            {
                node.TriggerFlow();
            }
        }

        /// <summary>
        /// Custom start of flow. 
        /// </summary>
        /// <param name="eventName">Name of the CustomEvent node witch starts the flow</param>
        public virtual void CustomEvent(string eventName)
        {
            OnCustomEventNode[] eventNodes = GetComponents<OnCustomEventNode>();
            if (eventNodes.Length == 0)
            {
                Debug.LogError(gameObject.name + ": FlowNodeGraph hasn't CustomEvent nodes");
            }

            int triggeredCount = 0;
            foreach (var node in eventNodes)
            {
                if (node.Name == eventName)
                {
                    ++triggeredCount;
                    node.TriggerFlow();
                }
            }

            if (triggeredCount == 0)
            {
                Debug.LogError(gameObject.name + ": FlowNodeGraph hasn't CustomEvent node with name \"" + eventName + "\"");
            }
        }

        [ContextMenu("Stop")]
        public virtual void Stop()
        {
            IFlowNode[] nodes = GetComponents<IFlowNode>();

            foreach (var node in nodes)
            {
                node.Stop();
            }
        }

        public Coroutine StartStaticCoroutine(IEnumerator coroutine)
        {
            if (this == null)
            {
                return null;
            }

            return StartCoroutine(coroutine);
        }

    }


    public static class ExtensionMethods
    {
        public static T RandomElement<T>(this List<T> parameters, int startIndex = 0)
        {
            int count=parameters.Count;
            if (parameters == null || count == 0)
            {
                return default;
            }

            return parameters[UnityEngine.Random.Range(Mathf.Min(count - 1, startIndex), count)];
        }

        public static T RandomElement<T>(this T[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return default(T);
            }
            return (T)parameters[UnityEngine.Random.Range(0, parameters.Length)];
        }

        public static string ToHex(this Color color)
        {
            string rtn = "#" + ((int)(color.r * 255)).ToString("X2") + ((int)(color.g * 255)).ToString("X2") + ((int)(color.b * 255)).ToString("X2");
            return rtn;
        }

        public static string Color(this string need, Color color)
        {
            return "<color=" + color.ToHex() + ">" + need + "</color>";
        }

        public static string Color(this string need, string color)
        {
            return "<color=" + color + ">" + need + "</color>";
        }

        public static T Get<T>(this object[] parameters, T def = default(T), int index = 0)
        {
            T result = def;
            int currentIndex = -1;
            Type targetType = typeof(T);
#if NETFX_CORE
            TypeInfo targetTypeInfo = targetType.GetTypeInfo();
#else
            Type targetTypeInfo = targetType;
#endif
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null)
                {
                    continue;
                }
#if NETFX_CORE
                TypeInfo type = parameters[i].GetType().GetTypeInfo();
#else
                Type type = parameters[i].GetType();
#endif
                if (targetTypeInfo.IsAssignableFrom(type) ||
                    type.IsAssignableFrom(targetTypeInfo) ||
                    type.IsSubclassOf(targetType))
                {
                    currentIndex++;
                    if (currentIndex == index)
                    {
                        result = (T)parameters[i];
                        break;
                    }
                }
            }
            return result;
        }

    }
}