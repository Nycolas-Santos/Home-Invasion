using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(0, 0, 1)]
    
    [Title("Raise Signal")]
    [Description("Raises a specific signal, which is captured by other listeners")]

    [Category("Logic/Raise Signal")]

    [Parameter("Signal", "The signal name risen")]

    [Keywords("Event", "Command", "Fire", "Trigger", "Dispatch", "Execute")]
    [Image(typeof(IconSignal), ColorTheme.Type.Red)]
    
    [Serializable]
    public class InstructionLogicRaiseSignal : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private Signal m_Signal;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title
        {
            get
            {
                string signal = this.m_Signal.ToString();
                return string.IsNullOrEmpty(signal) ? "Signal (none)" : $"Signal '{signal}'";
            }
        }

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            SignalArgs data = new SignalArgs(this.m_Signal.Value, args.Self);
            Signals.Raise(data);
            return DefaultResult;
        }
    }
}