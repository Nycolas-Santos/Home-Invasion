using System;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Serializable]
    public class ItemInstructions
    {
        [SerializeField] private InstructionList m_Instructions;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public InstructionList List => this.m_Instructions;
        
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public ItemInstructions(params Instruction[] instructions)
        {
            this.m_Instructions = instructions.Length == 0
                ? new InstructionList()
                : new InstructionList(instructions);
        }
    }
}