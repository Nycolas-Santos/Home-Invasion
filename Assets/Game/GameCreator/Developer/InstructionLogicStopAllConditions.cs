using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Version(1, 0, 2)]
    
[Title("Stop All Conditions")]
[Description("Stops all Conditions nested under the gameObject")]

[Category("Logic/Stop All Conditions")]


[Keywords("Cancel", "Pause", "Stop","All","Conditions")]
[Image(typeof(IconTriggers), ColorTheme.Type.Red, typeof(OverlayCross))]

[Serializable]
public class InstructionLogicStopAllConditions : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------

    [SerializeField] private PropertyGetGameObject m_Target = new PropertyGetGameObject();

    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => $"Stop all Conditions nested under {this.m_Target}";

    // RUN METHOD: ----------------------------------------------------------------------------

    protected override Task Run(Args args)
    {
        var target = this.m_Target.Get(args);

        if (target == null) return DefaultResult;

        var conditions = target.GetComponentsInChildren<Conditions>(true);
        if (conditions.Length == 0) return DefaultResult;
        
        foreach (var condition in conditions)
        {
            if (condition.IsRunning) condition.Cancel();
        }
        return DefaultResult;
    }
}
