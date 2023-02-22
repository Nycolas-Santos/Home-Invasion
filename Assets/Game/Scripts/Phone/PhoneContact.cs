using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Phone
{
    public class PhoneContact : MonoBehaviour
    {
        // MEMBERS
        [SerializeField] private List<GameObject> messageLogs;
        [SerializeField] private Sprite contactPicture;
        
        private PhoneMessageLogs _phoneMessageLogs;
        private PhoneMessageTab _phoneMessageTab;

        private Button _button;
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
        // FUNCTIONS

        private void Awake()
        {
            if (_phoneMessageLogs == null) _phoneMessageLogs = FindObjectOfType<PhoneMessageLogs>(true);
            if (_phoneMessageTab == null) _phoneMessageTab = FindObjectOfType<PhoneMessageTab>(true);
            if (_button == null) _button = GetComponent<Button>();
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
            _phoneMessageLogs.UpdateContact(this);
            _phoneMessageLogs.SetupMessages();
            _phoneMessageLogs.gameObject.SetActive(true);
            _phoneMessageTab.SetCurrentMessagingContact(this);
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
