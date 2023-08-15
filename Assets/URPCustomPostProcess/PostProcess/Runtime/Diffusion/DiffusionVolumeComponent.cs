using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace FRMN.PostProcess
{
    [Serializable, VolumeComponentMenuForRenderPipeline("FRMN/Diffusion", typeof(UniversalRenderPipeline))]
    public sealed class DiffusionVolumeComponent : VolumeComponent, IVolumeComponent
    {
        // Implement
        public bool IsActive => 0f < intensity.value;

        // Parameter
        [Header("Diffusion")]
        public FloatParameter contrast = new FloatParameter(1.0f);
        public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);
    }
}
