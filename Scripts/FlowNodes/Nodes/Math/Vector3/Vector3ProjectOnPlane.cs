using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Project a vector onto plane defined a normal
    /// </summary>
    [CreateNodeMenu("Vector3/ProjectOnPlane", 37)]
    [NodeWidth(180)]
    public class Vector3ProjectOnPlane: MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector3      vector;

        [Input(connectionType: ConnectionType.Override)]
        public Vector3      planeNormal;

        [Output]
        public Vector3        projectOnPlane;

        private NodePort portA;
        private NodePort portB;

        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(vector));
            portB = GetInputPort(nameof(planeNormal));
        }

        public override object GetValue(NodePort port)
        {
            return Vector3.ProjectOnPlane(portA.GetInputValue(vector), portB.GetInputValue(planeNormal));
        }
    }
}
