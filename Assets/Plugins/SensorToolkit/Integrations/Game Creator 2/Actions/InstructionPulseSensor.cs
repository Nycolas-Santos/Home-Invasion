using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Pulse Sensor")]
    [Description("Manually pulses the sensor")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayPhysics))]

    [Category("SensorToolkit/Pulse Sensor")]

    [Parameter("Sensor", "The game object with the Sensor")]
    [Parameter("Pulse Inputs", "Also pulse any input Sensors")]

    [Serializable]
    public class InstructionPulseSensor : Instruction {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        bool pulseInputs;

        public override string Title => $"Pulse {m_Sensor}";

        protected override Task Run(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            if (sensor == null) return DefaultResult;

            if (pulseInputs) {
                sensor.PulseAll();
            } else {
                sensor.Pulse();
            }
            return DefaultResult;
        }
    }

}