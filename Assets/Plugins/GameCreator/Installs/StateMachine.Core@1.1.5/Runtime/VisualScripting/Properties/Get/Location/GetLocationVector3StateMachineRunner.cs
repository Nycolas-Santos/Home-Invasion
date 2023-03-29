using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using NinjutsuGames.StateMachine.Runtime.Common;
using NinjutsuGames.StateMachine.Runtime.Variables;

namespace NinjutsuGames.StateMachine.Runtime
{
    [Title("Vector3 State Machine Runner Variable")]
    [Category("Variables/Vector3 State Machine Runner Variable")]
    
    [Image(typeof(IconStateMachine), ColorTheme.Type.Yellow, typeof(OverlayBolt))]
    [Description("Returns the Vector3 as the position value of a State Machine Runner Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetLocationVector3StateMachineRunner : PropertyTypeGetLocation
    {
        [SerializeField]
        protected FieldGetStateMachineRunner m_Variable = new(ValueVector3.TYPE_ID);

        public override Location Get(Args args) => new(m_Variable.Get<Vector3>(args));

        public override string String => m_Variable.ToString();
    }
}