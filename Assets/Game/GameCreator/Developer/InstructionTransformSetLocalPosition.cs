using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

[Version(1, 0, 1)]

[Title("Set Local Position")]
[Description("Simple instruction to Set the Local Position of a Transform, does not have any tweening or animation, but can be more accurate")]
[Category("Transforms/Set Local Position")]
[Keywords("Transform", "Local", "Position", "Set")]

[Parameter("Source", "Target gameObject")]
[Parameter("Vector3", "New vector3 local position")]

[Image(typeof(IconVector3), ColorTheme.Type.Yellow)]

public class InstructionTransformSetLocalPosition : Instruction
{
    
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_Source = new PropertyGetGameObject();
    [SerializeField] private Vector3 m_Vector3 = new Vector3(0,0,0);
    
    // PROPERTIES: ----------------------------------------------------------------------------
    public override string Title => $"Set {this.m_Source}'s Position to: {this.m_Vector3}";
    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        var source = this.m_Source.Get(args);
        var vector3 = this.m_Vector3;

        if (source == null) return DefaultResult;

        source.transform.localPosition = vector3;
        return DefaultResult;
    }
}
