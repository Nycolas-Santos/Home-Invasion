using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("On Obstruction")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]

    [Category("SensorToolkit/On Obstruction")]
    [Description("Executes when the RaySensor is obstructed and was previously clear")]

    [Serializable]
    public class EventOnObstruction : TEventSensor {
        protected override void WhenDisabled(Trigger trigger, Sensor sensor) {
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) return;
            
            raySensor.OnObstruction.RemoveListener(OnObstruction);
        }

        protected override void WhenEnabled(Trigger trigger, Sensor sensor) {
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) return;

            raySensor.OnObstruction.AddListener(OnObstruction);
        }

        void OnObstruction(IRayCastingSensor sensor) {
            _ = m_Trigger.Execute();
        }
    }

}