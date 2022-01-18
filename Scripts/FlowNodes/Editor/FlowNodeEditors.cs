using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XMonoNode;
using XMonoNodeEditor;

namespace FlowNodesEditor
{
    [CustomNodeEditor(typeof(InputFlowParameterFloat))]
    public class XFlowNodeFloatParameterEditor : NodeEditor
    {
        private InputFlowParameterFloat node = null;
        public override int OnBodyGUI()
        {
            int propertyCount = base.OnBodyGUI();

            if (node == null)
            {
                node = target as InputFlowParameterFloat;
                if (node == null)
                {
                    return propertyCount;
                }
            }
            serializedObject.Update();

            if (Target.ShowState == INode.ShowAttribState.ShowAll)
            {
                GUILayout.BeginHorizontal();

                float newValue = GUILayout.HorizontalSlider(node.DefaultValue, 0.0f, 1.0f, GUILayout.MinHeight(10));
                UpdateValue(newValue);
                GUILayout.EndHorizontal();
            }
            else
            {
                NodeEditorGUILayout.hasHiddenProperty = true;
            }

            ++propertyCount;
            return propertyCount;
        }

        private void UpdateValue(float newValue)
        {
            if (!Mathf.Approximately(newValue, node.DefaultValue))
            {
                Undo.RecordObject(node, node.Name);
                node.DefaultValue = newValue;
                FlowNodeGraph flowGraph = node.graph as FlowNodeGraph;
                if (flowGraph != null)
                {
                    flowGraph.UpdateTestParameters();
                }
                EditorUtility.SetDirty(node.gameObject);
            }
        }
    }

    [CustomNodeEditor(typeof(ButtonNode))]
    public class ButtonNodeEditor : NodeEditor
    {
        public ButtonNode Node => target as ButtonNode;

        public override int OnBodyGUI()
        {
            Node.FlowOutputPort.label = Node.ButtonText;
            return base.OnBodyGUI();
        }
    }

    [CustomNodeEditor(typeof(FloatEase))]
    public class FloatEaseEditor : NodeEditor
    {
        public override int OnBodyGUI()
        {
            int propertyCount = base.OnBodyGUI();
            ++propertyCount;
            if (Target.ShowState == INode.ShowAttribState.ShowAll)
            {
                FloatEase node = target as FloatEase;
                Texture2D tex = node.Clamped01 ? FlowNodeEditorResources.EaseTextureClamped01(node.EasingMode) : FlowNodeEditorResources.EaseTexture(node.EasingMode);
                GUILayout.BeginHorizontal();
                GUILayout.Label("", GUILayout.ExpandWidth(true), GUILayout.MinWidth(50));
                GUILayout.Label(new GUIContent(tex), GUILayout.MinWidth(tex.width + 2), GUILayout.Height(tex.height + 2));
                GUILayout.EndHorizontal();
            }
            else
            {
                NodeEditorGUILayout.hasHiddenProperty = true;
            }

            return propertyCount;
        }
    }
   
    public class AnimateEaseEditor : NodeEditor
    {
        public override int OnBodyGUI()
        {
            int propertyCount = base.OnBodyGUI();
            ++propertyCount;
            if (Target.ShowState == INode.ShowAttribState.ShowAll)
            {
                AnimateValue node = target as AnimateValue;

                node.EasingMode = (EasingMode)EditorGUILayout.EnumPopup(new GUIContent(ObjectNames.NicifyVariableName(nameof(AnimateValue.EasingMode))), node.EasingMode);

                Texture2D tex = FlowNodeEditorResources.EaseTextureClamped01(node.EasingMode);

                GUILayout.BeginHorizontal();
                GUILayout.Label("", GUILayout.ExpandWidth(true), GUILayout.MinWidth(50));
                GUILayout.Label(new GUIContent(tex), GUILayout.MinWidth(tex.width + 2), GUILayout.Height(tex.height + 2));
                GUILayout.EndHorizontal();
            }
            else
            {
                NodeEditorGUILayout.hasHiddenProperty = true;
            }
            return propertyCount;
        }
    }

