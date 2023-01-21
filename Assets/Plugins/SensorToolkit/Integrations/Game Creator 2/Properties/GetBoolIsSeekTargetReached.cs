using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Is Seeking")]

    [Image(typeof(IconCharacterWalk), ColorTheme.Type.Red)]
    [Description("Returns true if the SteeringSensor has reached its destination")]

    [Category("SensorToolkit/Is Seek Target Reached")]

    [Serializable]
    public class GetBoolIsSeekTargetReached : PropertyTypeGetBool {

        [SerializeField]
        protected PropertyGetGameObject m_SteeringSensor = new PropertyGetGameObject();

        public override bool Get(Args args) {
            var sensor = m_SteeringSensor.Get<BasePulsableSensor>(args);
            var steering = sensor as ISteeringSensor;
            if (steering == null) {
                return false;
            }
            return steering.IsDestinationReached;
        }

        public GetBoolIsSeekTargetReached() : base() { }

        public static PropertyGetBool Create => new PropertyGetBool(new GetBoolIsSeeking());

        public override string String => $"Has {m_SteeringSensor} reached target";
    }

}