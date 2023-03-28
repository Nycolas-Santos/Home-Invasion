using System;
using GameCreator.Runtime.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Phone
{
    public class Phone : MonoBehaviour
    {
        [BoxGroup("Core Phone Settings")]
        [SerializeField] private GameObject UI;
        
        [BoxGroup("Phone Message Settings")]
        [SerializeField] private GameObject playerMessagePrefab;
        [BoxGroup("Phone Message Settings")]
        [SerializeField] private GameObject contactMessagePrefab;
        [BoxGroup("Phone Message Settings")]
        [SerializeField] private GameObject dateMessagePrefab;

        

        private PhysicalPhone _physicalPhone;

        private const string MISSING_MESSAGE_PREFAB = "MISSING MESSAGE PREFAB";
        private const string NO_PHYSICAL_PHONE_AVAILABLE = "There is no Physical Phone on the Player";

        

        #region Properties

        public static Phone Instance { get; set; }
        
        public GameObject PlayerMessagePrefab
        {
            get => playerMessagePrefab;
            set => playerMessagePrefab = value;
        }

        public GameObject ContactMessagePrefab
        {
            get => contactMessagePrefab;
            set => contactMessagePrefab = value;
        }

        public GameObject DateMessagePrefab
        {
            get => dateMessagePrefab;
            set => dateMessagePrefab = value;
        }

        #endregion
        

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            Instance = this;
            CheckReferences();
            //_physicalPhone = Object.FindObjectOfType<PhysicalPhone>(); DISABLED UNTIL REFACTOR
            //if (_physicalPhone == null) Debug.LogError(NO_PHYSICAL_PHONE_AVAILABLE); DISABLED UNTIL REFACTOR
        }

        private void CheckReferences()
        {
            if (playerMessagePrefab == null || contactMessagePrefab == null || dateMessagePrefab == null)
            {
                Debug.LogError(MISSING_MESSAGE_PREFAB);
            }
        }

        public void CloseUI()
        {
            UI.gameObject.SetActive(false);
        }
        public void OpenUI()
        {
            UI.gameObject.SetActive(true);
        }
        public bool IsOpen()
        {
            return UI.gameObject.activeSelf;
        }

        public void EnableFlashlight()
        {
            _physicalPhone.EnableLight();
        }

        public void DisableFlashlight()
        {
            _physicalPhone.DisableLight();
        }
        public enum Contact
        {
            Grandma,
            Father,
            Boyfriend,
            Ash,
            Unknown,
            Professor,
            Friend,
            Stalker
        }
    }
}