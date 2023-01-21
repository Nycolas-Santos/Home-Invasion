using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("On Clear")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]

    [Category("SensorToolkit/On Clear")]
    [Description("Executed when the RaySensor is clear and was previously obstructed")]

    [Serializable]
    public class EventOnClear : TEventSensor {
        protected override void WhenDisabled(Trigger trigger, Sensor sensor) {
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) return;

            raySensor.OnClear.RemoveListener(OnClear);
        }

        protected override void WhenEnabled(Trigger trigger, Sensor sensor) {
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) return;

            raySensor.OnClear.AddListener(OnClear);
        }

        void OnClear(IRayCastingSensor sensor) {
            _ = m_Trigger.Execute();
        }
    }

}