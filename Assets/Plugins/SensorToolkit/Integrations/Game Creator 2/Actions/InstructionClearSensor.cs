using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Clear Sensor")]
    [Description("Clears the Sensor of all its detections")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayCross))]

    [Category("SensorToolkit/Clear Sensor")]

    [Parameter("Sensor", "The game object with the Sensor")]

    [Serializable]
    public class InstructionClearSensor : Instruction {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        public override string Title => $"Clear {m_Sensor}";

        protected override Task Run(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            if (sensor == null) return DefaultResult;

            sensor.Clear();
            return DefaultResult;
        }
    }

}