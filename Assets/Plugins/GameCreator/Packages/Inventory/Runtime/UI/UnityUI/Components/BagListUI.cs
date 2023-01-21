using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Inventory/Bag List UI")]
    [Icon(RuntimePaths.PACKAGES + "Inventory/Editor/Gizmos/GizmoBagUI.png")]
    
    public class BagListUI : TBagUI
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Item m_FilterByParent;
        [SerializeField] private RectTransform m_Content;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override Item FilterByParent
        {
            get => this.m_FilterByParent;
            set
            {
                this.m_FilterByParent = value;
                this.RefreshUI();
            }
        }
        
        protected override Type ExpectedBagType => typeof(BagList);
        protected override RectTransform Content => this.m_Content;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void RefreshUI()
        {
            if (this.Bag == null) return;
            base.RefreshUI();

            int numCells = this.Bag.Content.CountWithoutStack;
            int numChildren = this.m_Content.childCount;

            int numCreate = numCells - numChildren;
            int numDelete = numChildren - numCells;

            for (int i = numCreate - 1; i >= 0; --i) this.CreateCell();
            for (int i = numDelete - 1; i >= 0; --i) this.DeleteCell(numCells + i);

            for (int j = 0; j < numCells; ++j)
            {
                Transform child = this.m_Content.GetChild(j);
            
                BagCellUI cellUI = child.Get<BagCellUI>();
                if (cellUI != null) cellUI.RefreshUI(this, 0, j);
            }
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CreateCell()
        {
            Instantiate(this.PrefabCell, this.m_Content.transform);
        }
        
        private void DeleteCell(int index)
        {
            Transform child = this.m_Content.transform.GetChild(index);
            Destroy(child.gameObject);
        }
    }
}