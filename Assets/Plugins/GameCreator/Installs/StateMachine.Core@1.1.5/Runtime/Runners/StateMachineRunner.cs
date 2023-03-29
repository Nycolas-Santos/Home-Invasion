using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.StateMachine.Runtime
{
    [AddComponentMenu("Game Creator/State Machine/State Machine Runner")]
    public class StateMachineRunner : TLocalVariables, INameVariable
    {
        [FormerlySerializedAs("stateMachine")] public StateMachineAsset stateMachineAsset;
        private BaseGraphProcessor processor;
        
        
        // MEMBERS: -------------------------------------------------------------------------------
    
        [SerializeField] private Runtime.NameVariableRuntime m_Runtime = new();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public IdString UniqueId => m_SaveUniqueID.Get;

        public Runtime.NameVariableRuntime Runtime => m_Runtime;
        
        // EVENTS: --------------------------------------------------------------------------------
        
        private event Action<string> EventChange;

        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void Awake()
        {
            m_Runtime.OnStartup();
            m_Runtime.EventChange += OnRuntimeChange;
            
            base.Awake();
            
            if (stateMachineAsset != null) processor = new StateMachineGraphProcessor(stateMachineAsset, gameObject);
            // stateMachineAsset.LinkToObject(this);
        }
        
        private void Start()
        {
            processor?.Run();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            CacheUtils.Prune();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public bool Exists(string name)
        {
            return m_Runtime.Exists(name);
        }
        
        public object Get(string name)
        {
            return m_Runtime.Get(name);
        }

        public void Set(string name, object value)
        {
            m_Runtime.Set(name, value);
        }

        public void Register(Action<string> callback, GameObject target)
        {
            EventChange += callback;
        }

        public void Unregister(Action<string> callback, GameObject target)
        {
            EventChange -= callback;
        }

        public void Register(Action<string> callback)
        {
            EventChange += callback;
        }
        
        public void Unregister(Action<string> callback)
        {
            EventChange -= callback;
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void OnRuntimeChange(string name)
        {
            EventChange?.Invoke(name);
        }

        // IGAMESAVE: -----------------------------------------------------------------------------

        public override Type SaveType => typeof(SaveSingleNameVariables);

        public override object SaveData => m_SaveUniqueID.SaveValue
            ? new SaveSingleNameVariables(m_Runtime)
            : null;

        public override Task OnLoad(object value)
        {
            SaveSingleNameVariables saveData = value as SaveSingleNameVariables;
            if (saveData != null && m_SaveUniqueID.SaveValue)
            {
                NameVariable[] candidates = saveData.Variables.ToArray();
                foreach (NameVariable candidate in candidates)
                {
                    if (!m_Runtime.Exists(candidate.Name)) continue;
                    m_Runtime.Set(candidate.Name, candidate.Value);
                }
            }
            
            return Task.FromResult(saveData != null || !m_SaveUniqueID.SaveValue);
        }

        public void RunNode(string nodeId, GameObject context)
        {
            processor.RunNode(nodeId, context);
        }
    }
}