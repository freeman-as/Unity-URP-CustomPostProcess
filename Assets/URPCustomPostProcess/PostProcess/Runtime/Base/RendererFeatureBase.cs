using UnityEngine;
using UnityEngine.Rendering.Universal;


namespace FRMN.PostProcess
{
    public abstract class RendererFeatureBase<T> : ScriptableRendererFeature where T : ScriptableRenderPass
    {
        [SerializeField]
        protected Shader _shader;

        [SerializeField]
        protected RenderPassEvent _renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

        protected T _renderPass;

    }
}
