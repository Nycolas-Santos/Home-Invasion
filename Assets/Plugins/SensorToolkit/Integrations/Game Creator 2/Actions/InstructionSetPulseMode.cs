using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Set Pulse Mode")]
    [Description("Sets the mode by which the sensor will pulse")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayDot))]

    [Category("SensorToolkit/Set Pulse Mode")]

    [Parameter("Sensor", "The game object with the Sensor")]
    [Parameter("Pulse Mode", "The pulse mode to set")]

    [Serializable]
    public class InstructionSetPulseMode : Instruction {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        PulseRoutine.Modes m_pulseMode;

        public override string Title => $"Set {m_Sensor}'s pulse mode to {Enum.GetName(typeof(PulseRoutine.Modes), m_pulseMode)}";

        protected override Task Run(Args args) {
            var sensor = m_Sensor.Get<BasePulsableSensor>(args);
            if (sensor == null) return DefaultResult;
            var pulseRoutine = sensor as IPulseRoutine;
            if (pulseRoutine == null) return DefaultResult;

            pulseRoutine.PulseMode = m_pulseMode;
            return DefaultResult;
        }

    }

}