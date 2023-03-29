using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.StateMachine.Runtime
{
    [Serializable, NodeMenuItem("Actions Node")]
    public class ActionsNode : BaseGameCreatorNode, ICreateNodeFrom<Actions>
    {
        [Input("In", true)] public ActionPortIn input;
        [Input("In", true), Vertical] public TriggerPortIn input2;
        [Output("Out")] public ActionPortOut output;
        
        public InstructionList instructions = new();
        [SerializeReference] public NetworkingSettings networkingSettings;
        
        [NonSerialized] private GameObject runner;

        public override string name => "Actions";
        public override string layoutStyle => "GraphProcessorStyles/ActionsNode";

        protected override void Process(GameObject context, Args customArgs = null)
        {
            this.context = context;
            if(!Application.isPlaying) return;
            if(enabledForExecution == false) return;
            
            var args = customArgs ?? GetArgs(context);
            var runner = context.GetCached<ActionsRunner>(NodeId);
            if(runner.IsRunning) return;
            OnStartRunning();

            runner.Run(instructions.GetCachedData(NodeId), args.Clone, (args) =>
            {
                if(!Application.isPlaying) return;
                OnStopRunning();
                RunChildNodes(args);
            });
        }
        
        public bool InitializeNodeFromObject(Actions value)
        {
#if UNITY_EDITOR
            instructions = value.GetInstructionsList().Clone();
            return instructions != null;
#else
            return true;
#endif
        }
    }
}