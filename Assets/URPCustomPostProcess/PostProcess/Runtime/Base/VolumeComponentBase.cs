using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace FRMN.PostProcess
{
    public interface IVolumeComponent
    {
        /// <summary>
        /// VolumeComponent��Active��Ԏ擾
        /// ������ŏ����w��
        /// </summary>
        bool IsActive { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>�p����ő������x���w��[Serializable, VolumeComponentMenuForRenderPipeline("FRMN/ProcessName", typeof(UniversalRenderPipeline))]</remarks>
    public abstract class VolumeComponentBase : VolumeComponent , IPostProcessComponent
    {
        /// <summary>
        /// VolumeComponent��Active��Ԏ擾
        /// ������ŏ����w��
        /// </summary>
        public abstract bool IsActive();

        public abstract bool IsTileCompatible();
    }
}
