namespace GameCreator.Runtime.Inventory
{
    public class LastLoot
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public RuntimeItem RuntimeItem { get; }

        public Currency Currency { get; }

        public int Amount { get; }

        public bool IsItem => this.RuntimeItem != null;
        public bool IsCurrency => this.Currency != null;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public LastLoot(RuntimeItem runtimeItem, int amount)
        {
            this.RuntimeItem = runtimeItem;
            this.Amount = amount;
        }
        
        public LastLoot(Currency currency, int amount)
        {
            this.Currency = currency;
            this.Amount = amount;
        }
    }
}