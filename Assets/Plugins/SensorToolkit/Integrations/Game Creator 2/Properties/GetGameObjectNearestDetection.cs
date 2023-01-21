using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Get Nearest Detection")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]
    [Description("Returns the nearest detected game object to the Sensor")]

    [Category("SensorToolkit/Get Nearest Detection")]

    [Serializable]
    public class GetGameObjectNearestDetection : PropertyTypeGetGameObject {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        public override GameObject Get(Args args) => GetValue(args);

        GameObject GetValue(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            return sensor != null ? sensor.GetNearestDetection() : null;
        }

        public GetGameObjectNearestDetection() : base() { }

        public static PropertyGetGameObject Create => new PropertyGetGameObject(
            new GetGameObjectNearestDetection()
        );

        public override string String => $"{m_Sensor} Nearest Detection";
    }

}