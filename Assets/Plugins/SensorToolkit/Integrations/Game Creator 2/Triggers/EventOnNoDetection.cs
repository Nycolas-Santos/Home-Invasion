using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("On No Detection")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayCross))]

    [Category("SensorToolkit/On No Detection")]
    [Description("Executes when the Sensor no longer detects any game objects")]

    [Serializable]
    public class EventOnNoDetection : TEventSensor {
        protected override void WhenDisabled(Trigger trigger, Sensor sensor) {
            sensor.OnNoDetection.RemoveListener(OnNoDetection);
        }

        protected override void WhenEnabled(Trigger trigger, Sensor sensor) {
            sensor.OnNoDetection.AddListener(OnNoDetection);
        }

        void OnNoDetection() {
            _ = m_Trigger.Execute();
        }
    }

}