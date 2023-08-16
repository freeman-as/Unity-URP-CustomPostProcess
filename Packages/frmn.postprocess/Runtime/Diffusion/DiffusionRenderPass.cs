using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FRMN.PostProcess
{
    public sealed class DiffusionRenderPass : RenderPassBase<DiffusionVolumeComponent>
    {
        protected override string _renderTag => nameof(DiffusionRenderPass);

        #region Shader Property
        // IDキャッシュ
        private static readonly int SourceTexId = Shader.PropertyToID("_SourceTex");
        private static readonly int TempBlurBuffer1 = Shader.PropertyToID("_TempBlurBuffer1");
        private static readonly int TempBlurBuffer2 = Shader.PropertyToID("_TempBlurBuffer2");
        private static readonly int BlurTexId = Shader.PropertyToID("_BlurTex");
        private static readonly int ContrastId = Shader.PropertyToID("_Contrast");
        private static readonly int IntensityId = Shader.PropertyToID("_Intensity");
        #endregion

        public DiffusionRenderPass(Shader shader, RenderPassEvent renderEvent) : base(shader, renderEvent)
        {

        }

        protected override void BeforeRender(CommandBuffer cmd, ref RenderingData renderingData)
        {
            _material.SetFloat(ContrastId, _volumeComponent.contrast.value);
            _material.SetFloat(IntensityId, _volumeComponent.intensity.value);
        }


        protected override void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            var cameraData = renderingData.cameraData;
            var originalBuffer = cameraData.renderer.cameraColorTarget;
            var w = cameraData.camera.scaledPixelWidth / 2;
            var h = cameraData.camera.scaledPixelHeight / 2;

            cmd.GetTemporaryRT(TempBlurBuffer1, w, h);

            cmd.SetGlobalTexture(SourceTexId, renderingData.cameraData.renderer.cameraColorTarget);
            Blit(cmd, renderingData.cameraData.renderer.cameraColorTarget, TempBlurBuffer1, _material, 0);

            cmd.GetTemporaryRT(TempBlurBuffer2, w, h);

            cmd.SetGlobalTexture(SourceTexId, TempBlurBuffer1);
            Blit(cmd, TempBlurBuffer1, TempBlurBuffer2, _material, 1);

            cmd.SetGlobalTexture(SourceTexId, TempBlurBuffer2);
            Blit(cmd, TempBlurBuffer2, TempBlurBuffer1, _material, 2);

            cmd.ReleaseTemporaryRT(TempBlurBuffer2);

            cmd.SetGlobalTexture(SourceTexId, renderingData.cameraData.renderer.cameraColorTarget);
            cmd.SetGlobalTexture(BlurTexId, TempBlurBuffer1);
            Blit(cmd, ref renderingData, _material, 3);

            cmd.ReleaseTemporaryRT(TempBlurBuffer1);
        }
    }
}
