using System;
using System.Threading.Tasks;
using Game.Scripts;
using Game.Scripts.Phone;
using Game.Scripts.Phone.Contacts;
using GameCreator.Runtime.Common;
using UnityEngine;
using Object = UnityEngine.Object;


namespace GameCreator.Runtime.VisualScripting
{
    [Version(1, 0, 1)]

    [Title("Store Fresh Messages")]
    [Description("Simple instruction to store all fresh messages to message log of the phone contacts")]
    [Category("Custom/Store Fresh Messages")]
    [Keywords("Message", "Store", "Fresh", "Set")]

    [Image(typeof(IconMessage), ColorTheme.Type.Blue)]
    
    public class InstructionStoreFreshMessages : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField] private Phone.Contact contact;
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string Title => $"Store {this.contact}'s fresh messages to message log";
        // RUN METHOD: ----------------------------------------------------------------------------
        
        protected override Task Run(Args args)
        {
            PhoneContact contact = null;
            switch (this.contact)
            {
                case Phone.Contact.Grandma:
                    contact = Object.FindObjectOfType<PhoneGrandma>(true);
                    break;
                case Phone.Contact.Father:
                    contact = Object.FindObjectOfType<PhoneFather>(true);
                    break;
                case Phone.Contact.Boyfriend:
                    contact = Object.FindObjectOfType<PhoneBoyfriend>(true);
                    break;
                case Phone.Contact.Ash:
                    contact = Object.FindObjectOfType<PhoneAsh>(true);
                    break;
                case Phone.Contact.Unknown:
                    contact = Object.FindObjectOfType<PhoneUnknown>(true);
                    break;
                case Phone.Contact.Professor:
                    contact = Object.FindObjectOfType<PhoneProfessor>(true);
                    break;
                case Phone.Contact.Friend:
                    contact = Object.FindObjectOfType<PhoneFriend>(true);
                    break;
                case Phone.Contact.Stalker:
                    contact = Object.FindObjectOfType<PhoneStalker>(true);
                    break;
            }
            var freshMessages = Object.FindObjectOfType<PhoneFreshMessages>(true);

            if (contact == null) return DefaultResult;
            if (freshMessages == null) return DefaultResult;

            freshMessages.StoreMessages(contact);
            return DefaultResult;
        }
    }
}