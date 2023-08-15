using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace URPCustomPostProcess
{
    [System.Serializable]
    public sealed class ExampleRenderPassSettings
    {
        public RenderPassEvent PassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    public class ExampleRendererFeature : ScriptableRendererFeature
    {
        [SerializeField]
        private ExampleRenderPassSettings _settings = new ExampleRenderPassSettings();

        private ExampleRenderPass _renderPass;

        /// <inheritdoc/>
        public override void Create()
        {
            var passEvent = _settings.PassEvent;
            _renderPass = new ExampleRenderPass(passEvent);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            _renderPass.SetUp(renderer.cameraColorTarget);
            renderer.EnqueuePass(_renderPass);
        }
    }
}
