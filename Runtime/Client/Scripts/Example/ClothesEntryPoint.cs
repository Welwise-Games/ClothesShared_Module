using UnityEngine;
using WelwiseClothesSharedModule.Runtime.Shared.Scripts;

namespace WelwiseClothesSharedModule.Runtime.Client.Scripts.Example
{
    public class ClothesEntryPoint
    {
        private ItemsConfigsProviderService _itemsConfigsProviderService = new();
        private ClothesFactory _clothesFactory = new();

        public async void OnCreatePlayerAsync(
            ColorableClothesViewSerializableComponents colorableClothesViewSerializableComponents,
            EquippedItemsData equippedItemsData)
        {
            ClothesSharedTools.GetPlayerColorableClothesViewController(
                await _itemsConfigsProviderService.GetItemsConfigAsync(), _clothesFactory,
                colorableClothesViewSerializableComponents, equippedItemsData);
        }
    }
}