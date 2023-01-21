using System;
using GameCreator.Runtime.Common.SaveSystem;
using UnityEngine;

namespace GameCreator.Runtime.Common
{
    [Serializable]
    public class GeneralSave
    {
        [SerializeReference] private IDataStorage m_System = new StoragePlayerPrefs();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public IDataStorage System => this.m_System;
    }
}