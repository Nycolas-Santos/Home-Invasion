using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Is Obstructed")]
    [Description("Returns true if the RaySensor is obstructed")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]

    [Category("SensorToolkit/Is Obstructed")]

    [Serializable]
    public class ConditionIsObstructed : Condition {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        protected override string Summary => $"Is {m_Sensor} obstructed";

        protected override bool Run(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) {
                return false;
            }
            return raySensor.IsObstructed;
        }

    }

}