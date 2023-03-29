using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using NinjutsuGames.StateMachine.Runtime;
using NinjutsuGames.StateMachine.Runtime.Common;

namespace NinjutsuGames.StateMachine.Runtime.Variables
{
    [Title("State Machine Node")]
    [Category("State Machine/State Machine Node")]
    [Image(typeof(IconStateMachine), ColorTheme.Type.Blue)]
    [Description("Returns a node of a State Machine Variable")]
    [Serializable]
    [HideLabelsInEditor]
    public class GetNodeStateMachine : PropertyTypeGetString
    {
        [SerializeField] protected FieldGetNodeStateMachine m_Variable = new();

        public override string Get(Args args) 
        {
            return m_Variable.GUID;//.Get<string>();
        }

        public override string Get(GameObject gameObject)
        {
            return m_Variable.GUID;//.Get<string>();
        }
        
        public StateMachineAsset GetStateMachine() => m_Variable.GetStateMachine();

        public override string String => m_Variable.ToString();
        
        public static PropertyGetString Create => new(
            new GetNodeStateMachine()
        );
    }
}