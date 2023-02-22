using Game.Scripts.Phone;
using UnityEngine;

namespace Game.Scripts
{
    public class PhoneMessageLogs : MonoBehaviour
    {
        // MEMBERS
        [SerializeField] private Transform messageLogsScroll;
        [SerializeField] private PhoneContact currentPhoneContact;
        // FUNCTIONS
        public void UpdateContact(PhoneContact contact)
        {
            currentPhoneContact = contact;
        }
        
        public void SetupMessages()
        {
            DestroyOldMessages();
            SetupNewMessages();
        }
        
        private void DestroyOldMessages()
        {
            for (int i = 0; i < messageLogsScroll.childCount; i++)
            {
                Destroy(messageLogsScroll.GetChild(i));
            }
        }

        private void SetupNewMessages()
        {
            foreach (var message in currentPhoneContact.MessageLogs)
            {
                message.transform.SetParent(messageLogsScroll);
                message.transform.localPosition = Vector3.zero;
                message.transform.localRotation = Quaternion.identity;
                message.SetActive(true);
            }
        }
    }
}
