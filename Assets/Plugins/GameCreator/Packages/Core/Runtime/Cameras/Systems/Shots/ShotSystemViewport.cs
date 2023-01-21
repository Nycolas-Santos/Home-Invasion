using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Cameras
{
    [Serializable]
    public class ShotSystemViewport : TShotSystem
    {
        public static readonly int ID = nameof(ShotSystemViewport).GetHashCode();

        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [Space] [SerializeField] private float m_SmoothTime = 0.01f;
        
        [SerializeField] private EnablerProjection m_Projection = new EnablerProjection();
        [SerializeField] private EnablerFloat m_FieldOfView = new EnablerFloat(false, 60f);
        [SerializeField] private EnablerFloat m_OrthographicSize = new EnablerFloat(false, 5f);

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override int Id => ID;

        public float SmoothTime => this.m_SmoothTime;
        
        public bool ChangeProjection => this.m_Projection.IsEnabled;
        public bool ChangeFieldOfView => this.m_FieldOfView.IsEnabled;
        public bool ChangeOrthographicSize => this.m_OrthographicSize.IsEnabled;

        public bool Projection => this.m_Projection.Value == EnablerProjection.Type.Orthographic;
        public float FieldOfView => this.m_FieldOfView.Value;
        public float OrthographicSize => this.m_OrthographicSize.Value;
    }
}