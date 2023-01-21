using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Serializable]
    public class StockData : TPolymorphicItem<StockData>
    {
        [SerializeField] private Item m_Item;
        [SerializeField] private int m_Amount = 1;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public Item Item => this.m_Item;
        public int Amount => this.m_Amount;
    }
}