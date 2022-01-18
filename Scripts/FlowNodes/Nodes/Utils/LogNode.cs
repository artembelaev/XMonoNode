using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Utils/Log", 521)]
    [NodeWidth(150)]
    public class LogNode : FlowNodeInOut 
    {
        public enum LogType
        {
            Info,
            Warning,
            Error,
        }

        [Input, HideLabel]
        public string Text;

        [SerializeField, Hiding, HideLabel]
        private LogType type = LogType.Info;

        private void LogFormat(LogType type, string format, params object[] args)
        {
            switch (type)
            {
                case LogType.Info:
                    Debug.LogFormat(format, args);
                    break;
                case LogType.Warning:
                    Debug.LogWarningFormat(format, args);
                    break;
                case LogType.Error:
                    Debug.LogErrorFormat(format, args);
                    break;
            }
        }

        public override void Flow(NodePort flowPort) 
        {
            NodePort port = GetInputPort(nameof(Text));
            int count = port.ConnectionCount;

            if (count == 0)
            {
                LogFormat(type, "<color=brown>[{0}] {1}: </color>" + Text, name, Name); // <Объект>.<Нода>: <Text>
            }
            else if (count == 1)
            {
                LogFormat(type, "<color=brown>[{0}] {1}: </color>" + port.GetInputValue<object>(), name, Name);
            }
            else
            {
                LogFormat(type, "<color=brown>[{0}] {1} ({2} inputs):</color>", name, Name, count);
                for (int i = 0; i < count; ++i)
                {
                    object[] input = port.GetInputValues();
                    LogFormat(type, "<color=brown>{0}) </color>" + input[i], i);
                }
            }
            FlowOut();
        }

        public override object GetValue(NodePort port) 
        {
            return null;
        }
    }
}
