using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace Game.Scripts.Phone
{
    public class Phone : MonoBehaviour
    {
        [SerializeField] private GameObject UI;

        public static Phone Instance { get; set; }

        private void Awake()
        {
            Instance = this;
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
            return gameObject.activeSelf;
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