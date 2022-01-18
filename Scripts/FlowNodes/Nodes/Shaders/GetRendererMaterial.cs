using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/GetRendererMaterial", 470)]
    [NodeWidth(190)]
    public class GetRendererMaterial : GetObjectParameter<Renderer, Material>
    {
        [Hiding]
        public bool shared = false;

        protected override Material GetValue(Renderer obj)
        {
#if UNITY_EDITOR
            return Application.isPlaying ?
                (shared ? obj.sharedMaterial : obj.material):
                obj.sharedMaterial;
#else
            return obj.material;
#endif
        }
    }
}
