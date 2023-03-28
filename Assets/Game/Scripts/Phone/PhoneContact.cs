using System;
using System.Collections.Generic;
using GameCreator.Runtime.Dialogue.UnityUI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Phone
{
    public class PhoneContact : MonoBehaviour
    {
        // MEMBERS
        [BoxGroup("Contact Settings")]
        [SerializeField] private string contactName;
        [BoxGroup("Contact Settings")]
        [SerializeField] private Sprite contactPicture;
        [BoxGroup("Contact Previous Messages")]
        [SerializeField] private List<Message> messages;
        [BoxGroup("Current Contact Messages")]
        [SerializeField] private List<GameObject> messageLogs;
        

        private Phone _phone;
        private PhoneMessageLogs _phoneMessageLogs;
        private PhoneMessageTab _phoneMessageTab;
        private PhoneCacheMessagesTab _phoneCacheMessagesTab;
        private PhoneCacheMessagesLogs _phoneCacheMessagesLogs;

        private Button _button;

        private const string PLAYER_MESSAGE_NAME = "You";
        // PROPERTIES
        public List<GameObject> MessageLogs
        {
            get => messageLogs;
            set => messageLogs = value;
        }

        public Sprite ContactPicture
        {
            get => contactPicture;
            set => contactPicture = value;
        }
        
        [Serializable]
        public struct Message
        {
            [ShowIf("type", Type.Message)]
            public Sender sender;
            public Type type;
            [TextArea] public string content;
            public enum Type
            {
                Message,
                Date
            }
            public enum Sender
            {
                Player,
                Contact
            }
        }
        // FUNCTIONS

        private void Awake()
        {
            if (_phoneMessageLogs == null) _phoneMessageLogs = FindObjectOfType<PhoneMessageLogs>(true);
            if (_phoneMessageTab == null) _phoneMessageTab = FindObjectOfType<PhoneMessageTab>(true);
            if (_phoneCacheMessagesTab == null) _phoneCacheMessagesTab = FindObjectOfType<PhoneCacheMessagesTab>(true);
            if (_button == null) _button = GetComponent<Button>();
            if (_phone == null) _phone = GetComponentInParent<Phone>(true);
            if (messages.Count > 0) SetupContactMessages();
        }

        private void SetupContactMessages()
        {
            foreach (var message in messages)
            {
                GameObject newMessage = null;
                Text contentText;
                Text contactNameText;
                switch (message.type)
                {
                    case Message.Type.Message:
                        switch (message.sender)
                        {
                            case Message.Sender.Player:
                                newMessage = Instantiate(_phone.PlayerMessagePrefab,transform.position,transform.rotation,transform);
                                contactNameText = newMessage.GetComponentInChildren<PhoneMessageContactName>().GetComponent<Text>();
                                contactNameText.text = PLAYER_MESSAGE_NAME;
                                break;
                            case Message.Sender.Contact:
                                newMessage = Instantiate(_phone.ContactMessagePrefab,transform.position,transform.rotation,transform);
                                contactNameText = newMessage.GetComponentInChildren<PhoneMessageContactName>().GetComponent<Text>();
                                contactNameText.text = contactName;
                                break;
                        }
                        contentText = newMessage.GetComponentInChildren<PhoneMessageText>().GetComponent<Text>();

                        contentText.text = message.content;
                        
                        break;
                    case Message.Type.Date:
                        newMessage = Instantiate(_phone.DateMessagePrefab,transform.position,transform.rotation,transform);
                        
                        contentText = newMessage.GetComponentInChildren<PhoneMessageText>().GetComponent<Text>();

                        contentText.text = message.content;
                        break;
                }
                newMessage.SetActive(false);
                messageLogs.Add(newMessage);
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(EnterContactMessages);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(EnterContactMessages);
        }

        public void EnterContactMessages()
        {
            //_phoneMessageLogs.UpdateContact(this); #OLD METHOD
            //_phoneMessageLogs.SetupMessages(); #OLD METHOD
            //_phoneMessageLogs.gameObject.SetActive(true); #OLD METHOD
            _phoneCacheMessagesTab.OpenCacheMessages(this);
        }

        public void AddMessageLog(GameObject[] messages)
        {
            foreach (var message in messages)
            {
                messageLogs.Add(message);
                message.transform.SetParent(transform);
                message.SetActive(false);
            }
        }
    }
}
