using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Dialogue
{
    [Serializable]
    public class Expressions
    {
        public const string NAME_EXPRESSIONS = nameof(m_Expressions);
        
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private Expression[] m_Expressions = { new Expression() };
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public int Length => this.m_Expressions.Length;
        
        // METHODS: -------------------------------------------------------------------------------

        public Expression FromId(IdString id)
        {
            if (this.m_Expressions.Length == 0) return null;
            
            foreach (Expression expression in this.m_Expressions)
            {
                if (expression.Id.Hash == id.Hash) return expression;
            }

            return this.m_Expressions[0];
        }

        public Expression FromIndex(int index)
        {
            if (this.m_Expressions.Length == 0) return null;
            index = Math.Clamp(index, 0, this.m_Expressions.Length);

            return this.m_Expressions[index];
        }
    }
}