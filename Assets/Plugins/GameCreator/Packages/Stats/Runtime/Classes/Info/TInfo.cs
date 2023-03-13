using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [Serializable]
    public abstract class TInfo
    {
        public PropertyGetString name;
        public PropertyGetString acronym;
        public PropertyGetString description;

        public Sprite icon;
        public Color color;
        
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        protected TInfo()
        {
            this.icon = null;
            this.color = Color.grey;
        }
    }
}