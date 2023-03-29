using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using NinjutsuGames.StateMachine.Runtime.Common;
using NinjutsuGames.StateMachine.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.StateMachine.Runtime
{
    [Title("Run State Machine Node")]
    [Description("Executes a State Machine node from an specific target runner")]

    [Category("State Machine/Run State Machine Node")]

    [Parameter(
        "Target",
        "The target GameObject that contains the State Machine Runner"
    )]

    [Parameter(
        "Node",
        "The node to execute from the specified State Machine"
    )]
    
    [Keywords("Execute", "Call", "Instruction", "Action", "State Machine", "Run")]
    [Image(typeof(IconStateMachine), ColorTheme.Type.Yellow, typeof(OverlayArrowRight))]
    
    [Serializable]
    public class InstructionStateMachineRunNode : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private PropertyGetString m_Node = GetNodeStateMachine.Create;
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectTarget.Create();
        // [SerializeField] private bool m_WaitToFinish = true;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Run {m_Node} on {m_Target}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            var runner = m_Target.Get<StateMachineRunner>(args);
            if (runner == null) return DefaultResult;
            var nodeId = m_Node.Get(args);
            runner.Get<StateMachineRunner>().RunNode(nodeId, args.Target);
            // var stateMachine = ((GetNodeStateMachine)m_Node).GetStateMachine();
            // var processor = new StateMachineGraphProcessor(stateMachine, args);
            // processor.Run();
            return DefaultResult;
        }
    }
}