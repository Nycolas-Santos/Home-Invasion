using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Inventory/Tooltip Socket UI")]
    [Icon(RuntimePaths.PACKAGES + "Inventory/Editor/Gizmos/GizmoTooltipUI.png")]
    
    public class TooltipSocketUI : TTooltipUI
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private RuntimeItemUI m_ItemUI = new RuntimeItemUI();
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private SocketUI m_SocketUI;
        
        // INITIALIZERS: --------------------------------------------------------------------------
        
        protected override void OnEnable()
        {
            base.OnEnable();
            SocketUI.EventHoverEnter -= this.OnHoverEnter;
            SocketUI.EventHoverEnter += this.OnHoverEnter;

            SocketUI.EventHoverExit -= this.OnHoverExit;
            SocketUI.EventHoverExit += this.OnHoverExit;

            SocketUI.EventSocketAttach -= this.OnSocketAttach;
            SocketUI.EventSocketAttach += this.OnSocketAttach;
            
            SocketUI.EventSocketDetach -= this.OnSocketDetach;
            SocketUI.EventSocketDetach += this.OnSocketDetach;
        }

        protected override void OnDisable()
        {
            base.OnEnable();
            SocketUI.EventHoverEnter -= this.OnHoverEnter;
            SocketUI.EventHoverExit -= this.OnHoverExit;
            SocketUI.EventSocketAttach -= this.OnSocketAttach;
            SocketUI.EventSocketDetach -= this.OnSocketDetach;
        }
        
        // CALLBACKS: -----------------------------------------------------------------------------

        private void OnHoverEnter(SocketUI socketUI)
        {
            this.m_SocketUI = socketUI;
            this.RefreshUI();
        }

        private void OnHoverExit(SocketUI socketUI)
        {
            this.m_SocketUI = null;
            this.RefreshUI();
        }
        
        private void OnSocketAttach(SocketUI socketUI)
        {
            this.RefreshUI();
        }
        
        private void OnSocketDetach(SocketUI socketUI)
        {
            this.RefreshUI();
        }
        
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void RefreshUI()
        {
            if (this.m_SocketUI == null)
            {
                this.SetTooltip(false);
                return;
            }

            RuntimeSocket socket = this.m_SocketUI.RuntimeSocket;
            if (socket is { HasAttachment: false })
            {
                this.SetTooltip(false);
                return;
            }

            this.m_ItemUI.RefreshUI(socket.Attachment.Bag, socket.Attachment, true);
            this.SetTooltip(true);
        }
    }
}