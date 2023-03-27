using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Version(1, 0, 1)]
    [Dependency("inventory", 2, 1, 3)]

    [Title("Is Equipped With Sockets")]
    [Description("Returns true if the Bag's wearer has an Item Equipped with Sockets")]

    [Category("Inventory/Equipment/Is Equipped With Sockets")]
    
    [Parameter("Bag", "The targeted Bag")]
    [Parameter("Item", "The item type to check")]
    [Parameter("EquipmentSlot", "The index of the Equipment slot to check")]
    [Parameter("Attachments", "The Attachments to verify")]
    

    [Keywords("Inventory", "Container", "Socket", "Equipped")]

    [Image(typeof(IconEquipment), ColorTheme.Type.Blue)]
    [Serializable]
    public class ConditionEquipmentIsEquippedWithSocket : Condition
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        [SerializeField] protected PropertyGetGameObject m_Bag = GetGameObjectPlayer.Create();
        [SerializeField] private PropertyGetItem m_Item = new PropertyGetItem();
        [SerializeField] private Int32 m_EquipmentSlot = 0;
        [SerializeField] private PropertyGetItem[] m_Attachments = Array.Empty<PropertyGetItem>();

        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override string Summary => $"Item {this.m_Item} is Equipped on slot {this.m_EquipmentSlot} in {this.m_Bag} and has sockets attached";
          
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            Item item = this.m_Item.Get(args);

            Bag bag = this.m_Bag.Get<Bag>(args);


            bool itemIsEquipped = bag.Equipment.IsEquippedType(item);
            bool ret = false;
            if(!itemIsEquipped){
                return false;
            }

            RuntimeItem runtimeItem = bag.Content.GetRuntimeItem(bag.Equipment.GetSlotRootRuntimeItemID(m_EquipmentSlot));
           
            foreach (PropertyGetItem value in this.m_Attachments)
            {
                Item attachment = value.Get(args);
                if (attachment == null) continue;

                RuntimeItem runtimeAttachment = attachment.CreateRuntimeItem();
                if (runtimeAttachment == null) continue;
               

                foreach (var slotAttachment in runtimeItem.Sockets)
                {
                    if(slotAttachment.Value.HasAttachment){     
                    
                        RuntimeItem itemRuntimeAttachment =  slotAttachment.Value.Attachment;

                        if(itemRuntimeAttachment.ItemID.ToString() == runtimeAttachment.ItemID.ToString()){
                            ret = true;
                        }
                    }
                  }
            }

            return bag != null && ret;
        }
    }

}


