using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Get Nearest Detection To Point")]
    [Description("Gets the nearest detected game object to the specified point")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]

    [Category("SensorToolkit/Get Nearest Detection To Point")]

    [Parameter("Sensor", "The game object with the Sensor")]
    [Parameter("Point", "The target point to compare distance with")]
    [Parameter("Store In", "Stores the nearest detected game object here")]
    [Parameter("Wait For Detection", "If there are no detections then wait until there is one")]

    [Serializable]
    public class InstructionGetNearestDetectionToPoint : Instruction {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        protected PropertyGetPosition m_Point = new PropertyGetPosition();

        [SerializeField]
        PropertySetGameObject m_StoreIn = new PropertySetGameObject();

        [SerializeField]
        bool m_WaitForDetection;

        public override string Title => m_WaitForDetection
            ? $"Wait for nearest detection to {m_Point} from {m_Sensor}"
            : $"Get nearest detection to {m_Point} from {m_Sensor}";

        protected override async Task Run(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            if (sensor == null) return;

            if (m_WaitForDetection) {
                await While(delegate {
                    var detection = sensor.GetNearestDetectionToPoint(m_Point.Get(args));
                    if (detection == null) {
                        return true;
                    }
                    m_StoreIn.Set(detection, args);
                    return false;
                });
            } else {
                var detection = sensor.GetNearestDetectionToPoint(m_Point.Get(args));
                m_StoreIn.Set(detection, args);
            }
        }

    }

}