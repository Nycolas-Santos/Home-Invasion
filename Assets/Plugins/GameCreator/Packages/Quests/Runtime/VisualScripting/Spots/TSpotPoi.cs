using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Quests
{
    [Keywords("Point", "Interest", "Beacon", "Dot", "Important")]

    [Serializable]
    public abstract class TSpotPoi : Spot
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] protected Vector3 m_Offset = Vector3.zero;
        [SerializeField] protected Space m_Space = Space.Self;

        [SerializeField] private InterestLayer m_Layers = InterestLayers.Every;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Vector3 Position
        {
            get
            {
                Vector3 offset = this.m_Space switch
                {
                    Space.World => this.m_Offset,
                    Space.Self => this.Hotspot.transform.TransformDirection(this.m_Offset),
                    _ => throw new ArgumentOutOfRangeException()
                };

                return this.Hotspot.Position + offset;
            }
        }

        public InterestLayer Layers => this.m_Layers;
        
        [field: NonSerialized] public Hotspot Hotspot { get; private set; }
        [field: NonSerialized] public Args Args { get; private set; }
        
        public abstract string GetName { get; }
        public abstract Sprite GetSprite { get; }
        public abstract Color GetColor { get; }
        
        [field: NonSerialized] protected int Id { get; private set; }

        [field: NonSerialized] protected bool HasBeenDisabled { get; private set; }

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        public override void OnEnable(Hotspot hotspot)
        {
            base.OnEnable(hotspot);
            
            this.Hotspot = hotspot;
            this.Args = new Args(this.Hotspot);

            this.Id = Guid.NewGuid().GetHashCode();
            this.HasBeenDisabled = false;

            hotspot.EventOnActivate += this.OnHotspotActivate;
            hotspot.EventOnDeactivate += this.OnHotspotDeactivate;
            
            this.Refresh();
        }

        public override void OnDisable(Hotspot hotspot)
        {
            base.OnDisable(hotspot);
            
            this.HasBeenDisabled = true;
            
            hotspot.EventOnActivate -= this.OnHotspotActivate;
            hotspot.EventOnDeactivate -= this.OnHotspotDeactivate;
            
            this.Refresh();
        }

        // ABSTRACT METHODS: ----------------------------------------------------------------------

        protected abstract void OnHotspotActivate();
        protected abstract void OnHotspotDeactivate();
        
        protected abstract void Refresh();
        public abstract bool Filter(bool hiddenQuests, bool hideUntracked);

        public override void OnGizmos(Hotspot hotspot)
        {
            base.OnGizmos(hotspot);

            Gizmos.color = Color.yellow;
            Vector3 position = hotspot.transform.TransformPoint(this.m_Offset);
            
            Gizmos.DrawLine(hotspot.transform.position, position);
            GizmosExtension.Octahedron(position, Quaternion.identity, 0.05f, 3);
        }
    }
}
