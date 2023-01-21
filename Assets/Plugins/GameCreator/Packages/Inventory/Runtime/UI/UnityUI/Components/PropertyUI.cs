using System;
using System.Globalization;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Common.UnityUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Inventory/Property UI")]
    [Icon(RuntimePaths.PACKAGES + "Inventory/Editor/Gizmos/GizmoPropertyUI.png")]
    
    public class PropertyUI : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        ISelectHandler
    {
        private static readonly CultureInfo CULTURE = CultureInfo.InvariantCulture;
        private const string FORMAT = "+#;-#;0";
        
        public static event Action<PropertyUI> EventHoverEnter;
        public static event Action<PropertyUI> EventHoverExit;
        
        #if UNITY_EDITOR

        [UnityEditor.InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode()
        {
            EventHoverEnter = null;
            EventHoverExit = null;
        }
        
        #endif
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private TextReference m_NumberValue = new TextReference();
        [SerializeField] private TextReference m_StringValue = new TextReference();
        
        [SerializeField] private Image m_Icon;
        [SerializeField] private Graphic m_Color;
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private RuntimeProperty m_RuntimeProperty;

        // PRIVATE CALLBACKS: ---------------------------------------------------------------------

        public void OnPointerEnter(PointerEventData data)
        {
            if (data.dragging) return;
            if (this.m_RuntimeProperty == null) return;
            
            RuntimeItem.UI_LastPropertyHovered = this.m_RuntimeProperty;
            EventHoverEnter?.Invoke(this);
        }
        
        public void OnPointerExit(PointerEventData data)
        {
            if (data.dragging) return;
            EventHoverExit?.Invoke(this);
        }

        public void OnSelect(BaseEventData data)
        {
            if (this.m_RuntimeProperty == null) return;
            RuntimeItem.UI_LastPropertySelected = this.m_RuntimeProperty;
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void RefreshUI(Bag bag, RuntimeItem runtimeItem, RuntimeProperty runtimeProperty)
        {
            this.m_RuntimeProperty = runtimeProperty;

            this.m_NumberValue.Text = runtimeProperty
                .GetTotalNumber(runtimeItem)
                .ToString(FORMAT, CULTURE);
            
            this.m_StringValue.Text = runtimeProperty.Text;

            if (this.m_Icon != null) this.m_Icon.overrideSprite = runtimeProperty.Icon;
            if (this.m_Color != null) this.m_Color.color = runtimeProperty.Color;
        }
    }
}