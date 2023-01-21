using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Dialogue
{
    public struct Tag
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public IdString Name { get; }
        public int NodeId { get; }

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public Tag(IdString tag, int nodeId)
        {
            this.Name = tag;
            this.NodeId = nodeId;
        }
    }
}