using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(ValuesNodeTexts))]
    public class ValuesNodeTextsDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Texts";
    }
}