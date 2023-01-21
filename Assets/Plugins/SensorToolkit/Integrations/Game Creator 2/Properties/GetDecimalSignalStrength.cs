using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Signal Strength")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]
    [Description("Returns the signal strength for a detected game object on a Sensor")]

    [Category("SensorToolkit/Signal Strength")]

    [Serializable]
    public class GetDecimalSignalStrength : PropertyTypeGetDecimal {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        protected PropertyGetGameObject m_Target = new PropertyGetGameObject();

        public override double Get(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            var target = m_Target.Get(args);
            if (sensor == null || target == null) {
                return 0f;
            }
            Signal signal;
            if (sensor.TryGetSignal(target, out signal)) {
                return signal.Strength;
            }
            return 0f;
        }

        public GetDecimalSignalStrength() : base() { }

        public static PropertyGetDecimal Create => new PropertyGetDecimal(new GetDecimalSignalStrength());

        public override string String => $"{m_Target} Signal Strength from {m_Sensor}";
    }

}