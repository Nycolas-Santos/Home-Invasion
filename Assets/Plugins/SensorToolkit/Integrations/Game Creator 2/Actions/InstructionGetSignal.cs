using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Get Signal")]
    [Description("Get the Signal data for the detected game object")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayDot))]

    [Category("SensorToolkit/Get Signal")]

    [Parameter("Sensor", "The game object with the Sensor")]
    [Parameter("Target", "The game object to get the signal data for")]
    [Parameter("Store Strength", "Will store the Signal Strength here")]
    [Parameter("Store Bounds Center", "Will store the center-point of the Signals bounding box")]
    [Parameter("Store Bounds Size", "Will store the size of the Signals bounding box")]

    [Serializable]
    public class InstructionGetSignal : Instruction {

        [SerializeField]
        PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        PropertyGetGameObject m_Target = new PropertyGetGameObject();

        [SerializeField]
        PropertySetNumber m_StoreStrength = SetNumberLocalName.Create;

        [SerializeField]
        PropertySetVector3 m_StoreBoundsCenter = SetVector3LocalName.Create;

        [SerializeField]
        PropertySetVector3 m_StoreBoundsSize = SetVector3LocalName.Create;

        public override string Title => $"Get Signal for {m_Target} from {m_Sensor}";

        protected override Task Run(Args args) {
            var gameObject = m_Sensor.Get(args);
            if (gameObject == null) return DefaultResult;

            var sensor = gameObject.Get<Sensor>();
            if (sensor == null) return DefaultResult;

            var targetGameObject = m_Target.Get(args);
            if (targetGameObject == null) return DefaultResult;

            Signal signal;
            if (sensor.TryGetSignal(targetGameObject, out signal)) {
                m_StoreStrength.Set(signal.Strength, args);
                m_StoreBoundsCenter.Set(signal.Bounds.center, args);
                m_StoreBoundsSize.Set(signal.Shape.extents, args);
            }

            return DefaultResult;
        }
    }

}