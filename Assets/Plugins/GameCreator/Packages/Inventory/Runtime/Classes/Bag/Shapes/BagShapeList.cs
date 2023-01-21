using System;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Serializable]
    public class BagShapeList : TBagShapeWithWeight
    {
        [SerializeField] private bool m_HasMaxHeight;
        [SerializeField] private int m_Height = 200;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override int MaxWidth => 1;
        public override int MaxHeight => this.HasInfiniteHeight ? int.MaxValue : this.m_Height;

        public override bool HasInfiniteWidth => false;
        public override bool HasInfiniteHeight => !this.m_HasMaxHeight;

        public override bool CanIncreaseWidth => false;
        public override bool CanDecreaseWidth => false;
        
        public override bool CanIncreaseHeight => true;
        public override bool CanDecreaseHeight => false;

        // METHODS: -------------------------------------------------------------------------------

        public override bool IncreaseWidth(int numColumns) => false;
        public override bool DecreaseWidth(int numColumns) => false;

        public override bool IncreaseHeight(int numRows)
        {
            if (!this.CanIncreaseHeight) return false;
            this.m_Height += numRows;
            
            this.ExecuteEventChange();
            return true;
        }
        
        public override bool DecreaseHeight(int numRows) => false;
    }
}