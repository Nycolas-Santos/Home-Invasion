using System;
using System.Threading.Tasks;
using Game.Scripts.Phone;
using Game.Scripts.Phone.Contacts;
using GameCreator.Runtime.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(1, 0, 1)]

    [Title("Set Current Contact")]
    [Category("Custom/Set Current Contact")]
    [Keywords("Contact", "Current", "Set")]

    [Image(typeof(IconShotFirstPerson), ColorTheme.Type.Blue)]
    public class InstructionSetCurrentMessagingContact : Instruction
    {
        
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField] private Phone.Contact contact;
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string Title => $"Set {this.contact}'s as messaging";
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            var messagesTab = Object.FindObjectOfType<PhoneMessageTab>(true);

            if (messagesTab == null) return DefaultResult;
            
            switch (contact)
            {
                case Phone.Contact.Grandma:
                    messagesTab.SetCurrentMessagingContact(Object.FindObjectOfType<PhoneGrandma>(true));
                    break;
                case Phone.Contact.Father:
                    messagesTab.SetCurrentMessagingContact(Object.FindObjectOfType<PhoneFather>(true));
                    break;
                case Phone.Contact.Boyfriend:
                    messagesTab.SetCurrentMessagingContact(Object.FindObjectOfType<PhoneBoyfriend>(true));
                    break;
                case Phone.Contact.Ash:
                    messagesTab.SetCurrentMessagingContact(Object.FindObjectOfType<PhoneAsh>(true));
                    break;
                case Phone.Contact.Unknown:
                    messagesTab.SetCurrentMessagingContact(Object.FindObjectOfType<PhoneUnknown>(true));
                    break;
                case Phone.Contact.Professor:
                    messagesTab.SetCurrentMessagingContact(Object.FindObjectOfType<PhoneProfessor>(true));
                    break;
                case Phone.Contact.Friend:
                    messagesTab.SetCurrentMessagingContact(Object.FindObjectOfType<PhoneFriend>(true));
                    break;
                case Phone.Contact.Stalker:
                    messagesTab.SetCurrentMessagingContact(Object.FindObjectOfType<PhoneStalker>(true));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return DefaultResult;
        }
    }
}