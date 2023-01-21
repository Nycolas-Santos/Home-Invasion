using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Ray Obstruction Target")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]
    [Description("Returns the game object that is obstructing a RaySensor")]

    [Category("SensorToolkit/Ray Obstruction Target")]

    [Serializable]
    public class GetGameObjectRayObstruction : PropertyTypeGetGameObject {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        public override GameObject Get(Args args) {
            var sensor = m_Sensor.Get<BasePulsableSensor>(args);
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) {
                return null;
            }
            return raySensor.GetObstructionRayHit().GameObject;
        }

        public GetGameObjectRayObstruction() : base() { }

        public static PropertyGetGameObject Create => new PropertyGetGameObject(new GetGameObjectRayObstruction());

        public override string String => $"GameObject obstructing {m_Sensor}";
    }
}