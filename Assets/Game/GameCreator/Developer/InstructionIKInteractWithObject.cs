using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using RootMotion.FinalIK;
using UnityEngine;

[Version(1, 0, 2)]

[Dependency("Final IK",2,0,0)]
[Title("Interact With Object (Final IK)")]
[Description("Uses the default Interaction System from Final IK to Interact with an Object(Final IK)")]

[Category("Animation Rigging/Properties/Interact With Object")]
    
[Parameter("Interaction System", "GameObject with the InteractionSystem component")]
[Parameter("Interaction Object", "Object to interact with, must contain the InteractionObject component")]
[Parameter("Effector", "What effector will it use")]
[Parameter("Can Interrupt", "Can the interaction be interrupted and started again")]

[Keywords("Rig", "Weight", "IK", "Interact", "Set","Final IK","Effector","Biped")]
[Image(typeof(IconSkeleton), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionIKInteractWithObject : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_InteractionSystem = new PropertyGetGameObject();
    [SerializeField] private PropertyGetGameObject m_InteractionObject = new PropertyGetGameObject();
    [SerializeField] private FullBodyBipedEffector m_Effector;
    [SerializeField] private PropertyGetBool m_CanInterrupt = new PropertyGetBool();
    
    // PROPERTIES: ----------------------------------------------------------------------------
    
    public override string Title => 
        $"Interact with {this.m_InteractionObject}'s using the {this.m_Effector}";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        var interactionSystem = this.m_InteractionSystem.Get<InteractionSystem>(args);
        var interactionObject = this.m_InteractionObject.Get<InteractionObject>(args);
        var canInterrupt = this.m_CanInterrupt.Get(args);
        if (interactionSystem == null || interactionObject == null) return DefaultResult;
        
        interactionSystem.StartInteraction(this.m_Effector, interactionObject, canInterrupt);
        Debug.Log("Test");
        return DefaultResult;
    }
}
