using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[Version(1, 0, 0)]

[Title("Change Rig Weight")]
[Description("Changes the weight value of an IK Rig")]

[Category("Animation Rigging/Properties/Change Rig Weight")]
    
[Parameter("Rig", "The game object that contains a Rig component")]
[Parameter("Duration", "How long it takes to perform the transition")]
[Parameter("Easing", "The change rate of the parameter over time")]
[Parameter("Wait to Complete", "Whether to wait until the transition is finished")]

[Keywords("Rig", "Weight", "IK", "Change", "Set")]
[Image(typeof(IconSkeleton), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionPropertyRigWeight : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_Rig = new PropertyGetGameObject();
    [SerializeField, Range(0,1)] private float m_Value = 0;
    [SerializeField] private Transition m_Transition = new Transition();
        
    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => 
        $"Rig Weight from {this.m_Rig} to {this.m_Value}";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override async Task Run(Args args)
    {
        var rig = this.m_Rig.Get<Rig>(args);
        
        if (rig == null) return;

        var valueSource = rig.weight;
        var valueTarget = (float) this.m_Value;
        
        ITweenInput tween = new TweenInput<float>(
            valueSource,
            valueTarget,
            this.m_Transition.Duration,
            (a, b, t) => rig.weight = Mathf.Lerp(a, b, t),
            Tween.GetHash(typeof(Rig), "property:rig-weight"),
            this.m_Transition.EasingType,
            this.m_Transition.Time
        );
        
        Tween.To(rig.gameObject, tween);
        if (this.m_Transition.WaitToComplete) await this.Until(() => tween.IsFinished);
        
    }
}
