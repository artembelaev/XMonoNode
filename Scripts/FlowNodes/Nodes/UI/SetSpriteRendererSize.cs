using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Set SpriteRenderer Size", 441)]
    public class SetSpriteRendererSize : SetObjectParameter<SpriteRenderer, Vector2>
    {
        protected override void SetValue(SpriteRenderer obj, Vector2 value)
        {
            obj.size = value;
        }
    }
}
