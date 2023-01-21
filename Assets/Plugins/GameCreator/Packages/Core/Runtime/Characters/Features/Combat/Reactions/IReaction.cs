using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Characters
{
    public interface IReaction
    {
        // METHODS: -------------------------------------------------------------------------------
        
        bool CanRun(Character character, Args args, ReactionInput input);
        ReactionOutput Run(Character character, Args args, ReactionInput input);
    }
}