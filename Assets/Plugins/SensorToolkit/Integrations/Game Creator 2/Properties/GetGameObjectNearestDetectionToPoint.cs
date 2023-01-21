using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Get Nearest Detection To Point")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal)]
    [Description("Returns the nearest detected game object to the specified point")]

    [Category("SensorToolkit/Get Nearest Detection To Point")]

    [Serializable]
    public class GetGameObjectNearestDetectionToPoint : PropertyTypeGetGameObject {

        [SerializeField]
        protected PropertyGetGameObject m_Sensor = new PropertyGetGameObject();

        [SerializeField]
        protected PropertyGetPosition m_Point = new PropertyGetPosition();

        public override GameObject Get(Args args) {
            var sensor = m_Sensor.Get<Sensor>(args);
            return sensor != null ? sensor.GetNearestDetectionToPoint(m_Point.Get(args)) : null;
        }

        public GetGameObjectNearestDetectionToPoint() : base() { }

        public static PropertyGetGameObject Create => new PropertyGetGameObject(new GetGameObjectNearestDetectionToPoint());

        public override string String => $"{m_Sensor} Nearest Detection to {m_Point}";
    }

}