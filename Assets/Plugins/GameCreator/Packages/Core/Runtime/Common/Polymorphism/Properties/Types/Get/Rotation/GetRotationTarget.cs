using System;
using UnityEngine;

namespace GameCreator.Runtime.Common
{
    [Title("Target")]
    [Category("Target")]
    
    [Image(typeof(IconTarget), ColorTheme.Type.Yellow)]
    [Description("Rotation of the targeted game object in local or world space")]

    [Serializable] [HideLabelsInEditor]
    public class GetRotationTarget : PropertyTypeGetRotation
    {
        [SerializeField] private RotationSpace m_Space = RotationSpace.Global;
        [SerializeField] private Vector3 m_Offset = Vector3.zero;
        
        public override Quaternion Get(Args args)
        {
            Quaternion offset = Quaternion.Euler(this.m_Offset);
            return args.Target != null 
                ? this.m_Space == RotationSpace.Global 
                    ? args.Target.transform.rotation * offset
                    : args.Target.transform.localRotation * offset
                : default;
        }

        public static PropertyGetRotation Create => new PropertyGetRotation(
            new GetRotationTarget()
        );

        public override string String => $"{this.m_Space} Target";
    }
}