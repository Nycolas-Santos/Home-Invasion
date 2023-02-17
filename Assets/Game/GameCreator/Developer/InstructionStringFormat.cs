using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Version(1, 0, 0)]
    
    [Title("Format String")]
    [Description("Format String To Microsoft C# Format check this out to know more about this: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings")]
    [Category("Math/Text/Format String")]
    [Keywords("Format", "digits", "String Format", "ToString")]
    [Image(typeof(IconString), ColorTheme.Type.Yellow, typeof(OverlayPlus))]
    [Serializable]
    public class InstructionStringFormat : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField] private PropertyGetString m_String = new PropertyGetString();
        [SerializeField] private Format m_Format;
        [SerializeField] private PropertyGetInteger m_Modifier = new PropertyGetInteger();

        // PROPERTIES: ----------------------------------------------------------------------------
        public override string Title => $"Format {this.m_String} To {this.m_Format}{this.m_Modifier} ";
        
        public enum Format
        {
            Currency,
            Decimal,
            Scientific,
            FixedPoint,
            General,
            Number,
            Percentage,
            RoundTrip,
            Hexadecimal
        }
        // RUN METHOD: ----------------------------------------------------------------------------
        protected override Task Run(Args args)
        {
            var str = this.m_String.Get(args);
            var modifier = (int)this.m_Modifier.Get(args);
            if (str == null) return DefaultResult;
            switch (this.m_Format)
            {
                case Format.Currency:
                    str = int.Parse(str).ToString("C" + modifier);
                    break;
                case Format.Decimal:
                    str = int.Parse(str).ToString("D" + modifier);
                    break;
                case Format.Scientific:
                    str = int.Parse(str).ToString("E" + modifier);
                    break;
                case Format.FixedPoint:
                    str = int.Parse(str).ToString("F" + modifier);
                    break;
                case Format.General:
                    str = int.Parse(str).ToString("G" + modifier);
                    break;
                case Format.Number:
                    str = int.Parse(str).ToString("N" + modifier);
                    break;
                case Format.Percentage:
                    str = int.Parse(str).ToString("P" + modifier);
                    break;
                case Format.RoundTrip:
                    str = int.Parse(str).ToString("R" + modifier);
                    break;
                case Format.Hexadecimal:
                    str = int.Parse(str).ToString("H" + modifier);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return DefaultResult;
        }
    }
