using System;
using UnityEngine;

namespace GameCreator.Editor.Common.Versions
{
    [Serializable]
    internal class AssetVersion
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private int major;
        [SerializeField] private int minor;
        [SerializeField] private int patch;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public int Major => this.major;
        public int Minor => this.minor;
        public int Patch => this.patch;
        
        // TO STRING: -----------------------------------------------------------------------------

        public override string ToString() => $"{this.Major}.{this.Minor}.{this.Patch}";
    }
}