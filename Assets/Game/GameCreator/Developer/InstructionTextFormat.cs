using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Version(1, 0, 0)]
    
    [Title("Format Text")]
    [Description("Format Text To Microsoft C# Format check this out to know more about this: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings")]
    [Category("Math/Text/Format Text")]
    [Keywords("Format", "digits", "Text Format", "ToString")]
    [Image(typeof(IconString), ColorTheme.Type.Yellow, typeof(OverlayPlus))]
    [Serializable]
    public class InstructionTextFormat : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField] private PropertyGetGameObject m_Text = new PropertyGetGameObject();
        [SerializeField] private Format m_Format;
        [SerializeField] private PropertyGetInteger m_Modifier = new PropertyGetInteger();

        // PROPERTIES: ----------------------------------------------------------------------------
        public override string Title => $"Format {this.m_Text} To {this.m_Format}{this.m_Modifier} ";
        
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
            var text = this.m_Text.Get<Text>(args);
            var modifier = (int)this.m_Modifier.Get(args);
            if (text == null) return DefaultResult;
            switch (this.m_Format)
            {
                case Format.Currency:
                    text.text = int.Parse(text.text).ToString("C" + modifier);
                    break;
                case Format.Decimal:
                    text.text = int.Parse(text.text).ToString("D" + modifier);
                    break;
                case Format.Scientific:
                    text.text = int.Parse(text.text).ToString("E" + modifier);
                    break;
                case Format.FixedPoint:
                    text.text = int.Parse(text.text).ToString("F" + modifier);
                    break;
                case Format.General:
                    text.text = int.Parse(text.text).ToString("G" + modifier);
                    break;
                case Format.Number:
                    text.text = int.Parse(text.text).ToString("N" + modifier);
                    break;
                case Format.Percentage:
                    text.text = int.Parse(text.text).ToString("P" + modifier);
                    break;
                case Format.RoundTrip:
                    text.text = int.Parse(text.text).ToString("R" + modifier);
                    break;
                case Format.Hexadecimal:
                    text.text = int.Parse(text.text).ToString("H" + modifier);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return DefaultResult;
        }
    }
