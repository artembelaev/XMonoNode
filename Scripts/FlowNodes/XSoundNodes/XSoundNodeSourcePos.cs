using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ¬озвращает положение источника звука
    /// </summary>
    [AddComponentMenu("X Sound Node/SourcePos", 221)]
    [CreateNodeMenu("Sound/SourcePos", 221)]
    [NodeWidth(150)]
    public class XSoundNodeSourcePos: XSoundNodeSimple
    {
        [Output]
        public Vector3  sourcePos;

        private void Reset()
        {
            Name = "Source Pos";
        }

        public override object GetValue(NodePort port)
        {
            AudioSources sources = GetAudioInput();
            return sources.List.Count != 0 ? sources.List[0].gameObject.transform.position : Vector3.zero;

        }
    }
}
