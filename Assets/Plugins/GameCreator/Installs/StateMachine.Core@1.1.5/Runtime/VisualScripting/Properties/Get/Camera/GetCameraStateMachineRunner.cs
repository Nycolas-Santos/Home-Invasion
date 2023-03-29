using System;
using GameCreator.Runtime.Cameras;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using NinjutsuGames.StateMachine.Runtime;
using NinjutsuGames.StateMachine.Runtime.Common;
using NinjutsuGames.StateMachine.Runtime.Variables;

namespace NinjutsuGames.StateMachine.Runtime
{
    [Title("State Machine Runner Variable")]
    [Category("Variables/State Machine Runner Variable")]
    
    [Image(typeof(IconStateMachine), ColorTheme.Type.Yellow, typeof(OverlayBolt))]
    [Description("Returns the Game Creator Camera value of a State Machine Runner Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetCameraStateMachineRunner : PropertyTypeGetCamera
    {
        [SerializeField]
        protected FieldGetStateMachineRunner m_Variable = new(ValueGameObject.TYPE_ID);

        public override TCamera Get(Args args)
        {
            GameObject camera = m_Variable.Get<GameObject>(args);
            return camera != null ? camera.Get<TCamera>() : null;
        }

        public override string String => m_Variable.ToString();
    }
}