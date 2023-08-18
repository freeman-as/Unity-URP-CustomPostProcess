
using UnityEngine;
using UnityEngine.Rendering.Universal;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace FRMN.PostProcess
{
    [DisallowMultipleRendererFeature("FRMN/Example")]
    public class ExampleRendererFeature : ScriptableRendererFeature
	{
		[SerializeField]
		private Shader _shader;

		[SerializeField]
		private RenderPassEvent _renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

		private ExampleRenderPass _renderPass;

		public override void Create()
		{
			_renderPass = new ExampleRenderPass(_shader, _renderPassEvent);
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(_renderPass);
		}


	}

	#if UNITY_EDITOR
	[CustomEditor(typeof(ExampleRendererFeature))]
	public class ExampleRendererFeatureEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var shader = serializedObject.FindProperty("_shader");
			shader.objectReferenceValue = Shader.Find("Hidden/FRMN/PostProcess/Example");

			serializedObject.ApplyModifiedProperties();
		}
	}
	#endif
}
