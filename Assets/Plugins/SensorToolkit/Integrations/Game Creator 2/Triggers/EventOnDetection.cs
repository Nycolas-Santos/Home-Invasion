using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("On Detection")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayPlus))]

    [Category("SensorToolkit/On Detection")]
    [Description("Executes when the Sensor detects a new game object")]

    [Serializable]
    public class EventOnDetection : TEventSensor {

        [SerializeField]
        PropertySetGameObject m_StoreDetection = new PropertySetGameObject();

        protected override void WhenDisabled(Trigger trigger, Sensor sensor) {
            sensor.OnSignalAdded -= OnDetection;
        }

        protected override void WhenEnabled(Trigger trigger, Sensor sensor) {
            sensor.OnSignalAdded += OnDetection;
        }

        void OnDetection(Signal signal, Sensor sensor) {
            m_StoreDetection.Set(signal.Object, m_Trigger);
            _ = m_Trigger.Execute(signal.Object);
        }
    }

}