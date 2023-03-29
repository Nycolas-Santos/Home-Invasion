using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.StateMachine.Runtime
{
    [System.Serializable, NodeMenuItem("Branch Node")]
    public class BranchNode : BaseGameCreatorNode//, ICreateNodeFrom<Actions>
    {
        [Input("In", true), Vertical] public BranchPortIn input;
        [Output("Out"), Vertical] public BranchPortOut output;

        public Branch branch = new();

        public override string name => "Branch";
        
        public override string layoutStyle => "GraphProcessorStyles/BranchNode";

        protected override void Process(GameObject context, Args customArgs = null)
        {
            this.context = context;
            if(!Application.isPlaying) return;
            if(enabledForExecution == false) return;
            
            var args = customArgs ?? GetArgs(context).Clone;

            var runner = context.GetCached<BranchRunner>(NodeId);
            if(runner.IsRunning) return;

            OnStartRunning();

            runner.Run(branch.GetCachedData(NodeId), args, (result) =>
            {
                if(!Application.isPlaying) return;

                OnStopRunning(result);
                if(!result) RunChildNodes(args);
            });
        }
    }
}