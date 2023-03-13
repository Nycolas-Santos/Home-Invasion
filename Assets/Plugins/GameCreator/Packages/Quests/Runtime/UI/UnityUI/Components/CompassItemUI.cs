using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Common.UnityUI;
using UnityEngine;
using UnityEngine.UI;

namespace GameCreator.Runtime.Quests.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Quests/Compass Item UI")]
    [Icon(RuntimePaths.PACKAGES + "Quests/Editor/Gizmos/GizmoCompassUI.png")]
    
    [Serializable]
    public class CompassItemUI : MonoBehaviour
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private TextReference m_Text;
        [SerializeField] private Image m_Sprite;
        [SerializeField] private Graphic m_Color;

        [SerializeField] private CanvasGroup m_CanvasGroup;
        [SerializeField] private Gradient m_Alpha = new Gradient
        {
            alphaKeys = new[]
            {
                new GradientAlphaKey(0f, 0.0f),
                new GradientAlphaKey(1f, 0.1f),
                new GradientAlphaKey(1f, 0.9f),
                new GradientAlphaKey(0f, 1.0f),
            }
        };

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Refresh(TSpotPoi spot, float ratio)
        {
            if (spot == null) return;

            this.m_Text.Text = spot.GetName;
            
            if (this.m_Sprite != null) this.m_Sprite.overrideSprite = spot.GetSprite;
            if (this.m_Color != null) this.m_Color.color = spot.GetColor;

            if (this.m_CanvasGroup != null)
            {
                this.m_CanvasGroup.alpha = this.m_Alpha.Evaluate(ratio).a;
            }
        }
    }
}