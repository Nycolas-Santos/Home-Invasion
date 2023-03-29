using UnityEngine;

// using Unity.Entities;

namespace NinjutsuGames.StateMachine.Runtime
{
    /// <summary>
    /// Graph processor
    /// </summary>
    public abstract class BaseGraphProcessor
    {
        protected StateMachineAsset graph;
        public GameObject context;

        /// <summary>
        /// Manage graph scheduling and processing
        /// </summary>
        /// <param name="graph">Graph to be processed</param>
        public BaseGraphProcessor(StateMachineAsset graph, GameObject context)
        {
            this.graph = graph;
            this.context = context;

            UpdateComputeOrder();
        }

        public abstract void UpdateComputeOrder();

        /// <summary>
        /// Schedule the graph into the job system
        /// </summary>
        public abstract void Run();

        public void RunNode(string nodeId, GameObject context)
        {
            if(!graph.nodesPerGUID.ContainsKey(nodeId))
            {
                Debug.Log($"Couldn't find node with id: {nodeId} in graph: {graph.name}. Make sure you are targeting the correct State Machine Runner.");
                return;
            }
            graph.nodesPerGUID[nodeId].OnProcess(context);
        }
    }
}