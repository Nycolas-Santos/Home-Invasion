using System;
using System.Collections.Generic;
using System.Linq;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.StateMachine.Runtime
{
    public class BaseGameCreatorNode : BaseNode
    {
        public override bool isRenamable => true;
        public override bool needsInspector => true;
        public override bool hideControls => true;
        
        public GameObject Context => context;

        protected List<int> IsContextRunning { get; } = new();

        protected int NodeId => context.GetInstanceID() + GUID.GetHashCode();
        public delegate void StartRunningDelegate();
        public delegate void StopRunningDelegate(bool result);

        public event StartRunningDelegate eventStartRunning;
        public event StopRunningDelegate eventStopRunning;
        [NonSerialized] private Args args;
        [NonSerialized] protected GameObject context;

        public void SetArgs(Args args)
        {
            this.args = args;
        }

        protected Args GetArgs(GameObject fallbackTarget)
        {
            return new Args(fallbackTarget);
        }

        protected IEnumerable<BaseNode> GetChildNodes()
        {
            return GetOutputNodes().Where(n => n.enabledForExecution && n is ActionsNode or ConditionsNode or StateMachineNode or BranchNode);
        }

        protected void RunChildNodes(Args args)
        {
            var nodes = GetChildNodes();

            foreach (var baseNode in nodes)
            {
                var node = (BaseGameCreatorNode) baseNode;
                node.SetArgs(args);
                node.OnProcess(args.Self);
            }
        }

        protected void OnStartRunning()
        {
            if(!Application.isPlaying) return;

            IsContextRunning.Add(NodeId);
            eventStartRunning?.Invoke();
        }

        protected void OnStopRunning(bool runResult = true)
        {
            if(!Application.isPlaying) return;

            IsContextRunning.Remove(NodeId);
            eventStopRunning?.Invoke(runResult);
        }
    }
}