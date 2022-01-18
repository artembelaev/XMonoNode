using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    //[ExecuteInEditMode]
    [AddComponentMenu("X Sound Node/SoundNodeGraph", 1)]
    [RequireNode(/*typeof(OnFlowEventNode), */typeof(XSoundNodePlay)/*, typeof(FlowEnd)*/)]
    [RequireComponent(/*typeof(OnFlowEventNode), */typeof(XSoundNodePlay)/*, typeof(FlowEnd)*/)]
    public class XSoundNodeGraph : FlowNodeGraph
    {
        private void Reset()
        {
#if UNITY_EDITOR
            // OnFlowStart
            if (!TryGetComponent(out OnFlowEventNode start))
            {
                start = gameObject.AddComponent<OnFlowEventNode>();
            }
            if (start != null)
            {
                start.graph = this;
                if (start.Name == null || start.Name.Trim() == "")
                {
                    start.Name = "OnFlowStart";
                }
                start.Position = new Vector2(-312.0f, -56.0f);
            }

            // Play добавлен автоматически
            if (!TryGetComponent(out XSoundNodePlay play))
            {
                play = gameObject.AddComponent<XSoundNodePlay>();
            }
            if (play != null)
            {
                play.graph = this;
                if (play.Name == null || play.Name.Trim() == "")
                {
                    play.Name = "Play";
                }
                play.Position = new Vector2(88.0f, -56.0f);
            }

            // Добавить Source и соединить с Play, а Play с Flow
            if (!TryGetComponent(out XSoundNodeSource source))
            {
                source = gameObject.AddComponent<XSoundNodeSource>();
            }
            if (source != null)
            {
                source.Name = "Source";
                source.Position = new Vector2(-376.0f, 40.0f);

                OnBeforeSerialize();
                if (play != null)
                {
                    NodePort sourceOutput = source.GetOutputPort(nameof(source.audioOutput));
                    NodePort playInput = play.GetInputPort(nameof(play.audioInput));
                    if (sourceOutput != null && playInput != null)
                    {
                        sourceOutput.Connect(playInput);
                    }
                    if (start != null)
                    {
                        NodePort startFlowOutput = start.GetOutputPort(nameof(start.FlowOutput));
                        NodePort playFlowInput = play.GetInputPort(nameof(play.FlowInput));
                        if (startFlowOutput != null && playFlowInput != null)
                        {
                            startFlowOutput.Connect(playFlowInput);
                        }
                    }
                    else
                    {
                        play.showState = INode.ShowAttribState.Minimize;
                    }
                }
            }

            // OnFlowStart добавлен автоматически
            FlowEnd end = GetComponent<FlowEnd>();
            if (end != null)
            {
                end.graph = this;
                if (end.Name == null || end.Name.Trim() == "")
                {
                    end.Name = "FlowEnd";
                }
                end.Position = new Vector2(440.0f, -56.0f);

                if (play != null)
                {
                    NodePort playEnd = play.GetOutputPort(nameof(play.onEnd));
                    NodePort endFlowInput = end.GetInputPort(nameof(play.FlowInput));
                    if (endFlowInput != null && playEnd != null)
                    {
                        playEnd.Connect(endFlowInput);
                    }
                }
            }
#endif
        }

        protected override IFlowNode[] GetFlowEventNodes()
        {
            IFlowNode[] result =  base.GetFlowEventNodes();
  
            if (result.Length != 0)
            {
                return result;
            }
            else
            {
                return GetComponents<XSoundNodePlay>();
            }
        }
    }
}
