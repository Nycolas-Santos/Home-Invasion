using GameCreator.Editor.Common;
using UnityEditor;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    public abstract class StateTLocomotionEditor : StateOverrideAnimatorEditor
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        private VisualElement m_Content;

        private VisualElement m_StandContent;
        private VisualElement m_LandContent;
        private VisualElement m_AirborneContent;
        
        private VisualElement m_BreathContent;
        private VisualElement m_TwitchContent;
        
        private SerializedProperty m_PropertyAirborneMode;

        private SerializedProperty m_PropertyStand;
        private SerializedProperty m_PropertyLand;
        private SerializedProperty m_PropertyAirborne;

        private SerializedProperty m_PropertyBreathing;
        private SerializedProperty m_PropertyTwitching;
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected abstract string NameOfStand { get; }
        protected abstract string NameOfLand { get; }

        // PAINT METHODS: -------------------------------------------------------------------------

        protected override void CreateContent()
        {
            this.m_Content = new VisualElement();
            
            this.m_PropertyAirborneMode = this.serializedObject.FindProperty("m_AirborneMode");

            SerializedProperty useBreathing = this.serializedObject.FindProperty("m_UseBreathing");
            SerializedProperty useTwitching = this.serializedObject.FindProperty("m_UseTwitching");

            this.m_PropertyBreathing = this.serializedObject.FindProperty("m_Breathing");
            this.m_PropertyTwitching = this.serializedObject.FindProperty("m_Twitching");

            this.UpdateProperties();
            
            bool isExpandedStand = this.m_PropertyStand?.isExpanded ?? true;
            bool isExpandedLand = this.m_PropertyLand?.isExpanded ?? true;
            bool isExpandedAirborne = this.m_PropertyAirborne?.isExpanded ?? true;

            if (this.m_PropertyStand != null) this.m_PropertyStand.isExpanded = isExpandedStand;
            if (this.m_PropertyLand != null) this.m_PropertyLand.isExpanded = isExpandedLand;
            if (this.m_PropertyAirborne != null) this.m_PropertyAirborne.isExpanded = isExpandedAirborne;

            this.m_StandContent = new VisualElement();
            this.m_LandContent = new VisualElement();
            this.m_AirborneContent = new VisualElement();

            this.RefreshLocomotion();
            this.RefreshAirborne();
            
            PropertyTool fieldAirborneMode = new PropertyTool(this.m_PropertyAirborneMode);
            
            this.Space();
            this.m_Content.Add(m_StandContent);
            this.Space();
            this.m_Content.Add(m_LandContent);
            this.Space();
            this.m_Content.Add(fieldAirborneMode);
            this.m_Content.Add(m_AirborneContent);

            this.EmptyLine();

            PropertyTool fieldUseBreathing = new PropertyTool(useBreathing);
            PropertyTool fieldUseTwitching = new PropertyTool(useTwitching);

            this.m_BreathContent = new VisualElement();
            this.m_TwitchContent = new VisualElement();
            
            this.m_Content.Add(fieldUseBreathing);
            this.m_Content.Add(this.m_BreathContent);
            if (useBreathing.boolValue)
            {
                this.m_BreathContent.Add(new PropertyTool(this.m_PropertyBreathing));
            }

            this.Space();
            
            this.m_Content.Add(fieldUseTwitching);
            this.m_Content.Add(this.m_TwitchContent);
            if (useTwitching.boolValue)
            {
                m_TwitchContent.Add(new PropertyTool(this.m_PropertyTwitching));
            }
            
            this.Space();

            fieldAirborneMode.EventChange += _ =>
            {
                this.UpdateProperties();
                this.m_PropertyAirborne.isExpanded = true;
                
                this.RefreshAirborne();
            };
            
            fieldUseBreathing.EventChange += changeEvent =>
            {
                this.m_BreathContent.Clear();
                if (changeEvent.changedProperty.boolValue)
                {
                    PropertyTool fieldBreathing = new PropertyTool(this.m_PropertyBreathing);
                    fieldBreathing.Bind(this.serializedObject);
                    this.m_BreathContent.Add(fieldBreathing);
                }
            };
            
            fieldUseTwitching.EventChange += changeEvent =>
            {
                this.m_TwitchContent.Clear();
                if (changeEvent.changedProperty.boolValue)
                {
                    PropertyTool fieldTwitching = new PropertyTool(this.m_PropertyTwitching);
                    fieldTwitching.Bind(this.serializedObject);
                    this.m_TwitchContent.Add(fieldTwitching);
                }
            };
            
            this.m_Root.Add(this.m_Content);
        }

        private void UpdateProperties()
        {
            this.m_PropertyStand = this.serializedObject.FindProperty(this.NameOfStand);
            this.m_PropertyLand = this.serializedObject.FindProperty(this.NameOfLand);

            SerializedProperty airborneSingle = this.serializedObject.FindProperty("m_AirborneSingle");
            SerializedProperty airborneVertical = this.serializedObject.FindProperty("m_AirborneVertical");
            SerializedProperty airborneDirectional = this.serializedObject.FindProperty("m_AirborneDirectional");

            this.m_PropertyAirborne = m_PropertyAirborneMode.enumValueIndex switch
            {
                0 => airborneSingle, // Single
                1 => airborneVertical, // Vertical
                2 => airborneDirectional, // Directional
                _ => null
            };
        }
        
        private void RefreshLocomotion()
        {
            this.m_StandContent.Clear();
            this.m_LandContent.Clear();

            PropertyTool fieldStand = new PropertyTool(this.m_PropertyStand);
            PropertyTool fieldLand = new PropertyTool(this.m_PropertyLand);
            
            fieldStand.Bind(this.serializedObject);
            fieldLand.Bind(this.serializedObject);
            
            this.m_StandContent.Add(fieldStand);
            this.m_LandContent.Add(fieldLand);
        }
        
        private void RefreshAirborne()
        {
            this.m_AirborneContent.Clear();

            PropertyTool fieldAirborne = new PropertyTool(this.m_PropertyAirborne);
            fieldAirborne.Bind(this.serializedObject);

            this.m_AirborneContent.Add(fieldAirborne);
        }

        private void EmptyLine()
        {
            VisualElement space = new VisualElement();
            space.AddToClassList("gc-space-small");

            this.m_Content.Add(space);
        }
        
        private void Space()
        {
            VisualElement space = new VisualElement();
            space.AddToClassList("gc-space-smaller");

            this.m_Content.Add(space);
        }
    }
}