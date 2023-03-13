using System;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Stats
{
    [Serializable]
    public class StatInfo : TInfo
    {
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public StatInfo() : base()
        {
            this.acronym = new PropertyGetString("STA");
            this.name = new PropertyGetString("Stat Name");
            this.description = new PropertyGetString("Description...");
        }
    }
}