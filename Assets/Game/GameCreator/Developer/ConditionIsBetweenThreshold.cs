using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Version(1, 0, 0)]
    
[Title("Is Between Threshold")]
[Description("Checks if a Target gameObject is between a Source gameObject by a threshold")]

[Parameter("Source", "The Source GameObject")]
[Parameter("Target", "The Target GameObject")]
[Parameter("Threshold", "Threshold value")]
[Parameter("Axis", "Axis to use as base for comparison")]

[Category("Transforms/Is Between Threshold")]

[Keywords("Is", "Between", "Threshold", "Distance")]
[Image(typeof(IconLocation), ColorTheme.Type.Yellow)]

[Serializable]
public class ConditionIsBetweenThreshold : Condition
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_Source = new PropertyGetGameObject();// The source gameObject to check against
    [SerializeField] private PropertyGetGameObject m_Target = new PropertyGetGameObject(); // The target gameObject to check
    [SerializeField] private PropertyGetDecimal m_Threshold = new PropertyGetDecimal(); // The minimum distance the target must be below the source
    [SerializeField] private Axis m_Axis = Axis.Y; // Add enum to select comparison axis
    // PROPERTIES: ----------------------------------------------------------------------------
    
    protected override string Summary => 
        $"Is {this.m_Target} Between {this.m_Source} by {this.m_Threshold} Units in the {this.m_Axis} Axis";
    
    public enum Axis // Define the enum to select the comparison axis
    {
        X,
        Y,
        Z
    }
    
    // RUN METHOD: ----------------------------------------------------------------------------
    protected override bool Run(Args args)
    {
        var source = this.m_Source.Get(args);
        var target = this.m_Target.Get(args);
        var threshold = this.m_Threshold.Get(args);
        var axis = this.m_Axis;
        if (source == null || target == null) return false;

        float sourcePos = 0f;
        float targetPos = 0f;

        // Select the axis to compare based on the enum value
        switch (axis)
        {
            case Axis.X:
                sourcePos = source.transform.position.x;
                targetPos = target.transform.position.x;
                break;
            case Axis.Y:
                sourcePos = source.transform.position.y;
                targetPos = target.transform.position.y;
                break;
            case Axis.Z:
                sourcePos = source.transform.position.z;
                targetPos = target.transform.position.z;
                break;
            default:
                break;
        }

        if (Mathf.Abs(sourcePos - targetPos) <= threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

