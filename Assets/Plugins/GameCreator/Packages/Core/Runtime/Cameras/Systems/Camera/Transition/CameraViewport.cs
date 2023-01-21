using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Cameras
{
    [Serializable]
    public class CameraViewport
    {
        [NonSerialized] private TCamera m_Camera;
        
        [NonSerialized] private bool m_Projection;
        [NonSerialized] private AnimFloat m_FieldOfView;
        [NonSerialized] private AnimFloat m_OrthographicSize;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool Projection => this.m_Projection;
        public float FieldOfView => this.m_FieldOfView.Current;
        public float OrthographicSize => this.m_OrthographicSize.Current;

        // INITIALIZERS: --------------------------------------------------------------------------

        internal void OnEnable(TCamera camera)
        {
            this.m_Camera = camera;
            
            this.m_Projection = camera.Get<Camera>().orthographic;
            this.m_FieldOfView = new AnimFloat(camera.Get<Camera>().fieldOfView, 0f);
            this.m_OrthographicSize = new AnimFloat(camera.Get<Camera>().orthographicSize, 0f);

            camera.Transition.EventCut -= this.OnChangeShot;
            camera.Transition.EventTransition -= this.OnChangeShot;
            
            camera.Transition.EventCut += this.OnChangeShot;
            camera.Transition.EventTransition += this.OnChangeShot;
        }

        internal void OnDisable(TCamera camera)
        {
            camera.Transition.EventCut -= this.OnChangeShot;
            camera.Transition.EventTransition -= this.OnChangeShot;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void SetProjection(bool isOrthographic)
        {
            this.m_Projection = isOrthographic;
            this.m_Camera.Get<Camera>().orthographic = isOrthographic;
        }
        
        public void SetFieldOfView(float value, float smooth = 0f)
        {
            this.m_FieldOfView.Smooth = smooth;
            this.m_FieldOfView.Target = value;

            if (smooth >= float.Epsilon) return;
            
            this.m_FieldOfView.Current = value;
            this.m_Camera.Get<Camera>().fieldOfView = value;
        }
        
        public void SetOrthographicSize(float value, float smooth = 0f)
        {
            this.m_OrthographicSize.Smooth = smooth;
            this.m_OrthographicSize.Target = value;

            if (smooth >= float.Epsilon) return;
            
            this.m_OrthographicSize.Current = value;
            this.m_Camera.Get<Camera>().orthographicSize = value;
        }
        
        // UPDATE METHOD: -------------------------------------------------------------------------

        internal void NormalUpdate()
        {
            this.OnUpdateValues(this.m_Camera.Time.DeltaTime);
            this.OnUpdateCamera();
        }

        internal void FixedUpdate()
        {
            this.OnUpdateValues(this.m_Camera.Time.FixedDeltaTime);
            this.OnUpdateCamera();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void OnUpdateValues(float deltaTime)
        {
            this.m_FieldOfView.UpdateWithDelta(this.m_FieldOfView.Target, deltaTime);
            this.m_OrthographicSize.UpdateWithDelta(this.m_OrthographicSize.Target, deltaTime);
        }

        private void OnUpdateCamera()
        {
            this.m_Camera.Get<Camera>().orthographic = this.m_Projection;
            this.m_Camera.Get<Camera>().fieldOfView = this.m_FieldOfView.Current;
            this.m_Camera.Get<Camera>().orthographicSize = this.m_OrthographicSize.Current;
        }
        
        private void OnChangeShot(ShotCamera shotCamera)
        {
            IShotType shotType = shotCamera.ShotType;
            if (shotType.GetSystem(ShotSystemViewport.ID) is not ShotSystemViewport view)
            {
                return;
            }

            float smooth = view.SmoothTime;
            
            if (view.ChangeProjection) this.SetProjection(view.Projection);
            if (view.ChangeFieldOfView) this.SetFieldOfView(view.FieldOfView, smooth);
            if (view.ChangeOrthographicSize) this.SetFieldOfView(view.OrthographicSize, smooth);

            this.OnUpdateCamera();
        }
    }
}