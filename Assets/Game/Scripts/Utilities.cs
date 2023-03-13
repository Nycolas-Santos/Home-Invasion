using UnityEngine;

namespace Game.Scripts
{
    public class Utilities : MonoBehaviour
    {
        public static GameObject FindObjectInHierarchy(GameObject parent, string name)
        {
            if (parent.name == name)
            {
                return parent;
            }
    
            foreach (Transform child in parent.transform)
            {
                GameObject found = FindObjectInHierarchy(child.gameObject, name);
        
                if (found != null)
                {
                    return found;
                }
            }
    
            return null;
        }
    }
}