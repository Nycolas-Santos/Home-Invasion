using System;
using System.Threading.Tasks;
using Game.Scripts.Phone;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

[Version(1, 0, 0)]
    
[Title("Close Phone UI")]
[Description("Close the Phone UI")]

[Category("Custom/Close Phone UI")]

[Keywords("Phone", "UI", "Close")]
[Image(typeof(IconMobile), ColorTheme.Type.Red)]

[Serializable]
public class InstructionClosePhoneUI : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    // PROPERTIES: ----------------------------------------------------------------------------
    
    public override string Title => 
        $"Close Phone UI";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        var phone = Phone.Instance;
        if (phone == null) return DefaultResult;
        
        phone.CloseUI();
        return DefaultResult;
    }
}
