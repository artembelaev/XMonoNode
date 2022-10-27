using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Logic/Xor", 103)]
    [NodeWidth(90)]
    public class Xor : MonoNode
    {
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never, typeConstraint: TypeConstraint.Inherited), HideLabel, Inline]
        public bool a = false;
        [Output]
        public bool XOR;

        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never, typeConstraint: TypeConstraint.Inherited), HideLabel]
        public bool b = false;

       

        protected NodePort InputPortA = null;
        protected NodePort InputPortB = null;

        protected override void Init()
        {
            base.Init();

            InputPortA = GetInputPort(nameof(a));
            InputPortB = GetInputPort(nameof(b));
        }

        public override object GetValue(NodePort port)
        {
            return InputPortA.GetInputValue(a) ^ InputPortB.GetInputValue(b);
        }

    }
}