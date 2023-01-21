using GameCreator.Editor.Common;
using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    [CustomEditor(typeof(Merchant))]
    public class MerchantEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;

        private VisualElement m_ContentBuyType;

        private SerializedProperty m_Info;
        
        private SerializedProperty m_InfiniteCurrency;
        private SerializedProperty m_InfiniteStock;
        private SerializedProperty m_AllowBuyBack;
        private SerializedProperty m_SellNicheType;
        private SerializedProperty m_SellType;
        private SerializedProperty m_BuyRate;
        private SerializedProperty m_SellRate;
        private SerializedProperty m_Bag;
        private SerializedProperty m_SkinUI;

        private PropertyField m_FieldInfo;
        private GameCreator.Editor.Common.PropertyTool m_FieldInfiniteCurrency;
        private GameCreator.Editor.Common.PropertyTool m_FieldInfiniteStock;
        private GameCreator.Editor.Common.PropertyTool m_FieldAllowBuyBack;
        private GameCreator.Editor.Common.PropertyTool m_FieldSellNicheType;
        private GameCreator.Editor.Common.PropertyTool m_FieldSellType;
        private GameCreator.Editor.Common.PropertyTool m_FieldBuyRate;
        private GameCreator.Editor.Common.PropertyTool m_FieldSellRate;
        private GameCreator.Editor.Common.PropertyTool m_FieldMerchantBag;
        private GameCreator.Editor.Common.PropertyTool m_FieldMerchantSkin;
        
        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();

            this.m_Info = this.serializedObject.FindProperty("m_Info");
            this.m_InfiniteCurrency = this.serializedObject.FindProperty("m_InfiniteCurrency");
            this.m_InfiniteStock = this.serializedObject.FindProperty("m_InfiniteStock");
            this.m_AllowBuyBack = this.serializedObject.FindProperty("m_AllowBuyBack");
            this.m_SellNicheType = this.serializedObject.FindProperty("m_SellNicheType");
            this.m_SellType = this.serializedObject.FindProperty("m_SellType");
            this.m_BuyRate = this.serializedObject.FindProperty("m_BuyRate");
            this.m_SellRate = this.serializedObject.FindProperty("m_SellRate");
            this.m_Bag = this.serializedObject.FindProperty("m_Bag");
            this.m_SkinUI = this.serializedObject.FindProperty("m_SkinUI");

            this.m_FieldInfo = new PropertyField(this.m_Info);
            this.m_FieldInfiniteCurrency = new GameCreator.Editor.Common.PropertyTool(this.m_InfiniteCurrency);
            this.m_FieldInfiniteStock = new GameCreator.Editor.Common.PropertyTool(this.m_InfiniteStock);
            this.m_FieldAllowBuyBack = new GameCreator.Editor.Common.PropertyTool(this.m_AllowBuyBack);
            this.m_FieldSellNicheType = new GameCreator.Editor.Common.PropertyTool(this.m_SellNicheType);
            this.m_FieldSellType = new GameCreator.Editor.Common.PropertyTool(this.m_SellType);
            this.m_FieldBuyRate = new GameCreator.Editor.Common.PropertyTool(this.m_BuyRate);
            this.m_FieldSellRate = new GameCreator.Editor.Common.PropertyTool(this.m_SellRate);
            this.m_FieldMerchantBag = new GameCreator.Editor.Common.PropertyTool(this.m_Bag);
            this.m_FieldMerchantSkin = new GameCreator.Editor.Common.PropertyTool(this.m_SkinUI);

            this.m_ContentBuyType = new VisualElement();

            this.m_Root.Add(new SpaceSmaller());
            this.m_Root.Add(this.m_FieldInfo);
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(this.m_FieldInfiniteCurrency);
            this.m_Root.Add(this.m_FieldInfiniteStock);
            this.m_Root.Add(this.m_FieldAllowBuyBack);
            this.m_Root.Add(this.m_FieldSellNicheType);
            this.m_Root.Add(this.m_ContentBuyType);
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(this.m_FieldBuyRate);
            this.m_Root.Add(new SpaceSmaller());
            this.m_Root.Add(this.m_FieldSellRate);
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(this.m_FieldMerchantBag);
            this.m_Root.Add(new SpaceSmaller());
            this.m_Root.Add(this.m_FieldMerchantSkin);

            this.RefreshContentBuyType();
            this.m_FieldSellNicheType.EventChange += _ =>
            {
                this.RefreshContentBuyType();
            };
            
            return this.m_Root;
        }

        private void RefreshContentBuyType()
        {
            this.m_ContentBuyType.Clear();
            if (this.m_SellNicheType.boolValue)
            {
                this.m_ContentBuyType.Add(this.m_FieldSellType);
                this.m_FieldSellType.Bind(this.serializedObject);
            }
        }
    }
}