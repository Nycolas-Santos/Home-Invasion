using System;
using System.Threading.Tasks;
using Game.Scripts;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
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
    [SerializeField] public GameSettings.VFXMode vfxMode;
    // PROPERTIES: ----------------------------------------------------------------------------
        
    public override string Title => $"Set VFX to: {vfxMode}";
    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        var camera = Camera.main;
        if (camera == null) return DefaultResult;
        
        var vhs = camera.GetComponent<postVHSPro>();
        if (vhs == null) return DefaultResult;

        switch (this.vfxMode)
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

        var settings = Object.FindObjectOfType<GameSettings>();
        if (settings == null) return DefaultResult;
        
        settings.vfx = this.vfxMode;
        
        return DefaultResult;
    }
}
