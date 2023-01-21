using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Ray Obstruction Position")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]
    [Description("Returns the hit position that is obstructing a RaySensor")]

    [Category("SensorToolkit/Ray Obstruction Position")]

    [Serializable]
    public class GetPositionRayObstructionPoint : PropertyTypeGetPosition {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        public override Vector3 Get(Args args) {
            var sensor = m_Sensor.Get<BasePulsableSensor>(args);
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) {
                return Vector3.zero;
            }
            return raySensor.GetObstructionRayHit().Point;
        }

        public GetPositionRayObstructionPoint() : base() { }

        public static PropertyGetPosition Create => new PropertyGetPosition(new GetPositionRayObstructionPoint());

        public override string String => $"{m_Sensor} obstruction point";
    }
}