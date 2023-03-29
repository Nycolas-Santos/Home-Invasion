using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using NinjutsuGames.StateMachine.Runtime.Common;
using NinjutsuGames.StateMachine.Runtime.Variables;

namespace NinjutsuGames.StateMachine.Runtime
{
    [Title("Game Object State Machine Variable")]
    [Category("Variables/Game Object State Machine Variable")]
    
    [Image(typeof(IconStateMachine), ColorTheme.Type.Blue)]
    [Description("Returns the Game Object value of a State Machine Variable")]

    [Serializable]
    public class GetLocationObjectStateMachine : PropertyTypeGetLocation
    {
        [SerializeField]
        protected FieldGetStateMachine m_Variable = new(ValueGameObject.TYPE_ID);

        [SerializeField] private bool m_Rotate = true;
        
        public override Location Get(Args args)
        {
            GameObject value = m_Variable.Get<GameObject>(args);
            return new Location(
                value ? value.transform : null,
                Space.Self, Vector3.zero,
                m_Rotate,
                Quaternion.identity
            );
        }

        public override string String => m_Variable.ToString();
    }
}