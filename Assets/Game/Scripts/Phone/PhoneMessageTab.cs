using System;
using UnityEngine;

namespace Game.Scripts.Phone
{
    public class PhoneMessageTab : MonoBehaviour
    {
        // MEMBERS
        private PhoneContact currentMessagingContact;
        // FUNCTIONS

        public void SetCurrentMessagingContact(PhoneContact contact)
        {
            currentMessagingContact = contact;
            UpdateContactPicture();
        }
        public void SetupMessagesTab(PhoneContact contact)
        {
            GetComponentInChildren<PhoneContactPicture>().SetContact(contact);
        }
        
        public void UpdateContactPicture()
        {
            SetupMessagesTab(currentMessagingContact);
        }
    }
}