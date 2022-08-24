using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("GameObject/Mouse", 504)]
    [NodeWidth(150)]
    public class InputMouse : MonoNode 
    {
        [Input(connectionType: ConnectionType.Override), Range(0, 2)]
        public int                              button = 0;

        [Output]
        public bool                             mouseButton;

        [Output]
        public bool                             mouseButtonDown;

        [Output]
        public bool                             mouseButtonUp;

        [Output]
        public Vector3                          mousePosition;

        private NodePort                        portButton = null;
        private NodePort                        portMouseButton = null;
        private NodePort                        portMouseButtonDown = null;
        private NodePort                        portMouseButtonUp = null;
        private NodePort                        portMousePosition = null;

        protected override void Init()
        {
            base.Init();

            portButton = GetInputPort(nameof(button));
            portMouseButton = GetOutputPort(nameof(mouseButton));
            portMouseButtonDown = GetOutputPort(nameof(mouseButtonDown));
            portMouseButtonUp = GetOutputPort(nameof(mouseButtonUp));
            portMousePosition = GetOutputPort(nameof(mousePosition));
        }

        private int GetButton()
        {
            return portButton.GetInputValue(button);
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            if (port == portMouseButton)
            {
                return Input.GetMouseButton(GetButton());
            }
            else if (port == portMouseButtonDown)
            {
                return Input.GetMouseButtonDown(GetButton());
            }
            else if (port == portMouseButtonUp)
            {
                
                return Input.GetMouseButtonUp(GetButton());
            }
            else //if (port == portMousePosition)
            {
                return Input.mousePosition;
            }
        }
    }
}
