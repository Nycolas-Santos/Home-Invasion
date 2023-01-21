using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("On Detection Lost")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayMinus))]

    [Category("SensorToolkit/On Detection Lost")]
    [Description("Executes when the Sensor loses detection of a game object")]

    [Serializable]
    public class EventOnDetectionLost : TEventSensor {

        [SerializeField]
        PropertySetGameObject m_StoreLost = new PropertySetGameObject();

        protected override void WhenDisabled(Trigger trigger, Sensor sensor) {
            sensor.OnSignalLost -= OnDetectionLost;
        }

        protected override void WhenEnabled(Trigger trigger, Sensor sensor) {
            sensor.OnSignalLost += OnDetectionLost;
        }

        void OnDetectionLost(Signal signal, Sensor sensor) {
            m_StoreLost.Set(signal.Object, m_Trigger);
            _ = m_Trigger.Execute(signal.Object);
        }
    }

}