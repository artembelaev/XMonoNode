using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/GetRendererMaterial", 470)]
    [NodeWidth(190)]
    public class GetRendererMaterial : GetObjectParameter<Renderer, Material>
    {
        [Input(connectionType: ConnectionType.Override), Hiding]
        public int      materialIndex = 0;
        [Hiding]
        public bool shared = false;

        private NodePort materialIndexPort = null;

        protected override void Init()
        {
            base.Init();

            materialIndexPort = GetInputPort(nameof(materialIndex));
        }

        protected override Material GetValue(Renderer obj)
        {
            int index = materialIndexPort.GetInputValue(materialIndex);
#if UNITY_EDITOR
            return Application.isPlaying ?
                (shared ? obj.sharedMaterials[index] : obj.materials[index]) :
                obj.sharedMaterials[index];
#else
            return shared ? obj.sharedMaterials[index] : obj.materials[index];
#endif

        }
    }
}
