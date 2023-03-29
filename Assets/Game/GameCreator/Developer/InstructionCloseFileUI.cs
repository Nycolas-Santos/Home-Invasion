using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

[Version(1, 0, 0)]
    
[Title("Close File UI")]
[Description("Close the File UI")]

[Category("Custom/Close File UI")]

[Keywords("File", "UI", "Close")]
[Image(typeof(IconPaste), ColorTheme.Type.Red)]

[Serializable]
public class InstructionCloseFileUI : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    // PROPERTIES: ----------------------------------------------------------------------------
    
    public override string Title => 
        $"Close File UI";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        var file = File.Instance;
        if (file == null) return DefaultResult;
        
        file.CloseUI();
        return DefaultResult;
    }
}
