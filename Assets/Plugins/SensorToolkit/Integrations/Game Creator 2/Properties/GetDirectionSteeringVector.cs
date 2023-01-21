using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Steering Vector")]
    [Description("Returns the Steering Vector from the SteeringSensor. This points towards the seek target while avoiding nearby obstacles.")]

    [Category("SensorToolkit/Steering Vector")]

    [Image(typeof(IconCharacterWalk), ColorTheme.Type.Red)]

    [Serializable]
    public class GetDirectionSteeringVector : PropertyTypeGetDirection {

        [SerializeField]
        protected PropertyGetGameObject m_SteeringSensor = new PropertyGetGameObject();

        [SerializeField]
        bool m_Normalized;

        public override Vector3 Get(Args args) {
            var sensor = m_SteeringSensor.Get<BasePulsableSensor>(args);
            var steering = sensor as ISteeringSensor;
            if (steering == null) {
                return Vector3.zero;
            }
            var vSteer = steering.GetSteeringVector();
            return m_Normalized ? vSteer.normalized : vSteer;
        }

        public GetDirectionSteeringVector() : base() { }

        public static PropertyGetDirection Create => new PropertyGetDirection(new GetDirectionSteeringVector());

        public override string String => $"{m_SteeringSensor}'s steering vector";
    }

}