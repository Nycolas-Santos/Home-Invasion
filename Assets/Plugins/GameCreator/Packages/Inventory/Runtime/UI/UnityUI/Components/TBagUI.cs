using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [Icon(RuntimePaths.PACKAGES + "Inventory/Editor/Gizmos/GizmoBagUI.png")]
    public abstract class TBagUI : MonoBehaviour
    {
        private const string ERR_TYPE = "Unexpected Bag type set to '{0}'. Expected type '{1}'";
        
        #if UNITY_EDITOR

        [UnityEditor.InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode()
        {
            IsOpen = false;
            LastBagOpened = null;
            LastBagUIOpened = null;
            EventOpen = null;
            EventClose = null;
        }
        
        #endif
        
        public enum EnumDropAmount
        {
            One,
            Stack
        }
        
        public static Bag LastBagOpened;
        public static TBagUI LastBagUIOpened;
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_DefaultBag = GetGameObjectNone.Create();
        
        [SerializeField] private GameObject m_PrefabCell;
        [SerializeField] private bool m_CanDropOutside = true;
        [SerializeField] private float m_MaxDropDistance = 1f;
        [SerializeField] private EnumDropAmount m_DropAmount = EnumDropAmount.One;
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        [field: NonSerialized] public static bool IsOpen { get; private set; }

        [field: NonSerialized] public Bag Bag { get; private set; }
        [field: NonSerialized] public MerchantUI MerchantUI { get; private set; }
        
        protected GameObject PrefabCell => this.m_PrefabCell;
        protected abstract Type ExpectedBagType { get; }
        protected abstract RectTransform Content { get; }

        public bool CanDropOutside
        {
            get => this.m_CanDropOutside;
            set => this.m_CanDropOutside = value;
        }

        public float MaxDropDistance
        {
            get => this.m_MaxDropDistance;
            set => this.m_MaxDropDistance = value;
        }

        public EnumDropAmount DropAmount
        {
            get => this.m_DropAmount;
            set => this.m_DropAmount = value;
        }

        public virtual Item FilterByParent
        {
            get => null;
            set => this.RefreshUI();
        }
        
        // EVENTS: --------------------------------------------------------------------------------

        public event Action EventRefreshUI;

        public static event Action EventOpen;
        public static event Action EventClose;
        
        // INITIALIZERS: --------------------------------------------------------------------------

        protected virtual void Awake()
        {
            Bag bag = this.m_DefaultBag.Get<Bag>(this.gameObject);
            if (bag == null) return;

            this.Bag = bag;
        }

        protected virtual void OnEnable()
        {
            IsOpen = true;

            if (this.Bag != null)
            {
                this.Bag.EventChange -= this.RefreshUI;
                this.Bag.EventChange += this.RefreshUI;

                BagCellUI.EventSelect -= this.OnSelectCell;
                BagCellUI.EventSelect += this.OnSelectCell;

                this.RefreshUI();
            }
            
            EventOpen?.Invoke();
        }
        
        protected virtual void OnDisable()
        {
            if (this.Bag != null) this.Bag.EventChange -= this.RefreshUI;
            BagCellUI.EventSelect -= this.OnSelectCell;
            
            IsOpen = false;
            EventClose?.Invoke();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void OpenUI(Bag bag, MerchantUI merchantUI = null)
        {
            LastBagOpened = bag;
            LastBagUIOpened = this;
            this.MerchantUI = merchantUI;
            
            this.ChangeBag(bag);
            this.SelectFirst();
        }

        public virtual void RefreshUI()
        {
            if (this.Bag.Type != this.ExpectedBagType)
            {
                Debug.LogError(string.Format(ERR_TYPE, this.Bag.Type, this.ExpectedBagType.Name));
                return;
            }
            
            this.EventRefreshUI?.Invoke();
        }

        public void ChangeBag(Bag bag)
        {
            if (this.Bag != null) this.Bag.EventChange -= this.RefreshUI;
            
            this.Bag = bag;
            
            if (this.Bag == null) return;
            if (!this.isActiveAndEnabled) return;

            this.Bag.EventChange -= this.RefreshUI;
            this.Bag.EventChange += this.RefreshUI;
            
            BagCellUI.EventSelect -= this.OnSelectCell;
            BagCellUI.EventSelect += this.OnSelectCell;
            
            this.RefreshUI();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void OnSelectCell(BagCellUI cellUI)
        {
            this.RefreshUI();
        }

        private void SelectFirst()
        {
            if (this.Content.childCount <= 0) return;
            
            BagCellUI firstItem = this.Content.GetChild(0).Get<BagCellUI>();
            if (firstItem == null) return;
            
            firstItem.Select();
            if (EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(firstItem.gameObject);
            }
        }
    }
}