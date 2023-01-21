using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("On Some Detection")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]

    [Category("SensorToolkit/On Some Detection")]
    [Description("Executes when the Sensor detects a game object and previously detected nothing")]

    [Serializable]
    public class EventOnSomeDetection : TEventSensor {
        protected override void WhenDisabled(Trigger trigger, Sensor sensor) {
            sensor.OnSomeDetection.RemoveListener(OnSomeDetection);
        }

        protected override void WhenEnabled(Trigger trigger, Sensor sensor) {
            sensor.OnSomeDetection.AddListener(OnSomeDetection);
        }

        void OnSomeDetection() {
            _ = m_Trigger.Execute();
        }
    }

}