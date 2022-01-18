using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Project a vector into another vector
    /// </summary>
    [CreateNodeMenu("Vector4/Project", 36)]
    [NodeWidth(150)]
    public class Vector4Project: MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4      vector;

        [Input(connectionType: ConnectionType.Override)]
        public Vector4      normal;

        [Output]
        public Vector4      project;

        private NodePort portA;
        private NodePort portB;

        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(vector));
            portB = GetInputPort(nameof(normal));
        }

        public override object GetValue(NodePort port)
        {
            return Vector4.Project(portA.GetInputValue(vector), portB.GetInputValue(normal));
        }
    }
}
