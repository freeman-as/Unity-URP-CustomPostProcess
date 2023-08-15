using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace FRMN.PostProcess
{
    public abstract class RenderPassBase<T> : ScriptableRenderPass where T : VolumeComponent, IVolumeComponent
    {
        protected readonly Material _material;

        // �t���[���f�o�b�K�\��̖��O�\��
        protected abstract string _renderTag { get; }

        private readonly ProfilingSampler _profilingSampler;

        private T __volumeComponent; // InnerVolumeComponent
        protected T _volumeComponent => __volumeComponent;

        protected RenderPassBase(Shader shader, RenderPassEvent renderEvent)
        {
            if (shader != null)
            {
                _material = CoreUtils.CreateEngineMaterial(shader);
            }

            renderPassEvent = renderEvent;
            // Profiler������
            _profilingSampler = new ProfilingSampler(_renderTag);
        }

        /// <summary>
        /// �������s�̃I���I�t��RenderPass���Ő���
        /// </summary>
        protected virtual bool IsActive => true;

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (_material == null)
            {
                return;
            }

            // �J�����̃|�X�g�v���Z�X�ݒ肪�����Ȃ珈�����Ȃ�
            if (!renderingData.cameraData.postProcessEnabled)
            {
                return;
            }

            var volumeStack = VolumeManager.instance.stack;
            __volumeComponent = volumeStack.GetComponent<T>();

            if (__volumeComponent == null
                || !__volumeComponent.active
                || __volumeComponent.IsActive)
            {
                return;
            }

            if (!IsActive)
            {
                return;
            }

            // �R�}���h�o�b�t�@����
            var cmd = CommandBufferPool.Get(_renderTag);

            // �v���t�@�C��
            using (new ProfilingScope(cmd, _profilingSampler))
            {
                BeforeRender(cmd, ref renderingData);
                Render(cmd, ref renderingData);
                AfterRender(cmd, ref renderingData);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        protected virtual void BeforeRender(CommandBuffer cmd, ref RenderingData renderingData)
        {
        }

        protected abstract void Render(CommandBuffer cmd, ref RenderingData renderingData);

        protected virtual void AfterRender(CommandBuffer cmd, ref RenderingData renderingData)
        {
        }

    }
}