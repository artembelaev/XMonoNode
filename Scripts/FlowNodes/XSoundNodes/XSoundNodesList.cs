using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Базовый класс для нодов, которые принимают несколько источников звуков и возвращают один
    /// </summary>
    [NodeWidth(180)]
    public abstract class XSoundNodesList : XSoundNodeBase
    {
        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Inherited)]
        public AudioSources output;

        [Input(
            backingValue: ShowBackingValue.Never, 
            connectionType: ConnectionType.Override, 
            typeConstraint: TypeConstraint.Inherited, 
            dynamicPortList: true)]
        public List<AudioSources> inputs = new List<AudioSources>();

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(output))
            {
                if (output == null)
                {
                    output = new AudioSources();
                }

                output.List.Clear();

                return output;
            }
            else
                return null;
        }

        protected List<NodePort> GetAudioInputs()
        {
            List<NodePort> result = new List <NodePort>();
            for (int i = 0; i < inputs.Count; ++i)
            {
                var port = GetPort("inputs " + i);
                if (port == null)
                {
                    Debug.LogErrorFormat("port inputs {0} is null", i);
                    continue;
                }
                result.Add(port);
            }
            return result;
        }
    }
}
