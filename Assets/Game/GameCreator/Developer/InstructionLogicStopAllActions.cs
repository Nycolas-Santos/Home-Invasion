using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Version(1, 0, 2)]
    
[Title("Stop All Actions")]
[Description("Stops all actions nested under the gameObject")]

[Category("Logic/Stop All actions")]


[Keywords("Cancel", "Pause", "Stop","All","Actions")]
[Image(typeof(IconTriggers), ColorTheme.Type.Red, typeof(OverlayCross))]

[Serializable]
public class InstructionLogicStopAllActions : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------

    [SerializeField] private PropertyGetGameObject m_Target = new PropertyGetGameObject();

    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => $"Stop all actions nested under {this.m_Target}";

    // RUN METHOD: ----------------------------------------------------------------------------

    protected override Task Run(Args args)
    {
        var target = this.m_Target.Get(args);

        if (target == null) return DefaultResult;

        var actions = target.GetComponentsInChildren<Actions>(true);
        if (actions.Length == 0) return DefaultResult;
        
        foreach (var action in actions)
        {
            if (action.IsRunning) action.Cancel();
        }
        return DefaultResult;
    }
}
