using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Phone
{
    public class Phone : MonoBehaviour
    {
        [SerializeField] private GameObject UI;

        private PhysicalPhone _physicalPhone;

        private const string NO_PHYSICAL_PHONE_AVAILABLE = "There is no Physical Phone on the Player";

        public static Phone Instance { get; set; }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            Instance = this;
            //_physicalPhone = Object.FindObjectOfType<PhysicalPhone>(); DISABLED UNTIL REFACTOR
            //if (_physicalPhone == null) Debug.LogError(NO_PHYSICAL_PHONE_AVAILABLE); DISABLED UNTIL REFACTOR
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