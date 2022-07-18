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

        [Input(connectionType: ConnectionType.Override), Hiding]
        public string param = "";

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
            NodePort portText = GetInputPort(nameof(Text));
            NodePort portParam = GetInputPort(nameof(param));
            int count = portText.ConnectionCount;
            string paramValue = portParam.ConnectionCount > 0 ? portParam.GetInputValue<object>().ToString() : param;
            if (count == 0)
            {
                LogFormat(type, "<color=brown>[{0}] {1}: </color>" + Text + " " + paramValue, name, Name); // <Объект>.<Нода>: <Text>
            }
            else if (count == 1)
            {
                LogFormat(type, "<color=brown>[{0}] {1}: </color>" + portText.GetInputValue<object>()+ " " + paramValue, name, Name);
            }
            else
            {
                LogFormat(type, "<color=brown>[{0}] {1} ({2} inputs):</color> " + paramValue, name, Name, count, param);
                for (int i = 0; i < count; ++i)
                {
                    object[] input = portText.GetInputValues();
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
