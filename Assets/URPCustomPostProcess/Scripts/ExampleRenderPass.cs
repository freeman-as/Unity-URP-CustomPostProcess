using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace URPCustomPostProcess
{
    public sealed class ExampleRenderPass : ScriptableRenderPass
    {
        public ExampleRenderPass(RenderPassEvent renderPassEvent)
        {
            this.renderPassEvent = renderPassEvent;
        }

        public void SetUp(RenderTargetIdentifier textureId)
        {

        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {

        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            throw new System.NotImplementedException();
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {

        }
    }
}
