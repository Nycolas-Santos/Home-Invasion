using GameCreator.Runtime.Common;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameCreator.Runtime.Inventory
{
    public class InventorySettings : AssetRepository<InventoryRepository>
    {
        public override IIcon Icon => new IconItem(ColorTheme.Type.TextLight);
        public override string Name => "Inventory";

        #if UNITY_EDITOR

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= this.OnChangePlayMode;
            EditorApplication.playModeStateChanged += this.OnChangePlayMode;
            
            this.RefreshItemList();
        }

        private void OnChangePlayMode(PlayModeStateChange playModeStateChange)
        {
            this.RefreshItemList();
        }

        private void RefreshItemList()
        {
            string[] itemsGuids = AssetDatabase.FindAssets($"t:{nameof(Item)}");
            Item[] items = new Item[itemsGuids.Length];

            for (int i = 0; i < itemsGuids.Length; i++)
            {
                string itemsGuid = itemsGuids[i];
                string itemPath = AssetDatabase.GUIDToAssetPath(itemsGuid);
                items[i] = AssetDatabase.LoadAssetAtPath<Item>(itemPath);
            }

            this.Get().Items.Set(items);
        }

        #endif
    }
}
