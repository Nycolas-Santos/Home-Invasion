using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 1)]
    
[Title("Change Canvas Render Camera")]
[Description("Changes the Render Camera property of a Canvas")]

[Parameter("Canvas", "The game object that contains a Canvas Component")]
[Parameter("Camera", "The Game Object that contains a Camera Component")]

[Category("UI/Change Canvas Render Camera")]

[Keywords("Canvas", "Camera", "Render", "Set", "Change")]
[Image(typeof(IconCamera), ColorTheme.Type.Gray)]

[Serializable]
public class InstructionChangeCanvasRenderCamera : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_Canvas = new PropertyGetGameObject();
    [SerializeField] private PropertyGetGameObject m_Camera = new PropertyGetGameObject();
    [SerializeField] private PropertyGetDecimal m_PlaneDistance = new PropertyGetDecimal();

    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => 
        $"Set Canvas Render Camera To:{this.m_Camera}";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        var canvas = this.m_Canvas.Get<Canvas>(args);
        var camera = this.m_Camera.Get<Camera>(args);
        
        var planeDistance = (float) this.m_PlaneDistance.Get(args);

        if (canvas == null || camera == null) return DefaultResult;

        canvas.worldCamera = camera;
        canvas.planeDistance = planeDistance;
        return DefaultResult;
    }
}
