using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Scripts.AI;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 0)]
    
[Title("Set Stalker State")]
[Description("Sets the current Stalker State")]

[Category("Custom/Set Stalker State")]

[Keywords("Set", "Stalker", "Stalker", "Current")]
[Image(typeof(IconCharacter), ColorTheme.Type.Yellow)]

public class InstructionSetStalkerState : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    
    [SerializeField] private Stalker.State m_State = Stalker.State.Idle;
    
    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => 
        $"Set Stalker State to: {this.m_State}";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        var stalker = Object.FindObjectOfType<Stalker>();
        
        if (stalker == null) return DefaultResult;
        
        stalker.ChangeState(this.m_State);
        return DefaultResult;
    }
}
