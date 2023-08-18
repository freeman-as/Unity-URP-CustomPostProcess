using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace FRMN.PostProcess
{
    public sealed class ExampleRenderPass : RenderPassBase<ExampleVolumeComponent>
    {
        protected override string _renderTag => nameof(ExampleRenderPass);
        private static readonly int SourceTexId = Shader.PropertyToID("_SourceTex");

        public ExampleRenderPass(Shader shader, RenderPassEvent renderEvent) : base(shader, renderEvent)
        {
        }

        protected override void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            cmd.SetGlobalTexture(SourceTexId, renderingData.cameraData.renderer.cameraColorTarget);
            Blit(cmd, ref renderingData, _material);
        }
    }
}
