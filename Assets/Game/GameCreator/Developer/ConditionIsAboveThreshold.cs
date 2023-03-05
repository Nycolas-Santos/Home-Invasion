using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 0)]
    
[Title("Is Above Threshold")]
[Description("Checks if a Target gameObject is above a Source gameObject by a threshold")]

[Parameter("Source", "The Source GameObject")]
[Parameter("Target", "The Target GameObject")]
[Parameter("Threshold", "Threshold value")]

[Category("Transforms/Is Above Threshold")]

[Keywords("Is", "Above", "Threshold", "Distance")]
[Image(typeof(IconLocation), ColorTheme.Type.Yellow)]

[Serializable]
public class ConditionIsAboveThreshold : Condition
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_Source = new PropertyGetGameObject();// The source gameObject to check against
    [SerializeField] private PropertyGetGameObject m_Target = new PropertyGetGameObject(); // The target gameObject to check
    [SerializeField] private PropertyGetDecimal m_Threshold = new PropertyGetDecimal(); // The minimum distance the target must be above the source
    
    // PROPERTIES: ----------------------------------------------------------------------------
    
    protected override string Summary => 
        $"Is {this.m_Target} Above {this.m_Source} by {this.m_Threshold} Units";
    
    // RUN METHOD: ----------------------------------------------------------------------------
    protected override bool Run(Args args)
    {
        var source = this.m_Source.Get(args);
        var target = this.m_Target.Get(args);
        var threshold = this.m_Threshold.Get(args);
        
        if (source == null || target == null) return false;

        // Check if the target is above the source by at least the threshold distance
        float distance = target.transform.position.y - source.transform.position.y;
        if (distance > threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
