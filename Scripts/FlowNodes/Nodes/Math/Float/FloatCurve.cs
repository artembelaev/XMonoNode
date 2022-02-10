using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ���������� ����������� ������ ��������� �� ������� � ��������� ��� ���������
    /// </summary>
    [AddComponentMenu("Math/Curve")]
    [CreateNodeMenu("Float/Curve", -169)]
    [NodeWidth(110)]
    public class FloatCurve : MonoNode
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public float            input = 0.0f;
        [Output, HideLabel]
        public float            output;

        [SerializeField, HideLabel]
        private AnimationCurve  curve = new AnimationCurve();

        private NodePort inputPort;

        private void Reset()
        {
            Name = "Curve";
        }

        protected override void Init()
        {
            base.Init();
            inputPort = GetInputPort(nameof(input));
        }

        public override object GetValue(NodePort port)
        {
            input = inputPort.GetInputValue(input);
            output = curve.Evaluate(input);
            return output;
        }
    }
}
