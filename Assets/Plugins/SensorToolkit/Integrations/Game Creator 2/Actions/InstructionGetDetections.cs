using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Get Detections")]
    [Description("Get all detected game objects of a sensor and store in a list")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayListVariable))]

    [Category("SensorToolkit/Get Detections")]

    [Parameter("Sensor", "The game object with the Sensor")]
    [Parameter("Store In", "A list to store the detections in")]

    [Serializable]
    public class InstructionGetDetections : Instruction {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        private CollectorListVariable m_StoreIn = new CollectorListVariable();

        public override string Title => $"Get detections from {m_Sensor}";

        protected override Task Run(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            if (sensor == null) return DefaultResult;

            m_StoreIn.Fill(sensor.GetDetections().ToArray());
            return DefaultResult;
        }
    }

}