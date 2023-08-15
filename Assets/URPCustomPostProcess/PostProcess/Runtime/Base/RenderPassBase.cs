using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace FRMN.PostProcess
{
    public abstract class RenderPassBase<T> : ScriptableRenderPass where T : VolumeComponent, IVolumeComponent
    {
        protected readonly Material _material;

        // フレームデバッガ―上の名前表示
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
            // Profiler初期化
            _profilingSampler = new ProfilingSampler(_renderTag);
        }

        /// <summary>
        /// 処理実行のオンオフをRenderPass側で制御
        /// </summary>
        protected virtual bool IsActive => true;

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (_material == null)
            {
                return;
            }

            // カメラのポストプロセス設定が無効なら処理しない
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

            // コマンドバッファ生成
            var cmd = CommandBufferPool.Get(_renderTag);

            // プロファイル
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