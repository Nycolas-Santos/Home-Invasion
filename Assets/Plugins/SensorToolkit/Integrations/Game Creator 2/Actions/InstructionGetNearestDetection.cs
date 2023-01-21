using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Get Nearest Detection")]
    [Description("Gets the nearest detected game object to the Sensor")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]

    [Category("SensorToolkit/Get Nearest Detection")]

    [Parameter("Sensor", "The game object with the sensor")]
    [Parameter("Store In", "Stores the nearest detected game object here")]
    [Parameter("Wait For Detection", "If there are no detections then wait until there is one")]

    [Serializable]
    public class InstructionGetNearestDetection : Instruction {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        PropertySetGameObject m_StoreIn = new PropertySetGameObject();

        [SerializeField]
        bool m_WaitForDetection;

        public override string Title => m_WaitForDetection 
            ? $"Wait for nearest detection from {m_Sensor}" 
            : $"Get nearest detection from {m_Sensor}";

        protected override async Task Run(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            if (sensor == null) return;

            if (m_WaitForDetection) {
                await While(delegate {
                    var detection = sensor.GetNearestDetection();
                    if (detection == null) {
                        return true;
                    }
                    m_StoreIn.Set(detection, args);
                    return false;
                });
            } else {
                var detection = sensor.GetNearestDetection();
                m_StoreIn.Set(detection, args);
            }
        }

    }

}