using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Ray Detection Normal")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]
    [Description("Returns the hit normal direction for a game object detected by a RaySensor")]

    [Category("SensorToolkit/Ray Detection Normal")]

    [Serializable]
    public class GetDirectionRayDetectionNormal : PropertyTypeGetDirection {

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
            return raySensor.GetDetectionRayHit(target).Normal;
        }

        public GetDirectionRayDetectionNormal() : base() { }

        public static PropertyGetDirection Create => new PropertyGetDirection(new GetDirectionRayDetectionNormal());

        public override string String => $"{m_Sensor} hit point for {m_Target}";
    }
}