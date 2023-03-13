using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [CreateAssetMenu(
        fileName = "Formula", 
        menuName = "Game Creator/Stats/Formula",
        order    = 50
    )]
    
    [Icon(EditorPaths.PACKAGES + "Stats/Editor/Gizmos/GizmoFormula.png")]
    
    public class Formula : ScriptableObject
    {
        #if UNITY_EDITOR
        
        [UnityEditor.InitializeOnEnterPlayMode]
        public static void InitializeOnEnterPlayMode()
        {
            LastFormulaResult = 0f;
        }
        
        #endif
        
        // STATIC MEMBERS: ------------------------------------------------------------------------
        
        [field: NonSerialized] public static double LastFormulaResult { get; private set; }
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private string m_Formula;
        [SerializeField] private Table m_Table;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool Exists => !string.IsNullOrEmpty(this.m_Formula);
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public double Calculate(Traits source, Traits target)
        {
            LastFormulaResult = Evaluation.Calculate(
                source, target, 
                this.m_Formula, this.m_Table
            );

            return LastFormulaResult;
        }
    }
}