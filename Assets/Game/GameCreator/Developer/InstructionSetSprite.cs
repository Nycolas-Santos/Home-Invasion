using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 1)]

[Title("Set Sprite")]
[Description("Set a Sprite field")]
[Category("Properties/Set Sprite")]
[Keywords("Sprite", "Set", "Property", "Set Sprite")]

[Parameter("Target", "Target sprite that will be changed")]
[Parameter("Sprite", "New sprite to set Target as")]

[Image(typeof(IconSprite), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionSetSprite : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetSprite m_Target = new PropertyGetSprite();

    [SerializeField] private PropertyGetSprite m_Sprite = new PropertyGetSprite();
    // PROPERTIES: ----------------------------------------------------------------------------
    public override string Title => $"Set {this.m_Target} to {this.m_Sprite}";
    // RUN METHOD: ----------------------------------------------------------------------------

    protected override Task Run(Args args)
    {
        Sprite target = this.m_Target.Get(args);
        Sprite sprite = this.m_Sprite.Get(args);

        if (target == null || sprite == null) return DefaultResult;

        target = sprite;
        return DefaultResult;
    }
}
