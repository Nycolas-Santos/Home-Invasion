namespace GameCreator.Runtime.Characters
{
    public abstract class TStance : IStance
    {
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public abstract int Id { get; }
        public abstract Character Character { get; set; }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public abstract void OnEnable(Character character);
        public abstract void OnDisable(Character character);

        public abstract void OnUpdate();
    }
}