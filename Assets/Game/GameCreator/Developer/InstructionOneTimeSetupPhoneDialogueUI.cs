using System;
using System.Threading.Tasks;
using Game.Scripts.Phone;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

[Version(1, 0, 0)]
    
[Title("Setup Phone Dialogue UI")]
[Description("One time setup dialogue to support phone")]

[Category("Custom/Setup Phone Dialogue UI")]

[Keywords("Setup", "Phone", "Dialogue", "UI")]
[Image(typeof(IconMobile), ColorTheme.Type.Blue)]

[Serializable]
public class InstructionOneTimeSetupPhoneDialogueUI : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_DialogueUI = new PropertyGetGameObject();
    
    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => 
        $"Setup Phone Dialogue UI";

    // RUN METHOD: ----------------------------------------------------------------------------
    
    protected override Task Run(Args args)
    {
        var dialogueUI = m_DialogueUI.Get(args);
        if (dialogueUI == null) return DefaultResult;

        var phoneDialogueUI = Object.FindObjectOfType<PhoneDialogue>();
        if (phoneDialogueUI == null) return DefaultResult;
        
        phoneDialogueUI.SetupPhoneUI(dialogueUI);
        return DefaultResult;
    }
}
