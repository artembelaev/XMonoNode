using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает
    /// </summary>
    [AddComponentMenu("X Sound Node/ListenerPos", 222)]
    [CreateNodeMenu("Sound/ListenerPos", 222)]
    [NodeWidth(150)]
    public class XSoundNodeListenerPos : MonoNode
    {
        [Output]
        public Vector3  listenerPos;

        private void Reset()
        {
            Name = "Listener Pos";
        }

        public override object GetValue(NodePort port)
        {
            return Camera.main.transform.position;
        }
    }
}
