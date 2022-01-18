using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using XMonoNode;

namespace XMonoNode
{
    public enum EasingMode
    {
        Unset = 0,
        Linear = 1,
        InSine = 2,
        OutSine = 3,
        InOutSine = 4,
        InQuad = 5,
        OutQuad = 6,
        InOutQuad = 7,
        InCubic = 8,
        OutCubic = 9,
        InOutCubic = 10,
        InQuart = 11,
        OutQuart = 12,
        InOutQuart = 13,
        InQuint = 14,
        OutQuint = 15,
        InOutQuint = 16,
        InExpo = 17,
        OutExpo = 18,
        InOutExpo = 19,
        InCirc = 20,
        OutCirc = 21,
        InOutCirc = 22,
        InElastic = 23,
        OutElastic = 24,
        InOutElastic = 25,
        InBack = 26,
        OutBack = 27,
        InOutBack = 28,
        InBounce = 29,
        OutBounce = 30,
        InOutBounce = 31,
    }

    /// <summary>
    /// Определяет зависимость одного параметра от другого по одной из зависимостей
    /// </summary>
    [AddComponentMenu("Math/Ease")]
    [CreateNodeMenu("Float/Ease", -168)]
    [NodeWidth(220)]
    public class FloatEase : MonoNode
    {
        [Inline]
        [Input]
        public float            input = 0.0f;
        [Output]
        public float            output;

        [Input, Hiding]
        public bool clamped01 = true;

        [SerializeField, NodeEnum]
        private EasingMode      easingMode = EasingMode.Linear;

        public EasingMode EasingMode
        {
            get => easingMode;
            set => easingMode = value;
        }

        public bool Clamped01 => clampedPort.GetInputValue(clamped01);

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(input));
            clampedPort = GetInputPort(nameof(clamped01));
        }

        private NodePort inputPort;
        private NodePort clampedPort;

        public static float Ease(float t, EasingMode mode)
        {
            switch (mode)
            {
                case EasingMode.Unset: return 0f;
                case EasingMode.Linear: return Easing.Linear(t);
                case EasingMode.InSine: return Easing.InSine(t);
                case EasingMode.OutSine: return Easing.OutSine(t);
                case EasingMode.InOutSine: return Easing.InOutSine(t);
                case EasingMode.InQuad: return Easing.InQuad(t);
                case EasingMode.OutQuad: return Easing.OutQuad(t);
                case EasingMode.InOutQuad: return Easing.InOutQuad(t);
                case EasingMode.InCubic: return Easing.InCubic(t);
                case EasingMode.OutCubic: return Easing.OutCubic(t);
                case EasingMode.InOutCubic: return Easing.InOutCubic(t);
                case EasingMode.InQuart: return Easing.InPower(t, 4);
                case EasingMode.OutQuart: return Easing.OutPower(t, 4);
                case EasingMode.InOutQuart: return Easing.InOutPower(t, 4);
                case EasingMode.InQuint: return Easing.InPower(t, 5);
                case EasingMode.OutQuint: return Easing.OutPower(t, 5);
                case EasingMode.InOutQuint: return Easing.InOutPower(t, 5);
                case EasingMode.InExpo: return t == 0.0f ? 0.0f : Mathf.Pow(1024f, t - 1f);
                case EasingMode.OutExpo: return t == 1.0f ? 1.0f : 1.0f - Mathf.Pow(2f, -10f * t);
                case EasingMode.InOutExpo:
                {
                    if (t == 0.0f)
                        return 0.0f;
                    else if (t == 1.0f)
                        return 1.0f;
                    else if ((t *= 2) < 1.0f)
                        return 0.5f * Mathf.Pow(1024f, t - 1f);
                    else
                        return 0.5f * (-Mathf.Pow(2f, -10f * (t - 1f)) + 2f);
                }
                case EasingMode.InCirc:
                {
                    if (t > 1f)
                    {
                        t = 1f;
                    }
                    return Easing.InCirc(t);
                }
                case EasingMode.OutCirc:
                {
                    if (t < 0f)
                    {
                        t = 0;
                    }
                    return Easing.OutCirc(t);
                }
                case EasingMode.InOutCirc:  return Easing.InOutCirc(t);
                case EasingMode.InElastic:  return Easing.InElastic(t);
                case EasingMode.OutElastic: return Easing.OutElastic(t);
                case EasingMode.InOutElastic: return Easing.InOutElastic(t);
                case EasingMode.InBack:     return Easing.InBack(t);
                case EasingMode.OutBack:    return Easing.OutBack(t);
                case EasingMode.InOutBack:  return Easing.InOutBack(t);
                case EasingMode.InBounce:   return Easing.InBounce(t);
                case EasingMode.OutBounce:  return Easing.OutBounce(t);
                case EasingMode.InOutBounce:return Easing.InOutBounce(t);
                default: return t;
 
            }
        }

        public override object GetValue(NodePort port)
        {
            input = inputPort.GetInputValue(input);
            if (Clamped01)
            {
                input = Mathf.Clamp01(input);
            }

            output = Ease(input, easingMode);
            return output;
        }
    }
}
