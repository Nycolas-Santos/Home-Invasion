using System;

namespace GameCreator.Runtime.Characters
{
    public readonly struct ReactionOutput
    {
        public static readonly ReactionOutput None = new ReactionOutput();

        // PROPERTIES: ----------------------------------------------------------------------------
        
        /// <summary>
        /// The source length in seconds of the animation clip played
        /// </summary>
        [field: NonSerialized] public float Length { get; }
        
        /// <summary>
        /// The speed coefficient applied to the animation clip played
        /// </summary>
        [field: NonSerialized] public float Speed { get; }
        
        /// <summary>
        /// The Reaction asset reference played
        /// </summary>
        [field: NonSerialized] public Reaction Reaction { get; }

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public ReactionOutput(float length, float speed, Reaction reaction)
        {
            this.Length = length;
            this.Speed = speed;
            this.Reaction = reaction;
        }
    }
}