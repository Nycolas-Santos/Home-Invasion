using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [CreateAssetMenu(
        fileName = "Stat", 
        menuName = "Game Creator/Stats/Stat",
        order    = 50
    )]
    
    [Icon(EditorPaths.PACKAGES + "Stats/Editor/Gizmos/GizmoStat.png")]
    
    public class Stat : ScriptableObject
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private IdString m_ID = new IdString("stat-id");
        [SerializeField] private StatData m_Data = new StatData();
        [SerializeField] private StatInfo m_Info = new StatInfo();

        // PROPERTIES: ----------------------------------------------------------------------------

        public IdString ID => m_ID;

        public Sprite Icon => this.m_Info.icon;
        public Color Color => this.m_Info.color;

        public double Value => this.m_Data.Base;
        public Formula Formula => this.m_Data.Formula;

        // METHODS: -------------------------------------------------------------------------------

        public string GetAcronym(Args args) => this.m_Info.acronym.Get(args);
        public string GetName(Args args) => this.m_Info.name.Get(args);
        public string GetDescription(Args args) => this.m_Info.description.Get(args);
    }
}
