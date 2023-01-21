using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Dialogue
{
    [Title("Choice")]
    [Category("Choice")]
    
    [Image(typeof(IconNodeChoice), ColorTheme.Type.TextLight)]
    [Description("Lets the user choose an option from its children")]

    [Serializable]
    public class NodeTypeChoice : TNodeType
    {
        public static readonly string NAME_SKIP_CHOICE = nameof(m_SkipChoice);
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private bool m_HideUnavailable;
        [SerializeField] private bool m_HideVisited;
        [SerializeField] private bool m_SkipChoice;
        [SerializeField] private bool m_ShuffleChoices;

        [SerializeField] private TimedChoice m_TimedChoice = new TimedChoice();
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private int m_ChosenId;
        
        [NonSerialized] private float m_CurrentDuration;
        [NonSerialized] private float m_CurrentElapsed;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override bool IsBranch => true;
        
        public bool HideUnavailable => this.m_HideUnavailable;

        public bool HideVisited => this.m_HideVisited;
        
        public bool ShuffleChoices => this.m_ShuffleChoices;

        public bool TimedChoice => this.m_TimedChoice.IsTimed;
        
        public TimeoutBehavior Timeout => this.m_TimedChoice.Timeout;

        public float CurrentDuration => this.m_CurrentDuration;
        public float CurrentElapsed => this.m_CurrentElapsed;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public float GetDuration(Args args) => this.m_TimedChoice.GetDuration(args);

        public List<int> GetChoices(Story story, int nodeId, Args args, bool removeUnavailable)
        {
            List<int> children = story.Content.Children(nodeId);
            
            bool visitUnavailable = this.m_HideVisited && story.Visits.Nodes.Contains(nodeId);
            bool skipUnavailable = visitUnavailable || this.m_HideUnavailable || removeUnavailable;

            for (int i = children.Count - 1; i >= 0; --i)
            {
                Node child = story.Content.Get(children[i]);
                if (!skipUnavailable || (child?.CanRun(args) ?? false)) continue;
                
                children.RemoveAt(i);
            }
            
            if (this.m_ShuffleChoices) children.Shuffle();
            return children;
        }

        public void Choose(int nodeId)
        {
            if (this.m_ChosenId != Content.NODE_INVALID) return;
            this.m_ChosenId = nodeId;
        }

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        public override async Task Run(int id, Story story, Args args)
        {
            this.m_ChosenId = Content.NODE_INVALID;
            this.m_CurrentDuration = this.GetDuration(args);
            this.m_CurrentElapsed = 0f;
            
            this.InvokeEventStartChoice(id);
            
            List<int> choices = GetChoices(story, id, args, true);
            if (choices.Count == 1) this.Choose(choices[0]);

            float startTime = story.Time.Time;

            while (this.m_ChosenId == Content.NODE_INVALID && !story.IsCanceled)
            {
                await Task.Yield();
                if (!this.TimedChoice || story.Time.Time < startTime + this.m_CurrentDuration)
                {
                    this.m_CurrentElapsed = story.Time.Time - startTime;
                    continue;
                }

                this.m_CurrentElapsed = this.m_CurrentDuration;
                
                choices = GetChoices(story, id, args, true);
                if (choices.Count == 0) Debug.LogError("There cannot be zero choices");
                
                this.Choose(choices[this.Timeout switch
                {
                    TimeoutBehavior.ChooseRandom => UnityEngine.Random.Range(0, choices.Count),
                    TimeoutBehavior.ChooseFirst => 0,
                    TimeoutBehavior.ChooseLast => choices.Count - 1,
                    _ => throw new ArgumentOutOfRangeException()
                }]);
            }
            
            this.InvokeEventFinishChoice(id);
        }

        public override List<int> GetNext(int id, Story story, Args args)
        {
            if (ApplicationManager.IsExiting) return new List<int>();
            
            story.Visits.Nodes.Add(this.m_ChosenId);
            story.Visits.Tags.Add(story.Content.Get(this.m_ChosenId).Tag);
            
            return this.m_SkipChoice 
                ? story.Content.Children(this.m_ChosenId)
                : new List<int> { this.m_ChosenId };
        }
    }
}