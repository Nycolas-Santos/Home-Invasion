using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Cameras;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(0, 1, 1)]

    [Title("Change Field of View")]
    [Description("Changes the camera field of view")]

    [Category("Cameras/Properties/Change Field of View")]

    [Parameter("Camera", "The camera component whose property changes")]
    [Parameter("FoV", "The field of view of the camera, measured in degrees")]

    [Keywords("Cameras", "Perspective", "FOV", "3D")]
    [Image(typeof(IconCamera), ColorTheme.Type.Blue)]
    
    [Serializable]
    public class InstructionCameraFOV : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private PropertyGetCamera m_Camera = GetCameraMain.Create;
        
        [FormerlySerializedAs("m_FoV")]
        [SerializeField] private PropertyGetDecimal m_FieldOfView = new PropertyGetDecimal(60f);

        [SerializeField] private PropertyGetDecimal m_Smooth = new PropertyGetDecimal(0.01f);

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string Title => $"Change Field of View to {this.m_FieldOfView}";

        // RUN METHOD: ----------------------------------------------------------------------------
        
        protected override Task Run(Args args)
        {
            TCamera camera = this.m_Camera.Get(args);
            if (camera == null) return DefaultResult;

            float fov = (float) this.m_FieldOfView.Get(args);
            float smooth = (float) this.m_Smooth.Get(args);

            camera.Viewport.SetFieldOfView(fov, smooth);
            return DefaultResult;
        }
    }
}