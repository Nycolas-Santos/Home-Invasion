using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Is Seeking")]

    [Image(typeof(IconCharacterWalk), ColorTheme.Type.Red)]
    [Description("Returns true if the SteeringSensor is currently seeking towards a target")]

    [Category("SensorToolkit/Is Seeking")]

    [Serializable]
    public class GetBoolIsSeeking : PropertyTypeGetBool {

        [SerializeField]
        protected PropertyGetGameObject m_SteeringSensor = new PropertyGetGameObject();

        public override bool Get(Args args) {
            var sensor = m_SteeringSensor.Get<BasePulsableSensor>(args);
            var steering = sensor as ISteeringSensor;
            if (steering == null) {
                return false;
            }
            return !steering.IsDestinationReached;
        }

        public GetBoolIsSeeking() : base() { }

        public static PropertyGetBool Create => new PropertyGetBool(new GetBoolIsSeeking());

        public override string String => $"Is {m_SteeringSensor} currently seeking";
    }

}