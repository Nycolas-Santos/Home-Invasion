using System;
using System.Threading.Tasks;
using Game.Scripts.Phone;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

[Version(1, 0, 0)]
    
[Title("Open Phone UI")]
[Description("Open the Phone UI")]

[Category("Custom/Open Phone UI")]

[Keywords("Phone", "UI", "Open")]
[Image(typeof(IconMobile), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionOpenPhoneUI : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private bool m_WaitToClose;
    // PROPERTIES: ----------------------------------------------------------------------------
    
    public override string Title => 
        $"Open Phone UI and wait == {this.m_WaitToClose}";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override async Task Run(Args args)
    {
        var phone = Phone.Instance;
        if (phone == null) return;
        
        phone.OpenUI();
        await this.While(() => this.m_WaitToClose && phone.IsOpen());
    }
}
