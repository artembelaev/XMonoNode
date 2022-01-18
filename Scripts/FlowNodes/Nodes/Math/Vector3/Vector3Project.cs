using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Project a vector into another vector
    /// </summary>
    [CreateNodeMenu("Vector3/Project", 36)]
    [NodeWidth(180)]
    public class Vector3Project: MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector3      vector;

        [Input(connectionType: ConnectionType.Override)]
        public Vector3      normal;

        [Output]
        public Vector3      project;

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
            return Vector3.Project(portA.GetInputValue(vector), portB.GetInputValue(normal));
        }
    }
}
