using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace FRMN.PostProcess
{
    public interface IVolumeComponent
    {
        /// <summary>
        /// VolumeComponentのActive状態取得
        /// 実装先で条件指定
        /// </summary>
        bool IsActive { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>継承先で属性ラベル指定[Serializable, VolumeComponentMenuForRenderPipeline("FRMN/ProcessName", typeof(UniversalRenderPipeline))]</remarks>
    public abstract class VolumeComponentBase : VolumeComponent , IPostProcessComponent
    {
        /// <summary>
        /// VolumeComponentのActive状態取得
        /// 実装先で条件指定
        /// </summary>
        public abstract bool IsActive();

        public abstract bool IsTileCompatible();
    }
}
