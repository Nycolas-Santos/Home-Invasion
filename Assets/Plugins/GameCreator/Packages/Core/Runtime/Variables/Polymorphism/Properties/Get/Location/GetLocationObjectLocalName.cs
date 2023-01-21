using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Variables
{
    [Title("Game Object Local Name Variable")]
    [Category("Variables/Game Object Local Name Variable")]

    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple)]
    [Description("Returns the Game Object value of a Local Name Variable")]
    
    [Serializable] [HideLabelsInEditor]
    public class GetLocationObjectLocalName : PropertyTypeGetLocation
    {
        [SerializeField]
        protected FieldGetLocalName m_Variable = new FieldGetLocalName(ValueGameObject.TYPE_ID);

        [SerializeField] private bool m_Rotate = true;

        public override Location Get(Args args)
        {
            return new Location(this.m_Variable.Get<GameObject>(), Vector3.zero, this.m_Rotate);
        }

        public override Location Get(GameObject gameObject)
        {
            return new Location(this.m_Variable.Get<GameObject>(), Vector3.zero, this.m_Rotate);
        }

        public override string String => this.m_Variable.ToString();
    }
}