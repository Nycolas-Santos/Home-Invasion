using System;
using GameCreator.Runtime.Common;
using NinjutsuGames.StateMachine.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.StateMachine.Runtime.Variables
{
    [Serializable]
    public class FieldGetNodeStateMachine //: TFieldGetVariable
    {
        [SerializeField] protected StateMachineAsset m_StateMachine;
        [SerializeField] protected string m_Name;
        [SerializeField] protected string m_GUID;
        
        // [SerializeField] protected IdString m_TypeID = ValueNull.TYPE_ID;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public FieldGetNodeStateMachine()
        {
            // this.m_TypeID = typeID;
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public StateMachineAsset GetStateMachine() => m_StateMachine;
        
        public string NodeName => m_Name;
        public string GUID => m_GUID;
        
        public T Get<T>()
        {
            var value = Get();
            return Convert.ChangeType(value, typeof(T)) is T typedValue ? typedValue : default;
        }

        public object Get()
        {
            return m_StateMachine != null ? m_StateMachine.GetNode(m_Name) : null;
        }

        /*public override object Get()
        {
            return this.m_Variable != null ? this.m_Variable.Get(m_Name.String) : null;
        }*/

        public override string ToString()
        {
            return string.Format(
                "{0}{1}",
                m_StateMachine != null ? m_StateMachine.name : "(none)",
                string.IsNullOrEmpty(m_Name) ? string.Empty : $"[{m_Name}]" 
            );
        }
    }
}