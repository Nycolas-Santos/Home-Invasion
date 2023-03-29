using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
// using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.StateMachine.Runtime
{
    [AddComponentMenu("")]
    public class StateMachineVariablesManager : Singleton<StateMachineVariablesManager>, IGameSave
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void OnSubsystemsInit()
        {
            Instance.WakeUp();
        }
        
        // PROPERTIES: ----------------------------------------------------------------------------

        private Dictionary<IdString, NameVariableRuntime> Values;
        private Dictionary<IdString, List<BaseNode>> NodeValues;

        private HashSet<IdString> SaveValues;

        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnCreate()
        {
            base.OnCreate();

            Values = new Dictionary<IdString, NameVariableRuntime>();
            SaveValues = new HashSet<IdString>();

            _ = SaveLoadManager.Subscribe(this);
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool ExistsNode(StateMachineAsset asset, string name)
        {
            RequireInitNode(asset);

            return NodeValues.TryGetValue(asset.UniqueID, out var runtime);
        }
        
        public object GetNode(StateMachineAsset asset, string name)
        {
            RequireInitNode(asset);

            return Values.TryGetValue(asset.UniqueID, out var runtime);
        }
        
        public bool Exists(StateMachineAsset asset, string name)
        {
            RequireInit(asset);

            return Values.TryGetValue(
                asset.UniqueID,
                out var runtime
            ) && runtime.Exists(name);
        }
        
        public object Get(StateMachineAsset asset, string name)
        {
            RequireInit(asset);

            return Values.TryGetValue(asset.UniqueID, out var runtime) 
                ? runtime.Get(name) 
                : null;
        }
        
        public string Title(StateMachineAsset asset, string name)
        {
            RequireInit(asset);

            return Values.TryGetValue(asset.UniqueID, out var runtime) 
                ? runtime.Title(name) 
                : string.Empty;
        }
        
        public Texture Icon(StateMachineAsset asset, string name)
        {
            RequireInit(asset);

            return Values.TryGetValue(asset.UniqueID, out var runtime) 
                ? runtime.Icon(name) 
                : null;
        }

        public void Set(StateMachineAsset asset, string name, object value)
        {
            RequireInit(asset);
            if (!Values.TryGetValue(asset.UniqueID, out var runtime)) return;
            
            runtime.Set(name, value);
            if (asset.Save) this.SaveValues.Add(asset.UniqueID);
        }

        public void Register(StateMachineAsset asset, Action<string> callback)
        {
            RequireInit(asset); 

            if (Values.TryGetValue(asset.UniqueID, out var runtime))
            {
                runtime.EventChange += callback;
            }
        }
        
        public void Unregister(StateMachineAsset asset, Action<string> callback)
        {
            RequireInit(asset);

            if (Values.TryGetValue(asset.UniqueID, out var runtime))
            {
                runtime.EventChange -= callback;
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RequireInit(StateMachineAsset asset)
        {
            if (Values.ContainsKey(asset.UniqueID)) return;
            
            var runtime = new NameVariableRuntime(asset.NameList);
            runtime.OnStartup();

            Values[asset.UniqueID] = runtime;
        }
        
        private void RequireInitNode(StateMachineAsset asset)
        {
            if (NodeValues.ContainsKey(asset.UniqueID)) return;
            
            NodeValues[asset.UniqueID] = asset.nodes;
        }

        // IGAMESAVE: -----------------------------------------------------------------------------

        public string SaveID => "state-machine-name-variables";

        public LoadMode LoadMode => LoadMode.Greedy;
        public bool IsShared => false;

        public Type SaveType => typeof(SaveGroupNameVariables);

        public object SaveData
        {
            get
            {
                Dictionary<string, NameVariableRuntime> saveValues =
                    new Dictionary<string, NameVariableRuntime>();
                        
                foreach (KeyValuePair<IdString, NameVariableRuntime> entry in Values)
                {
                    if (!SaveValues.Contains(entry.Key)) continue;
                    saveValues.Add(entry.Key.String, entry.Value);
                }

                SaveGroupNameVariables saveData = new SaveGroupNameVariables(saveValues);
                return saveData;
            }
        }

        public Task OnLoad(object value)
        {
            if (value is not SaveGroupNameVariables saveData) return Task.FromResult(false);
        
            int numGroups = saveData.Count();
            for (int i = 0; i < numGroups; ++i)
            {
                IdString uniqueID = new IdString(saveData.GetID(i));
                List<NameVariable> candidates = saveData.GetData(i).Variables;

                if (!Values.TryGetValue(uniqueID, out NameVariableRuntime variables))
                {
                    continue;
                }
                
                foreach (NameVariable candidate in candidates)
                {
                    if (!variables.Exists(candidate.Name)) continue;
                    variables.Set(candidate.Name, candidate.Value);
                }
            }
            
            return Task.FromResult(true);
        }
    }
}