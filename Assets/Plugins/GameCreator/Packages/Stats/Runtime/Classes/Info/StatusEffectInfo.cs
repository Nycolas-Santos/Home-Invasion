using System;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Stats
{
    [Serializable]
    public class StatusEffectInfo : TInfo
    {
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public StatusEffectInfo() : base()
        {
            this.acronym = new PropertyGetString("SE");
            this.name = new PropertyGetString("Status Effect Name");
            this.description = new PropertyGetString("Description...");
        }
    }
}