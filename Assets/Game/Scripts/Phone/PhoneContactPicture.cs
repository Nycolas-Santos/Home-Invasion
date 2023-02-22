using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Phone
{
    public class PhoneContactPicture : MonoBehaviour
    {
        public void SetContact(PhoneContact contact)
        {
            GetComponent<Image>().sprite = contact.ContactPicture;
        }
    }
}