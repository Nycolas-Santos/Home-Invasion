using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Is Ray Obstructed")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]
    [Description("Returns true if the RaySensor is currently obstructed")]

    [Category("SensorToolkit/Is Ray Obstructed")]

    [Serializable]
    public class GetBoolIsRayObstructed : PropertyTypeGetBool {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        public override bool Get(Args args) {
            var sensor = m_Sensor.Get<BasePulsableSensor>(args);
            var raySensor = sensor as IRayCastingSensor;
            if (raySensor == null) {
                return false;
            }
            return raySensor.IsObstructed;
        }

        public GetBoolIsRayObstructed() : base() { }

        public static PropertyGetBool Create => new PropertyGetBool(new GetBoolIsRayObstructed());

        public override string String => $"Is {m_Sensor} obstructed";
    }
}