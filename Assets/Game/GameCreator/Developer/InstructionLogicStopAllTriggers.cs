using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Version(1, 0, 2)]
    
[Title("Stop All Triggers")]
[Description("Stops all triggers nested under the gameObject")]

[Category("Logic/Stop All Triggers")]


[Keywords("Cancel", "Pause", "Stop","All","Triggers")]
[Image(typeof(IconTriggers), ColorTheme.Type.Red, typeof(OverlayCross))]

[Serializable]
public class InstructionLogicStopAllTriggers : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------

    [SerializeField] private PropertyGetGameObject m_Target = new PropertyGetGameObject();

    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => $"Stop all triggers nested under {this.m_Target}";

    // RUN METHOD: ----------------------------------------------------------------------------

    protected override Task Run(Args args)
    {
        var target = this.m_Target.Get(args);

        if (target == null) return DefaultResult;

        var triggers = target.GetComponentsInChildren<Trigger>(true);
        if (triggers.Length == 0) return DefaultResult;
        
        foreach (var trigger in triggers)
        {
            if (trigger.IsExecuting) trigger.Cancel();
        }
        return DefaultResult;
    }
}
