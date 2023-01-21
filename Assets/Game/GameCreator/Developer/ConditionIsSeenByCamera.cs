using System;
using GameCreator.Runtime.Cameras;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 0)]
    
[Title("Is Seen By Camera")]
[Description("Checks if a Game Objects is Seen by a Camera (this is not raycast, this method is checking if the target pivot is within the camera frustum)")]

[Parameter("Camera", "The Game object that contains a Camera Component")]
[Parameter("Target", "The Game Object to check if is within the Camera Frustum")]

[Category("Cameras/Is Seen By Camera")]

[Keywords("Seen", "Camera", "Angle", "Frustum", "Is", "By", "Camera")]
[Image(typeof(IconCamera), ColorTheme.Type.Yellow)]

[Serializable]
public class ConditionIsSeenByCamera : Condition
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_Camera = new PropertyGetGameObject();
    [SerializeField] private PropertyGetGameObject m_Target = new PropertyGetGameObject();
    // PROPERTIES: ----------------------------------------------------------------------------
    
    protected override string Summary => 
        $"Is {this.m_Target} Seen by {this.m_Camera}";
    
    // RUN METHOD: ----------------------------------------------------------------------------
    protected override bool Run(Args args)
    {
        var camera = this.m_Camera.Get<Camera>(args);
        var target = this.m_Target.Get(args);
        
        if (camera == null) return false;
        if (target == null) return false;
        
        var screenPoint = camera.WorldToViewportPoint(target.transform.position);
        
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
