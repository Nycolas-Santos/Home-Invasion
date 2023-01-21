using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 1)]
[Title("Fullscreen")]
[Description("Sets the screen mode")]

[Category("Application/Fullscreen")]
    
[Parameter("Mode", "The screen mode to change")]

[Keywords("Minimize", "Mode")]
[Image(typeof(IconScale), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionFullscreen : Instruction
{
    [SerializeField] private FullScreenMode m_Mode = FullScreenMode.FullScreenWindow;
    
    protected override Task Run(Args args)
    {
        Screen.fullScreenMode = this.m_Mode;
        return DefaultResult;
    }
}
