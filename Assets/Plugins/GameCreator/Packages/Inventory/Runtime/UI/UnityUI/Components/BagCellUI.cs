using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Inventory/Bag Cell UI")]
    [Icon(RuntimePaths.PACKAGES + "Inventory/Editor/Gizmos/GizmoItemUI.png")]
    
    public class BagCellUI : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler,
        ISubmitHandler,
        IDragHandler,
        IBeginDragHandler,
        IEndDragHandler
    {
        public static event Action<BagCellUI> EventHoverEnter;
        public static event Action<BagCellUI> EventHoverExit;
        public static event Action<BagCellUI> EventSelect;
        public static event Action<BagCellUI, PointerEventData> EventBeginDrag;
        public static event Action<BagCellUI, PointerEventData> EventEndDrag;
        
        #if UNITY_EDITOR

        [UnityEditor.InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode()
        {
            EventHoverEnter = null;
            EventHoverExit = null;
            EventSelect = null;
            EventBeginDrag = null;
            EventEndDrag = null;
        }
        
        #endif
        
        private enum EnumOnDrop
        {
            Nothing,
            Move
        }
        
        private enum EnumOnChoose
        {
            Nothing,
            Select,
            Use,
            Dismantle,
            SendToBag,
            Equip,
            Unequip
        }
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] protected CellContentUI m_CellInfo = new CellContentUI();
        [SerializeField] protected CellMerchantUI m_CellMerchant = new CellMerchantUI();

        [SerializeField] private GameObject m_ActiveSelection;
        [SerializeField] private bool m_CanDrag = true;

        [SerializeField] private EnumOnDrop m_OnDrop = EnumOnDrop.Move;
        [SerializeField] private EnumOnChoose m_OnChoose = EnumOnChoose.Select;
        [SerializeField] private PropertyGetDecimal m_DismantleChance = GetDecimalPercentage.Create();
        [SerializeField] private PropertyGetGameObject m_SendToBag = GetGameObjectPlayer.Create();
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private Vector2Int m_Position;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        [field: NonSerialized] public Cell Cell { get; private set; }
        [field: NonSerialized] public TBagUI BagUI { get; private set; }

        // EVENTS: --------------------------------------------------------------------------------

        public event Action<BagCellUI> EventRefreshUI;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public virtual void RefreshUI(TBagUI bagUI, int i, int j)
        {
            this.BagUI = bagUI;
            this.m_Position = new Vector2Int(i, j);
            
            this.Cell = bagUI.Bag.Content.GetContent(this.m_Position);
            
            this.m_CellInfo.RefreshUI(bagUI.Bag, this.m_Position);
            this.m_CellMerchant.RefreshUI(bagUI, this.m_Position);

            this.RefreshActiveSelection();
            this.EventRefreshUI?.Invoke(this);

            Item item = this.Cell?.RootRuntimeItem?.Item;
            this.gameObject.SetActive(
                bagUI.FilterByParent == null ||
                item != null && item.InheritsFrom(bagUI.FilterByParent.ID)
            );
        }

        // PRIVATE CALLBACKS: ---------------------------------------------------------------------

        public void OnPointerEnter(PointerEventData data)
        {
            if (this.Cell?.RootRuntimeItem == null) return;
            
            this.m_CellInfo.OnHover(this.Cell?.RootRuntimeItem);
            EventHoverEnter?.Invoke(this);
        }
        
        public void OnPointerExit(PointerEventData data)
        {
            EventHoverExit?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData data) => this.OnChoose(data);
        public void OnSubmit(BaseEventData data) => this.OnChoose(data);

        private void OnChoose(BaseEventData data)
        {
            if (this.Cell?.RootRuntimeItem == null) return;
            
            switch (this.m_OnChoose)
            {
                case EnumOnChoose.Nothing:
                    break;
                
                case EnumOnChoose.Select:
                    this.Select();
                    data.Use();
                    break;
                
                case EnumOnChoose.Use:
                    this.Use();
                    data.Use();
                    break;
                
                case EnumOnChoose.Dismantle:
                    float chance = (float) this.m_DismantleChance.Get(this.BagUI.Bag.Args);
                    this.Dismantle(chance);
                    data.Use();
                    break;
                
                case EnumOnChoose.SendToBag:
                    Bag sendToBag = this.m_SendToBag.Get<Bag>(this.BagUI.Bag.Args);
                    this.SendToBag(sendToBag);
                    data.Use();
                    break;

                case EnumOnChoose.Equip:
                    this.Equip();
                    break;
                
                case EnumOnChoose.Unequip:
                    this.Unequip();
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void OnDrag(PointerEventData eventData)
        { }

        public void OnBeginDrag(PointerEventData data)
        {
            if (!this.m_CanDrag) return;
            
            EventBeginDrag?.Invoke(this, data);
            data.Use();
        }
        
        public void OnEndDrag(PointerEventData data)
        {
            if (!this.m_CanDrag) return;
            if (this.Cell?.RootRuntimeItem != null)
            {
                bool successfulDrag =
                    this.OnEndDragSocket(data) ||
                    this.OnEndDragCell(data)   ||
                    this.OnEndDragEquip(data)  ||
                    this.OnEndDragToWorld(data);
            
                if (successfulDrag) data.Use();   
            }

            EventEndDrag?.Invoke(this, data);
        }
        
        // DROP METHODS: --------------------------------------------------------------------------

        private bool OnEndDragSocket(PointerEventData data)
        {
            if (data.pointerEnter == null) return false;
            
            SocketUI targetSocketUI = data.pointerEnter.Get<SocketUI>();
            return targetSocketUI != null && targetSocketUI.OnDropCellUI(this);
        }
        
        private bool OnEndDragCell(PointerEventData data)
        {
            if (data.pointerEnter == null) return false;
            
            BagCellUI targetCellUI = data.pointerEnter.Get<BagCellUI>();
            return targetCellUI != null && targetCellUI.OnDropCellUI(this);
        }

        private bool OnEndDragEquip(PointerEventData data)
        {
            if (data.pointerEnter == null) return false;
            
            BagEquipUI equipUI = data.pointerEnter.Get<BagEquipUI>();
            return equipUI != null && equipUI.OnDropCellUI(this);
        }

        private bool OnEndDragToWorld(PointerEventData data)
        {
            Camera cam = ShortcutMainCamera.Instance.Get<Camera>();
            if (cam == null) return false;

            Ray ray = cam.ScreenPointToRay(data.position);
            if (!Physics.Raycast(ray, out RaycastHit hit) || hit.collider == null)
            {
                return false;
            }

            Trigger trigger = hit.collider.Get<Trigger>();
            if (trigger != null)
            {
                Item.LastItemDropped = this.Cell.Peek();
                trigger.OnReceiveCommand(EventInventoryOnDropItem.COMMAND_DROP_ITEM);
                return true;
            }

            if (this.BagUI == null || !this.BagUI.CanDropOutside) return false;

            Vector3 wearerPosition = this.BagUI.Bag.Wearer.transform.position;
            Vector3 direction = hit.point - wearerPosition;
                
            direction = Vector3.ClampMagnitude(direction, this.BagUI.MaxDropDistance);
            Vector3 point = wearerPosition + direction;
            int dropAmount = this.BagUI.DropAmount switch
            {
                TBagUI.EnumDropAmount.One => 1,
                TBagUI.EnumDropAmount.Stack => this.Cell.Count,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            Item.LastItemDropped = this.Cell.Peek();
            GameObject[] instances = this.BagUI.Bag.Content.Drop(
                this.m_Position, 
                dropAmount, 
                point
            );
                
            return instances.Length > 0;
        }

        // INTERNAL CALLBACKS: --------------------------------------------------------------------
        
        internal bool OnDropCellUI(BagCellUI dropCellUI)
        {
            if (dropCellUI == null) return false;
            if (dropCellUI == this) return false;
            if (this.m_CellInfo.Bag == null) return false;
            if (this.m_CellInfo.Bag != dropCellUI.m_CellInfo.Bag) return false;
            
            IBagContent content = this.m_CellInfo.Bag.Content;
            
            return this.m_OnDrop switch
            {
                EnumOnDrop.Nothing => false,
                EnumOnDrop.Move => content.Move(dropCellUI.m_Position, this.m_Position, true),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Select()
        {
            this.m_CellInfo.OnSelect(this.Cell?.RootRuntimeItem);
            EventSelect?.Invoke(this);
        }
        
        public void Use()
        {
            if (this.Cell == null || this.Cell.Available) return;
            this.m_CellInfo.Bag.Content.Use(this.m_Position);
        }

        public void Dismantle(float chance)
        {
            if (this.Cell == null || this.Cell.Available) return;
            this.Cell.Peek().Dismantle(chance);
        }
        
        public void SendToBag(Bag bag)
        {
            if (this.Cell == null || this.Cell.Available) return;

            RuntimeItem runtimeItem = this.Cell.Peek();
            if (!bag.Content.CanAdd(runtimeItem, true)) return;
            
            RuntimeItem removeRuntimeItem = this.BagUI.Bag.Content.Remove(runtimeItem);
            if (removeRuntimeItem != null) bag.Content.Add(removeRuntimeItem, true);
        }

        public void Equip()
        {
            if (this.Cell == null || this.Cell.Available) return;
            
            RuntimeItem runtimeItem = this.Cell.RootRuntimeItem;
            this.m_CellInfo.Bag.Equipment.Equip(runtimeItem);
        }
        
        public void Unequip()
        {
            if (this.Cell == null || this.Cell.Available) return;
            
            RuntimeItem runtimeItem = this.Cell.RootRuntimeItem;
            this.m_CellInfo.Bag.Equipment.Unequip(runtimeItem);
        }

        public void Drop(float randomRadius = 1f)
        {
            if (this.Cell == null || this.Cell.Available) return;
            
            RuntimeItem runtimeItem = this.Cell.Peek();
            if (!runtimeItem.Item.HasPrefab) return;
            if (!runtimeItem.Item.CanDrop) return;

            Transform target = this.BagUI.Bag.Wearer != null
                ? this.BagUI.Bag.Wearer.transform
                : this.BagUI.Bag.transform;
            
            this.BagUI.Bag.Content.Drop(
                this.m_Position, 1, 
                target.position + UnityEngine.Random.insideUnitSphere * randomRadius
            );
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RefreshActiveSelection()
        {
            if (this.m_ActiveSelection != null)
            {
                RuntimeItem runtimeItem = this.Cell is { Available: false } 
                    ? this.Cell.RootRuntimeItem 
                    : null;
                
                if (runtimeItem == null || RuntimeItem.UI_LastItemSelected == null)
                {
                    this.m_ActiveSelection.SetActive(false);
                }
                else
                {
                    int currentHash = runtimeItem.RuntimeID.Hash;
                    int selectionHash = RuntimeItem.UI_LastItemSelected.RuntimeID.Hash;
                    this.m_ActiveSelection.SetActive(currentHash == selectionHash);
                }
            }
        }
    }
}