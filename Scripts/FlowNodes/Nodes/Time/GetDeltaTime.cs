using System;
using System.Threading.Tasks;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Time/Delta Time", 539)]
    [NodeWidth(150)]
    public class GetDeltaTime : MonoNode
    {
        [Output]
        public float deltaTime;

        [Output, Hiding]
        public float fixedDeltaTime;

        [Output, Hiding]
        public float unscaledDeltaTime;

        private NodePort deltaTimePort;
        private NodePort fixedDeltaTimePort;
        private NodePort unscaledDeltaTimePort;

        private void Reset()
        {
            Name = "Delta Time";
        }

        protected override void Init()
        {
            base.Init();

            deltaTimePort = GetOutputPort(nameof(deltaTime));
            fixedDeltaTimePort = GetOutputPort(nameof(fixedDeltaTime));
            unscaledDeltaTimePort = GetOutputPort(nameof(unscaledDeltaTime));
        }

        public override object GetValue(NodePort port)
        {
            if (port == deltaTimePort)
            {
                return Time.deltaTime;
            }
            else if (port == fixedDeltaTimePort)
            {
                return Time.fixedDeltaTime;
            }
            else if (port == unscaledDeltaTimePort)
            {
                return Time.unscaledDeltaTime;
            }

            return null;
        }

    }
}
