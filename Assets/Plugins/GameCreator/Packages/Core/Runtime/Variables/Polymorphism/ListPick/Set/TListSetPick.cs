using System;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Variables
{
    [Title("From List")]
    
    [Serializable]
    public abstract class TListSetPick : IListSetPick
    {
        public abstract int GetIndex(ListVariableRuntime list, int count);

        public abstract int GetIndex(int count);
    }
}