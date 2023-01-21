using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Inventory/Tooltip Drag UI")]
    [Icon(RuntimePaths.PACKAGES + "Inventory/Editor/Gizmos/GizmoTooltipUI.png")]
    
    public class TooltipDragUI : TTooltipUI
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private RuntimeItemUI m_ItemUI = new RuntimeItemUI();
        
        [SerializeField] private Texture2D m_CursorDragIcon;
        [SerializeField] private Vector2 m_CursorDragPointer = Vector2.zero;
        
        // INITIALIZERS: --------------------------------------------------------------------------
        
        protected override void OnEnable()
        {
            base.OnEnable();
            BagCellUI.EventBeginDrag -= this.OnBeginDrag;
            BagCellUI.EventBeginDrag += this.OnBeginDrag;
            
            BagCellUI.EventEndDrag -= this.OnEndDrag;
            BagCellUI.EventEndDrag += this.OnEndDrag;
        }
        
        protected override void OnDisable()
        {
            base.OnEnable();
            BagCellUI.EventBeginDrag -= this.OnBeginDrag;
            BagCellUI.EventEndDrag -= this.OnEndDrag;
        }
        
        // CALLBACKS: -----------------------------------------------------------------------------

        private void OnBeginDrag(BagCellUI cellUI, PointerEventData pointerEventData)
        {
            if (cellUI.Cell?.RootRuntimeItem == null)
            {
                this.SetTooltip(false);
                return;
            }

            this.m_ItemUI.RefreshUI(
                cellUI.Cell.RootRuntimeItem.Bag,
                cellUI.Cell.RootRuntimeItem, 
                true
            );
            
            if (this.m_CursorDragIcon != null)
            {
                Cursor.SetCursor(
                    this.m_CursorDragIcon,
                    this.m_CursorDragPointer,
                    CursorMode.Auto
                );
            }
            
            this.SetTooltip(true);
        }

        private void OnEndDrag(BagCellUI cellUI, PointerEventData pointerEventData)
        {
            this.SetTooltip(false);
            if (this.m_CursorDragIcon != null)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }
}