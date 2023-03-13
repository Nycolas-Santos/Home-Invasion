using System;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [Serializable]
    public class StatusEffectInstruction
    {
        [SerializeField] private InstructionList m_Instructions;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public InstructionList List => this.m_Instructions;
        
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public StatusEffectInstruction(params Instruction[] instructions)
        {
            this.m_Instructions = instructions.Length == 0
                ? new InstructionList()
                : new InstructionList(instructions);
        }
    }
}