using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Cameras
{
    [Serializable]
    [AddComponentMenu("Game Creator/Cameras/Shot Camera")]
    [Icon(RuntimePaths.GIZMOS + "GizmoShot.png")]
    public class ShotCamera : MonoBehaviour
    {
        public enum Clipping
        {
            AvoidClipping,
            ClipThrough
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private bool m_IsMainShot;
        [SerializeField] private TimeMode m_TimeMode = new TimeMode();
        [SerializeField] private Clipping m_Clipping = Clipping.AvoidClipping; 
        
        [SerializeReference] private IShotType m_ShotType = new ShotTypeFixed();

        // PROPERTIES: ----------------------------------------------------------------------------

        public IShotType ShotType => this.m_ShotType;

        public bool IsMainShot => this.m_IsMainShot;
        public bool AvoidClipping => this.m_Clipping == Clipping.AvoidClipping;

        public Vector3 Position => this.m_ShotType?.Position ?? transform.position;
        public Quaternion Rotation => this.m_ShotType?.Rotation ?? transform.rotation;
        
        public Transform Target => this.m_ShotType.Target;

        public Transform[] Ignore => this.m_ShotType.Ignore;

        public virtual bool UseSmoothPosition => this.m_ShotType?.UseSmoothPosition ?? false;
        public virtual bool UseSmoothRotation => this.m_ShotType?.UseSmoothRotation ?? false;

        public TimeMode TimeMode => this.m_TimeMode;

        public bool HasObstacle => this.m_ShotType.HasObstacle;

        // INITIALIZERS: --------------------------------------------------------------------------

        protected virtual void Awake()
        {
            if (this.m_IsMainShot) ShortcutMainShot.Change(this);
            this.m_ShotType?.Awake(this);
        }

        protected virtual void Start()
        {
            this.m_ShotType?.Start(this);
        }

        protected void OnDestroy()
        {
            this.m_ShotType?.Destroy(this);
        }

        // UPDATE METHODS: ------------------------------------------------------------------------

        protected virtual void Update()
        {
            this.m_ShotType?.Update();
        }

        private void OnDrawGizmos()
        {
            this.m_ShotType?.DrawGizmos(this.transform);
        }
        
        private void OnDrawGizmosSelected()
        {
            this.m_ShotType?.DrawGizmosSelected(this.transform);
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public virtual void OnEnableShot(TCamera cameraSystem)
        {
            this.m_ShotType?.OnEnable(cameraSystem);
            this.m_ShotType?.Update();
        }

        public virtual void OnDisableShot(TCamera cameraSystem)
        {
            this.m_ShotType?.OnDisable(cameraSystem);
        }
    }
}