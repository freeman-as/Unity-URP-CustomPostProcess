using UnityEngine;
using UnityEngine.Rendering.Universal;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace FRMN.PostProcess
{
    [DisallowMultipleRendererFeature("FRMN/Diffusion")]
    public sealed class DiffusionRendererFeature : ScriptableRendererFeature
    {
        [SerializeField]
        private Shader _shader;

        [SerializeField]
        private RenderPassEvent _renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

        private DiffusionRenderPass _renderPass;

        public override void Create()
        {
            _renderPass = new DiffusionRenderPass(_shader, _renderPassEvent);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_renderPass);
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(DiffusionRendererFeature))]
    public class DiffusionRendererFeatureEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var shader = serializedObject.FindProperty("_shader");
            shader.objectReferenceValue = Shader.Find("Hidden/FRMN/PostProcess/Diffusion");

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
