using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using Event = GameCreator.Runtime.VisualScripting.Event;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Serializable]
    public abstract class TEventSensor : Event {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        protected override void OnEnable(Trigger trigger) {
            base.OnEnable(trigger);

            var sensor = m_Sensor.Get<Sensor>(trigger.gameObject);
            if (sensor != null) WhenEnabled(trigger, sensor);
        }

        protected override void OnStart(Trigger trigger) {
            base.OnStart(trigger);

            var sensor = m_Sensor.Get<Sensor>(trigger.gameObject);
            if (sensor == null) return;

            WhenDisabled(trigger, sensor);
            WhenEnabled(trigger, sensor);
        }

        protected override void OnDisable(Trigger trigger) {
            base.OnDisable(trigger);

            var sensor = m_Sensor.Get<Sensor>(trigger.gameObject);
            if (sensor != null) WhenDisabled(trigger, sensor);
        }

        protected abstract void WhenEnabled(Trigger trigger, Sensor sensor);
        protected abstract void WhenDisabled(Trigger trigger, Sensor sensor);
    }

}