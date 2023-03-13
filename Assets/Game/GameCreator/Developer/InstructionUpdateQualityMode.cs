using System;
using System.Threading.Tasks;
using Game.Scripts;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

[Version(1, 0, 0)]
    
[Title("Update Quality Mode")]
[Description("Update Quality Mode")]

[Category("Custom/Update Quality")]

[Keywords("Update", "Quality", "Mode")]
[Image(typeof(IconComputer), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionUpdateQualityMode : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    // PROPERTIES: ----------------------------------------------------------------------------
    public override string Title => $"Update Quality Mode";
    // RUN METHOD: ----------------------------------------------------------------------------
    
    protected override Task Run(Args args)
    {
        GameSettings.Instance.UpdateQualityMode();
        return DefaultResult;
    }
}
