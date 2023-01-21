using System;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Serializable]
    public class ItemConditions
    {
        [SerializeField] private ConditionList m_Conditions;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public ConditionList List => this.m_Conditions;
        
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public ItemConditions(params Condition[] conditions)
        {
            this.m_Conditions = conditions.Length == 0
                ? new ConditionList()
                : new ConditionList(conditions);
        }
    }
}