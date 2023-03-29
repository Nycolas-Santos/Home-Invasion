using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Event = GameCreator.Runtime.VisualScripting.Event;

namespace NinjutsuGames.StateMachine.Runtime
{
    [System.Serializable, NodeMenuItem("Trigger Node")]
    public class TriggerNode : BaseGameCreatorNode, ICreateNodeFrom<Trigger>
    {
        [Output("Out"), Vertical] public TriggerPortOut output;
        
        [SerializeReference] public Event triggerEvent = new EventOnStart();

        public override string name => "Trigger";
        
        public override string layoutStyle => "GraphProcessorStyles/TriggerNode";

        public override Color color => new Color(0.4f, 0.12f, 0.11f);

        protected override void Process(GameObject context, Args customArgs = null)
        {
            this.context = context;
            
            var runner = context.GetCached<TriggerRunner>(NodeId);
            var args = customArgs ?? GetArgs(context);
            runner.Setup(triggerEvent.GetCachedData(NodeId), args, OnTriggerExecuted, OnTriggerStopped);
        }

        private void OnTriggerStopped()
        {
            OnStopRunning();
        }

        private void OnTriggerExecuted(Args args)
        {
            if(enabledForExecution == false) return;

            OnStartRunning();
            RunChildNodes(args);
        }
        
        public bool InitializeNodeFromObject(Trigger value)
        {
#if UNITY_EDITOR
            triggerEvent = value.GetTriggerEvent().Clone();
            return triggerEvent != null;
#else
            return true;
#endif
        }
    }
}