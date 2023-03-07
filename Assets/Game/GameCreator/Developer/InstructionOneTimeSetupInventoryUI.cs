using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

[Version(1, 0, 0)]
    
[Title("Setup Inventory UI")]
[Description("One time setup inventory to support phone")]

[Category("Custom/Setup Inventory UI")]

[Keywords("Setup", "Phone", "Inventory", "UI")]
[Image(typeof(IconMobile), ColorTheme.Type.Light)]

[Serializable]
public class InstructionOneTimeSetupInventoryUI : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private PropertyGetGameObject m_InventoryUI = new PropertyGetGameObject();
    
    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => 
        $"Setup Phone Inventory UI";

    // RUN METHOD: ----------------------------------------------------------------------------
    
    protected override Task Run(Args args)
    {
        var inventory = m_InventoryUI.Get(args);
        if (inventory == null) return DefaultResult;

        var phoneInventory = Object.FindObjectOfType<PhoneInventory>();
        if (phoneInventory == null) return DefaultResult;
        
        phoneInventory.SetupPhoneUI(inventory);
        return DefaultResult;
    }
}
