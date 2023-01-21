using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Set Pulse Interval")]
    [Description("Sets the interval at which the Sensor will pulse. The Sensor should be set to 'Fixed Interval' mode for this to work")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayDot))]

    [Category("SensorToolkit/Set Pulse Interval")]

    [Parameter("Sensor", "The game object with the Sensor")]
    [Parameter("Interval", "The interval in seconds that should be pulsed")]

    [Serializable]
    public class InstructionSetPulseInterval : Instruction {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        PropertyGetDecimal m_Interval = new PropertyGetDecimal();

        public override string Title => $"Set {m_Sensor}'s pulse interval to {m_Interval}";

        protected override Task Run(Args args) {
            var sensor = m_Sensor.Get<BasePulsableSensor>(args);
            if (sensor == null) return DefaultResult;
            var pulseRoutine = sensor as IPulseRoutine;
            if (pulseRoutine == null) return DefaultResult;

            pulseRoutine.PulseInterval = (float)m_Interval.Get(args);
            return DefaultResult;
        }

    }

}