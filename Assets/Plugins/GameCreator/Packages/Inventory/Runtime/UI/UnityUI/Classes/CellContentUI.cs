using System;
using GameCreator.Runtime.Common.UnityUI;
using UnityEngine;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [Serializable]
    public class CellContentUI : RuntimeItemUI
    {
        private enum DisplayStack
        {
            Always,
            StackGreaterThanOne
        }
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private CellCorner m_ActiveCorner = CellCorner.BottomRight;

        [SerializeField] private DisplayStack m_DisplayStack = DisplayStack.StackGreaterThanOne;
        [SerializeField] private GameObject m_StackContent;
        [SerializeField] private TextReference m_StackCount = new TextReference();

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private Cell m_Cell;
        
        [NonSerialized] private Vector2Int m_StartPosition;
        [NonSerialized] private Vector2Int m_CurrentPosition;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        protected override bool ActiveCorner
        {
            get
            {
                CellCorner corner = this.GetCorner(); 
                return ((int) corner & (int) this.m_ActiveCorner) != 0;
            }
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void RefreshUI(Bag bag, Vector2Int position)
        {
            this.m_Cell = bag.Content.GetContent(position);

            this.m_CurrentPosition = position;
            this.m_StartPosition = bag.Content.FindStartPosition(position);

            RuntimeItem runtimeItem = this.m_Cell is { Available: false }
                ? this.m_Cell.RootRuntimeItem 
                : null;

            this.RefreshUI(bag, runtimeItem, true);

            int stackCountValue = this.m_Cell?.Count ?? 0;
            this.m_StackCount.Text = stackCountValue > 0 
                ? stackCountValue.ToString() 
                : string.Empty;

            if (this.m_DisplayStack == DisplayStack.StackGreaterThanOne)
            {
                if (this.m_StackContent != null)
                {
                    this.m_StackContent.SetActive(stackCountValue > 1);
                }
            }
        }
        
        // UI EVENTS: -----------------------------------------------------------------------------

        public void OnHover(Bag bag, Vector2Int position)
        {
            Cell cell = bag.Content.GetContent(position);
            this.OnHover(cell?.RootRuntimeItem);
        }

        public void OnSelect(Bag bag, Vector2Int position)
        {
            Cell cell = bag.Content.GetContent(position);
            this.OnSelect(cell?.RootRuntimeItem);
        }

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        protected override Sprite GetIcon(Bag bag, RuntimeItem runtimeItem)
        {
            Sprite sprite = base.GetIcon(bag, runtimeItem);
            if (sprite == null) return null;

            int cellsX = runtimeItem.Item.Shape.Width;
            int cellsY = runtimeItem.Item.Shape.Height;
            
            if (cellsX == 1 && cellsY == 1) return sprite;

            int cellsOffsetX = this.m_CurrentPosition.x - this.m_StartPosition.x;
            int cellsOffsetY = this.m_CurrentPosition.y - this.m_StartPosition.y;

            float cellW = sprite.rect.width  / cellsX;
            float cellH = sprite.rect.height / cellsY;
            
            Rect rect = new Rect(
                sprite.rect.x + cellsOffsetX * cellW,
                sprite.rect.y + cellH * (cellsY - cellsOffsetY - 1),
                cellW,
                cellH
            );
            
            return Sprite.Create(sprite.texture, rect, sprite.pivot);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private CellCorner GetCorner()
        {
            if (this.m_Cell == null || this.m_Cell.Available) return 0;
            CellCorner corner = 0;
            
            int cellsX = this.m_Cell.Item.Shape.Width;
            int cellsY = this.m_Cell.Item.Shape.Height;

            int positionX = this.m_CurrentPosition.x - this.m_StartPosition.x;
            int positionY = this.m_CurrentPosition.y - this.m_StartPosition.y;
            
            if (positionX == 0)
            {
                if (positionY == 0) corner |= CellCorner.TopLeft;
                if (positionY == cellsY - 1) corner |= CellCorner.BottomLeft;
            }

            if (positionX == cellsX - 1)
            {
                if (positionY == 0) corner |= CellCorner.TopRight;
                if (positionY == cellsY - 1) corner |= CellCorner.BottomRight;
            }
            
            return corner;
        }
    }
}