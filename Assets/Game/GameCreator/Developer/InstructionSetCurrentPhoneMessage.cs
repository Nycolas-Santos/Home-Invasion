using System;
using System.Threading.Tasks;
using Game.Scripts.Phone;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Dialogue;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;


[Version(1, 0, 0)]
    
[Title("Set Current Phone Message")]
[Description("Sets the current phone message")]

[Category("Custom/Set Current Phone Message")]

[Keywords("Set", "Phone", "Message", "Current")]
[Image(typeof(IconMobile), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionSetCurrentPhoneMessage : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [FormerlySerializedAs("m_GlobalNameVariables")] [SerializeField] private GlobalNameVariables m_GlobalVariables;
    [SerializeField] private Dialogue m_Message;

    private const string DEFAULT_CURRENT_MESSAGE = "Current-Message";
    private const string INVALID_CURRENT_MESSAGE_VARIABLE = "Invalid Global Variables: No Current-Message variable";
    private const string MISSING_REFERENCE = "Missing reference";
    
    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => 
        $"Setup Current Phone Message: {this.m_Message.gameObject.name}";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        var message = this.m_Message;
        var globalVariables = this.m_GlobalVariables;
        var messagesTab = Object.FindObjectOfType<PhoneMessageTab>(true);

        if (message == null || globalVariables == null) {Debug.LogWarning(MISSING_REFERENCE); return DefaultResult;}
        if (globalVariables.Exists(DEFAULT_CURRENT_MESSAGE) == false) Debug.LogWarning(INVALID_CURRENT_MESSAGE_VARIABLE); 
        
        globalVariables.Set(DEFAULT_CURRENT_MESSAGE,message.gameObject);
        return DefaultResult;
    }
}
