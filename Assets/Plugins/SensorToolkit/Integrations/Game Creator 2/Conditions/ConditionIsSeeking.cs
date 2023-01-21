using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Is Seeking")]
    [Description("Returns true if the SteeringSensor is currently seeking")]

    [Image(typeof(IconCharacterWalk), ColorTheme.Type.Red)]

    [Category("SensorToolkit/Is Steering")]

    [Serializable]
    public class ConditionIsSeeking : Condition {

        [SerializeField]
        PropertyGetGameObject m_SteeringSensor = new PropertyGetGameObject();

        protected override string Summary => $"Is {m_SteeringSensor} seeking";

        protected override bool Run(Args args) {
            var sensor = m_SteeringSensor.Get<BasePulsableSensor>(args);
            var steering = sensor as ISteeringSensor;
            if (steering == null) {
                return false;
            }
            return !steering.IsDestinationReached;
        }

    }

}