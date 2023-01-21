using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Seek GameObject")]
    [Description("Set the game object that the SteeringSensor should seek")]

    [Image(typeof(IconCharacterWalk), ColorTheme.Type.Red)]

    [Category("SensorToolkit/Seek GameObject")]

    [Parameter("Steering Sensor", "The game object with the SteeringSensor")]
    [Parameter("Target", "The game object to seek towards")]
    [Parameter("Target Distance", "The distance from the game object to achieve")]
    [Parameter("Wait Until Reached", "The instruction will wait until we reach the destination")]

    [Serializable]
    public class InstructionSeekGameObject : Instruction {

        [SerializeField]
        PropertyGetGameObject m_SteeringSensor = new PropertyGetGameObject();

        [SerializeField]
        PropertyGetGameObject m_Target = new PropertyGetGameObject();

        [SerializeField]
        PropertyGetDecimal m_TargetDistance = new PropertyGetDecimal(1f);

        [SerializeField]
        bool m_WaitUntilReached;

        public override string Title => $"Seek within {m_TargetDistance} of {m_Target}" + (m_WaitUntilReached ? " and wait until reached" : "");

        protected override async Task Run(Args args) {
            var sensor = m_SteeringSensor.Get<BasePulsableSensor>(args);
            var steering = sensor as ISteeringSensor;
            if (steering == null) {
                return;
            }
            var targetGo = m_Target.Get(args);
            var targetTransform = targetGo != null ? targetGo.transform : null;
            steering.Seek.DestinationTransform = targetTransform;
            steering.Seek.TargetDistance = (float)m_TargetDistance.Get(args);
            if (m_WaitUntilReached) {
                await While(() => !steering.IsDestinationReached);
            }
        }

    }

}