using GameCreator.Editor.Common;
using GameCreator.Runtime.Stats;
using UnityEditor;

namespace GameCreator.Editor.Stats
{
    [CustomPropertyDrawer(typeof(TInfo), true)]
    public class TInfoDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "UI";
    }
}