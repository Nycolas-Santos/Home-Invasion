using UnityEngine;

namespace Game.Scripts
{
    public class DeveloperNotes : MonoBehaviour
    {
        [TextArea]
        public string note;
        private void OnGUI()
        {
            GUI.Label(new Rect(50,50,300,50),"Developer Notes:");
            GUI.Label(new Rect(50,75,300,100),note);
        }
    }
}
