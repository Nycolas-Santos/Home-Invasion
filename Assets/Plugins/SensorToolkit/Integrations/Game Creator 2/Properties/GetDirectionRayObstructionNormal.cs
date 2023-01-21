using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Ray Obstruction Normal")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]
    [Description("Returns the hit normal direction for a game object obstructiong the RaySensor")]

    [Category("SensorToolkit/Ray Obstruction Normal")]

    [Serializable]
    public class GetDirectionRayObstructionNormal : PropertyTypeGetDirection {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        public override Vector3 Get(Args args) {
            var sensor = m_Sensor.Get<BasePulsableSensor>(args);
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) {
                return Vector3.zero;
            }
            return raySensor.GetObstructionRayHit().Normal;
        }

        public GetDirectionRayObstructionNormal() : base() { }

        public static PropertyGetDirection Create => new PropertyGetDirection(new GetDirectionRayObstructionNormal());

        public override string String => $"{m_Sensor} obstruction normal";
    }
}