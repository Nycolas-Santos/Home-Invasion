using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Is Detected")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]
    [Description("Returns true if the target is detected by the sensor")]

    [Category("SensorToolkit/Is Detected")]

    [Serializable]
    public class GetBoolIsDetected : PropertyTypeGetBool {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        protected PropertyGetGameObject m_Target = new PropertyGetGameObject();

        public override bool Get(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            var target = m_Target.Get(args);
            if (sensor == null || target == null) {
                return false;
            }
            return sensor.IsDetected(target);
        }

        public GetBoolIsDetected() : base() { }

        public static PropertyGetBool Create => new PropertyGetBool(new GetBoolIsDetected());

        public override string String => $"Is {m_Target} detected by {m_Sensor}";
    }
}