    [CustomNodeEditor(typeof(AnimateFloatEase))]
    public class AnimateFloatEaseEditor : AnimateEaseEditor
    {
    }

    [CustomNodeEditor(typeof(AnimateVector3Ease))]
    public class AnimateVector3EaseEditor : AnimateEaseEditor
    {
    }

    [CustomNodeEditor(typeof(AnimateColorEase))]
    public class AnimateColorEaseEditor : AnimateEaseEditor
    {
    }

    public class TweenNodeEditor : NodeEditor
    {
        public override int OnBodyGUI()
        {
            int propertyCount = base.OnBodyGUI();
            ++propertyCount;
            if (Target.ShowState == INode.ShowAttribState.ShowAll)
            {
                TweenNode node = target as TweenNode;

                Texture2D tex = FlowNodeEditorResources.EaseTextureClamped01(node.easingMode);

                GUILayout.BeginHorizontal();
                GUILayout.Label("", GUILayout.ExpandWidth(true), GUILayout.MinWidth(50));
                GUILayout.Label(new GUIContent(tex), GUILayout.MinWidth(tex.width + 2), GUILayout.Height(tex.height + 2));
                GUILayout.EndHorizontal();
            }
            else
            {
                NodeEditorGUILayout.hasHiddenProperty = true;
            }
            return propertyCount;
        }
    }

    [CustomNodeEditor(typeof(TweenAnchoredPosition))]
    public class TweenAnchoredPositionEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenBottomLeftColorTextMeshProUGUI))]
    public class TweenBottomLeftColorTextMeshProUGUIEditor : TweenNodeEditor
    { }

    [CustomNodeEditor(typeof(TweenBottomRightColorTextMeshProUGUI))]
    public class TweenBottomRightColorTextMeshProUGUIEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenColorMaterial))]
    public class TweenColorMaterialEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenColorTextMeshProUGUI))]
    public class TweenColorTextMeshProUGUIEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenFloatMaterial))]
    public class TweenFloatMaterialEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenGradientTextMeshProUGUI))]
    public class TweenGradientTextMeshProUGUIEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenGraphicColor))]
    public class TweenGraphicColorEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenCanvasGroupAlpha))]
    public class TweenCanvasGroupAlphaEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalPosition))]
    public class TweenLocalPositionEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalPosition2D))]
    public class TweenLocalPosition2DEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalRotation))]
    public class TweenLocalRotationEditor : TweenNodeEditor
    {

    }
    [CustomNodeEditor(typeof(TweenLocalRotation2D))]
    public class TweenLocalRotation2DEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalScale))]
    public class TweenLocalScaleEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalScale2D))]
    public class TweenLocalScale2DEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenPosition))]
    public class TweenPositionEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenPosition2D))]
    public class TweenPosition2DEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenRotation))]
    public class TweenRotationEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenRotation2D))]
    public class TweenRotation2DEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenTopLeftColorTextMeshProUGUI))]
    public class TweenTopLeftColorTextMeshProUGUIEditor : TweenNodeEditor
    {
    }
    [CustomNodeEditor(typeof(TweenTopRightColorTextMeshProUGUI))]
    public class TweenTopRightColorTextMeshProUGUIEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenVectorMaterial))]
    public class TweenVectorMaterialEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(MixerParameterTime))]
    public class FloatAudioMixerEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalPosX))]
    public class TweenLocalPosXEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalPosY))]
    public class TweenLocalPosYEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalPosZ))]
    public class TweenLocalPosZEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenPosX))]
    public class TweenPosXEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenPosY))]
    public class TweenPosYEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenPosZ))]
    public class TweenPosZEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalScaleX))]
    public class TweenLocalScaleXEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalScaleY))]
    public class TweenLocalScaleYEditor : TweenNodeEditor
    {
    }

    [CustomNodeEditor(typeof(TweenLocalScaleZ))]
    public class TweenLocalScaleZEditor : TweenNodeEditor
    {
    }

}
