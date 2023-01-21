using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Cameras;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[Version(1, 0, 3)]

[Dependency("VHS Pro",2,0,0)]
[Title("Change VHS Film Grain")]
[Description("Changes the Film Grain value of an VHS Script Rig")]

[Parameter("Dependency", "This instruction requires the 'VHS Pro' Asset from Vladimir Storm")]
[Parameter("Camera", "The game object that contains a postVHSPro Script (probably the Main Camera)")]
[Parameter("Enabled", "Boolean value of the property Twitch Horizontal")]
[Parameter("Value", "Value to set the Twitch Horizontal Frequency")]
[Parameter("Duration", "How long it takes to perform the transition")]
[Parameter("Easing", "The change rate of the parameter over time")]
[Parameter("Wait to Complete", "Whether to wait until the transition is finished")]

[Category("VHS Pro/Properties/Change Film Grain")]

[Keywords("VHS", "Film", "Grain", "Change", "Set")]
[Image(typeof(IconCamera), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionVHSPropertyFilmGrain : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_Camera = new PropertyGetGameObject();
    [SerializeField] private PropertyGetBool m_Enabled = new PropertyGetBool();
    [SerializeField, Range(0,0.1f)] private float m_Value = 0;
    [SerializeField] private Transition m_Transition = new Transition();

    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => 
        $"{this.m_Camera} Film Grain to {this.m_Value} and Enabled = {this.m_Enabled}";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override async Task Run(Args args)
    {
        var vhs = this.m_Camera.Get<postVHSPro>(args);
        var enabled = this.m_Enabled.Get(args);
        
        if (vhs == null) return;

        vhs.filmgrainOn = enabled;
        var valueSource = vhs.filmGrainAmount;
        var valueTarget = this.m_Value;

        ITweenInput tween = new TweenInput<float>(
            valueSource,
            valueTarget,
            this.m_Transition.Duration,
            (a, b, t) => vhs.filmGrainAmount = Mathf.Lerp(a, b, t),
            Tween.GetHash(typeof(Rig), "property:vhs-twitch-horizontal-frequency"),
            this.m_Transition.EasingType,
            this.m_Transition.Time
        );
        
        Tween.To(vhs.gameObject, tween);
        if (this.m_Transition.WaitToComplete) await this.Until(() => tween.IsFinished);
    }
}
