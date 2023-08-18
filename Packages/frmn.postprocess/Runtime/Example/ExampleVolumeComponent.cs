using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace FRMN.PostProcess
{
    [Serializable, VolumeComponentMenuForRenderPipeline("FRMN/Example", typeof(UniversalRenderPipeline))]
    public sealed class ExampleVolumeComponent : VolumeComponent, IVolumeComponent
    {
        // Implement
        public bool IsActive => IsOn.value;

        // Parameter
        [Header("Example")]
        public BoolParameter IsOn = new BoolParameter(false);

    }
}
