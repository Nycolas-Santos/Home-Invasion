using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using UnityEditor;

namespace GameCreator.Editor.Characters
{
    [CustomPropertyDrawer(typeof(LocomotionData))]
    public class LocomotionDataDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Locomotion";
    }
}