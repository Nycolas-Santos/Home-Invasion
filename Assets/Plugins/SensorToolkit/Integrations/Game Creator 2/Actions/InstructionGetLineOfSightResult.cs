using System;
using System.Linq;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Micosmo.SensorToolkit.GameCreator {

    [Title("Get Line Of Sight Result")]
    [Description("Get detailed results of a line of sight test for the specified target")]

    [Image(typeof(IconEye), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]

    [Category("SensorToolkit/Get Line Of Sight Result")]

    [Parameter("Sensor", "The game object with the LOSSensor")]
    [Parameter("Target", "The target to get the test results for")]
    [Parameter("Store Is Visible", "Stores whether the target is visible or not")]
    [Parameter("Store Visibility", "Stores the fraction visibility of the target")]
    [Parameter("Store Visible Transfroms", "Stores any visible transforms from a LOSTargets component on the target")]

    [Serializable]
    public class InstructionGetLineOfSightResult : Instruction {

        [SerializeField]
        PropertyGetGameObject m_LineOfSightSensor = new PropertyGetGameObject();

        [SerializeField]
        PropertyGetGameObject m_Target = new PropertyGetGameObject();

        [SerializeField]
        PropertySetBool m_StoreIsVisible = new PropertySetBool();

        [SerializeField]
        PropertySetNumber m_StoreVisibility = new PropertySetNumber();

        [SerializeField]
        CollectorListVariable m_StoreVisibleTransforms = new CollectorListVariable();

        public override string Title => $"Get LOS result for {m_Target}";

        protected override Task Run(Args args) {
            var target = m_Target.Get(args);
            if (target == null) {
                return DefaultResult;
            }
            var sensor = m_LineOfSightSensor.Get<Sensor>(args);
            if (sensor is LOSSensor) {
                Run3D(sensor as LOSSensor, target, args);
            } else if (sensor is LOSSensor2D) {
                Run2D(sensor as LOSSensor2D, target, args);
            }
            return DefaultResult;
        }

        void Run3D(LOSSensor sensor, GameObject target, Args args) {
            var result = sensor.GetResult(target);
            if (result == null) {
                return;
            }
            m_StoreIsVisible.Set(result.IsVisible, args);
            m_StoreVisibility.Set(result.Visibility, args);

            var visibleTransforms = result.Rays
                .Where(r => r.TargetTransform != null && r.Visibility > 0)
                .Select(r => r.TargetTransform.gameObject).ToArray();

            m_StoreVisibleTransforms.Fill(visibleTransforms);
        }

        void Run2D(LOSSensor2D sensor, GameObject target, Args args) {
            var result = sensor.GetResult(target);
            if (result == null) {
                return;
            }
            m_StoreIsVisible.Set(result.IsVisible, args);
            m_StoreVisibility.Set(result.Visibility, args);

            var visibleTransforms = result.Rays
                .Where(r => r.TargetTransform != null && r.Visibility > 0)
                .Select(r => r.TargetTransform.gameObject).ToArray();

            m_StoreVisibleTransforms.Fill(visibleTransforms);
        }

    }

}