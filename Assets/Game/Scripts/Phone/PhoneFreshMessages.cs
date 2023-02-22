using UnityEngine;

namespace Game.Scripts.Phone
{
    public class PhoneFreshMessages : MonoBehaviour
    {
        public void StoreMessages(PhoneContact contact)
        {
            // Create an array to hold the child transforms
            GameObject[] messages = new GameObject[transform.childCount];

            // Loop through each child transform and add it to the array
            for (int i = 0; i < transform.childCount; i++) {
                messages[i] = transform.GetChild(i).gameObject;
            }
            contact.AddMessageLog(messages);
        }
    }
}
