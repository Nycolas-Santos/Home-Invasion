using System;

namespace GameCreator.Runtime.Inventory.UnityUI
{
    [Serializable]
    public class ItemUI : TItemUI
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void RefreshUI(Bag bag, Item item)
        {
            this.RefreshItemUI(bag, item);

            RuntimeItem dummyRuntimeItem = new RuntimeItem(item);
            this.RefreshRuntimeItemUI(bag, dummyRuntimeItem);
        }
    }
}