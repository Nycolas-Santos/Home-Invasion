using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using RootMotion.FinalIK;
using UnityEngine;

[Version(1, 0, 2)]

[Dependency("Final IK",2,0,0)]
[Title("Change Effector Position Weight (Final IK)")]
[Description("Changes the weight value of an Effector (Final IK)")]

[Category("Animation Rigging/Properties/Change Effector Position Weight")]
    
[Parameter("Rig", "The game object that contains a Full Body Biped IK component")]
[Parameter("Duration", "How long it takes to perform the transition")]
[Parameter("Easing", "The change rate of the parameter over time")]
[Parameter("Wait to Complete", "Whether to wait until the transition is finished")]

[Keywords("Rig", "Weight", "IK", "Change", "Set","Final IK","Effector","Biped")]
[Image(typeof(IconSkeleton), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionPropertyEffectorPosWeight : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_Rig = new PropertyGetGameObject();
    [SerializeField] private FullBodyBipedEffector m_Effector;
    [SerializeField, Range(0,1)] private float m_Value = 0;
    [SerializeField] private Transition m_Transition = new Transition();
        
    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => 
        $"Position Weight from {this.m_Rig}'s {this.m_Effector.ToString()} to {this.m_Value}";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override async Task Run(Args args)
    {
        var rig = this.m_Rig.Get<FullBodyBipedIK>(args);
        
        if (rig == null) return;

        var valueSource = rig.solver.GetEffector(this.m_Effector).positionWeight;
        var valueTarget = (float) this.m_Value;
        
        ITweenInput tween = new TweenInput<float>(
            valueSource,
            valueTarget,
            this.m_Transition.Duration,
            (a, b, t) => rig.solver.GetEffector(this.m_Effector).positionWeight = Mathf.Lerp(a, b, t),
            Tween.GetHash(typeof(FullBodyBipedIK), "property:position-rig-weight"),
            this.m_Transition.EasingType,
            this.m_Transition.Time
        );
        
        Tween.To(rig.gameObject, tween);
        if (this.m_Transition.WaitToComplete) await this.Until(() => tween.IsFinished);
    }
}
