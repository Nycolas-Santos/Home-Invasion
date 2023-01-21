using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Compare Visibility")]
    [Description("Returns true if the visibility of the target satisfies the comparison")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]

    [Category("SensorToolkit/Compare Visibility")]

    [Serializable]
    public class ConditionCompareVisibility : Condition {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        PropertyGetGameObject m_Target = new PropertyGetGameObject();

        [SerializeField]
        CompareDouble m_CompareTo = new CompareDouble();

        protected override string Summary => $"{m_Target}'s visibility {m_CompareTo}";

        protected override bool Run(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            if (sensor == null) {
                return false;
            }

            var target = m_Target.Get(args);
            if (target == null) {
                return false;
            }

            Signal signal;
            if (sensor.TryGetSignal(target, out signal)) {
                return m_CompareTo.Match(signal.Strength, args);
            }
            return false;
        }

    }

}