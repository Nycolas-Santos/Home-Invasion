using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Clear Seek Target")]
    [Description("Clears the Seek Target on the SteeringSensor so that it stops seeking")]

    [Image(typeof(IconCharacterWalk), ColorTheme.Type.Red, typeof(OverlayCross))]

    [Category("SensorToolkit/Clear Seek Target")]

    [Parameter("Steering Sensor", "The game object with the SteeringSensor")]

    [Serializable]
    public class InstructionClearSeek : Instruction {

        [SerializeField]
        PropertyGetGameObject m_SteeringSensor = new PropertyGetGameObject();

        public override string Title => $"Clear Seek Target on {m_SteeringSensor}";

        protected override Task Run(Args args) {
            var sensor = m_SteeringSensor.Get<BasePulsableSensor>(args);
            var steering = sensor as ISteeringSensor;
            if (steering == null) {
                return DefaultResult;
            }
            steering.Seek.ClearDestination();
            return DefaultResult;
        }

    }

}