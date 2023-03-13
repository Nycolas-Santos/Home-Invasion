namespace GameCreator.Runtime.Stats.Formulas
{
    public class Domain
    {
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public Table Table { get; }
        public Traits Source { get; }
        public Traits Target { get; }

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public Domain(Traits source, Traits target, Table table)
        {
            this.Table = table;
            this.Source = source;
            this.Target = target;
        }
    }
}