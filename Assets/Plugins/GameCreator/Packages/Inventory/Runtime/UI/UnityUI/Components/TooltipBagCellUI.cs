using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Inventory/Tooltip Cell UI")]
    [Icon(RuntimePaths.PACKAGES + "Inventory/Editor/Gizmos/GizmoTooltipUI.png")]
    
    public class TooltipBagCellUI : TTooltipUI
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private RuntimeItemUI m_ItemUI = new RuntimeItemUI();
        
        // INITIALIZERS: --------------------------------------------------------------------------
        
        protected override void OnEnable()
        {
            base.OnEnable();
            BagCellUI.EventHoverEnter -= this.OnHoverEnter;
            BagCellUI.EventHoverEnter += this.OnHoverEnter;
            
            BagCellUI.EventHoverExit -= this.OnHoverExit;
            BagCellUI.EventHoverExit += this.OnHoverExit;
        }
        
        protected override void OnDisable()
        {
            base.OnEnable();
            BagCellUI.EventHoverEnter -= this.OnHoverEnter;
            BagCellUI.EventHoverExit -= this.OnHoverExit;
        }
        
        // CALLBACKS: -----------------------------------------------------------------------------

        private void OnHoverEnter(BagCellUI cellUI)
        {
            RuntimeItem runtimeItem = RuntimeItem.UI_LastItemHovered;
            if (runtimeItem == null) return;
            
            this.m_ItemUI.RefreshUI(runtimeItem.Bag, runtimeItem, true);
            this.SetTooltip(true);
        }

        private void OnHoverExit(BagCellUI cellUI)
        {
            this.SetTooltip(false);
        }
    }
}