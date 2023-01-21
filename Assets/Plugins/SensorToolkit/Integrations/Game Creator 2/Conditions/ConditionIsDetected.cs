using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Is Detected")]
    [Description("Returns true if the game object is detected by the Sensor")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]

    [Category("SensorToolkit/Is Detected")]

    [Serializable]
    public class ConditionIsDetected : Condition {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        PropertyGetGameObject m_Target = new PropertyGetGameObject();

        protected override string Summary => $"Is {m_Target} detected by {m_Sensor}";

        protected override bool Run(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            if (sensor == null) return false;

            var target = m_Target.Get(args);
            if (target == null) return false;

            return sensor.IsDetected(target);
        }

    }

}