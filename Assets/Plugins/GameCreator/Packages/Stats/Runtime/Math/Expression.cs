using System.Text;

namespace GameCreator.Runtime.Stats.Formulas
{
    public class Expression
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public StringBuilder Term { get; }
        public char Operation { get; set; }
        public bool NeedsEvaluation { get; set; }

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public Expression()
        {
            this.Term = new StringBuilder();
            this.NeedsEvaluation = false;
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool HasOperation()
        {
            return this.Operation != default;
        }
    }
}