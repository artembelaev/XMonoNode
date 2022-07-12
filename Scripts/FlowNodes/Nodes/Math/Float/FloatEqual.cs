﻿using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Equal", -177)]
    [NodeWidth(160)]
    public class FloatEqual : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)] public float a;
        [Input(connectionType: ConnectionType.Override)] public float b;
        [Output] public bool result;

        private NodePort portA;
        private NodePort portB;
        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(a));
            portB = GetInputPort(nameof(b));

            GetOutputPort(nameof(result)).label = "A == B";
        }

        public override object GetValue(NodePort port)
        {
            return portA.GetInputValue(a) == portB.GetInputValue(b);
        }
    }
}