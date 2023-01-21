using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Variables
{
    [Title("At Index")]
    [Category("At Index")]
    
    [Description("Replaces the list element at a specific position")]
    [Image(typeof(IconListIndex), ColorTheme.Type.Yellow)]
    
    [Serializable]
    public class SetPickIndex : TListSetPick
    {
        [SerializeField] private int m_Index = 0;

        public override int GetIndex(ListVariableRuntime list, int count) => this.m_Index;
        public override int GetIndex(int count) => this.m_Index;

        public override string ToString() => this.m_Index.ToString();
    }
}