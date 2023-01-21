using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Ray Detection Position")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]
    [Description("Returns the hit position for a game object detected by a RaySensor")]

    [Category("SensorToolkit/Ray Detection Position")]

    [Serializable]
    public class GetPositionRayDetectionPoint : PropertyTypeGetPosition {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        protected PropertyGetGameObject m_Target = new PropertyGetGameObject();

        public override Vector3 Get(Args args) {
            var sensor = m_Sensor.Get<BasePulsableSensor>(args);
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) {
                return Vector3.zero;
            }
            var target = m_Target.Get(args);
            if (target == null) {
                return Vector3.zero;
            }
            return raySensor.GetDetectionRayHit(target).Point;
        }

        public GetPositionRayDetectionPoint() : base() { }

        public static PropertyGetPosition Create => new PropertyGetPosition(new GetPositionRayDetectionPoint());

        public override string String => $"{m_Sensor} hit point for {m_Target}";
    }
}