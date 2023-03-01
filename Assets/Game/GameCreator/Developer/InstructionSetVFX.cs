using System;
using System.Threading.Tasks;
using Game.Scripts;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;


[Version(1, 0, 1)]

[Title("Set VHS Effect")]
[Category("Custom/Set VHS Effect")]
[Keywords("VHS", "Set", "VFX")]

[Image(typeof(IconCamera), ColorTheme.Type.Purple)]

[Serializable]
public class InstructionSetVFX : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [FormerlySerializedAs("vfxMode")] [SerializeField] public GameSettings.VFXMode m_VfxMode;
    // PROPERTIES: ----------------------------------------------------------------------------
        
    public override string Title => $"Set VFX to: {this.m_VfxMode}";
    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        var camera = Camera.main;
        if (camera == null) return DefaultResult;
        
        var vhs = camera.GetComponent<postVHSPro>();
        if (vhs == null) return DefaultResult;

        switch (this.m_VfxMode)
        {
            case GameSettings.VFXMode.VHS:
                vhs.enabled = true;
                break;
            case GameSettings.VFXMode.NoVFX:
                vhs.enabled = false;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        GameSettings.Instance.vfx = this.m_VfxMode;
        return DefaultResult;
    }
}
