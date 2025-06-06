using System;
using System.Collections.Generic;

namespace WelwiseClothesSharedModule.Runtime.Shared.Scripts
{
    [Serializable]
    public class ClientEquippedItemsData
    {
        public List<EquippedItemData> ItemsData { get; set; }

        public ClientEquippedItemsData() {}
        public ClientEquippedItemsData(List<EquippedItemData> itemsData) => ItemsData = itemsData;
    }
}