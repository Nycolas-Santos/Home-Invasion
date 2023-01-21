using System;
using UnityEngine;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [Serializable]
    public class RuntimeItemUI : TItemUI
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private GameObject m_ActiveContent;
        [SerializeField] private GameObject m_ActiveEquipped;
        [SerializeField] private GameObject m_ActiveNotEquipped;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected virtual bool ActiveCorner => true; 
        
        [field: NonSerialized] public Bag Bag { get; private set; }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void RefreshUI(Bag bag, RuntimeItem runtimeItem, bool isActive)
        {
            this.Bag = bag;

            if (this.m_ActiveContent != null)
            {
                bool activeCorner = this.ActiveCorner;
                this.m_ActiveContent.SetActive(runtimeItem != null && activeCorner && isActive);
            }

            if (bag != null && this.m_ActiveEquipped != null)
            {
                this.m_ActiveEquipped.SetActive(bag.Equipment.IsEquipped(runtimeItem));
            }
            
            if (bag != null && this.m_ActiveNotEquipped != null)
            {
                this.m_ActiveNotEquipped.SetActive(!bag.Equipment.IsEquipped(runtimeItem));
            }

            this.RefreshItemUI(bag, runtimeItem?.Item);
            this.RefreshRuntimeItemUI(bag, runtimeItem);
        }

        // UI EVENTS: -----------------------------------------------------------------------------

        public void OnHover(RuntimeItem runtimeItem)
        {
            RuntimeItem.UI_LastItemHovered = runtimeItem;
        }

        public void OnSelect(RuntimeItem runtimeItem)
        {
            RuntimeItem.UI_LastItemSelected = runtimeItem;
        }

        // VIRTUAL METHODS: -----------------------------------------------------------------------

        protected virtual Sprite GetIcon(Bag bag, RuntimeItem runtimeItem)
        {
            return runtimeItem?.Item.Info.Sprite(bag != null ? bag.Args : null);
        }
    }
}