using System;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Stats
{
    [Serializable]
    public class AttributeInfo : TInfo
    {
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public AttributeInfo() : base()
        {
            this.acronym = new PropertyGetString("ATT");
            this.name = new PropertyGetString("Attribute Name");
            this.description = new PropertyGetString("Description...");
        }
    }
}