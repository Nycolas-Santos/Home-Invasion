using System;
using GameCreator.Runtime.Cameras;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using NinjutsuGames.StateMachine.Runtime.Common;
using NinjutsuGames.StateMachine.Runtime.Variables;

namespace NinjutsuGames.StateMachine.Runtime
{
    [Title("State Machine Variable")]
    [Category("Variables/State Machine Variable")]
    
    [Image(typeof(IconStateMachine), ColorTheme.Type.Blue)]
    [Description("Returns the Camera Shot value of a State Machine Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetShotStateMachine : PropertyTypeGetShot
    {
        [SerializeField]
        protected FieldGetStateMachine m_Variable = new(ValueGameObject.TYPE_ID);

        public override ShotCamera Get(Args args)
        {
            GameObject camera = m_Variable.Get<GameObject>(args);
            return camera != null ? camera.Get<ShotCamera>() : null;
        }

        public override string String => m_Variable.ToString();
    }
}