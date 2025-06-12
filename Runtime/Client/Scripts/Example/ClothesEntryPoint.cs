using UnityEngine;
using WelwiseClothesSharedModule.Runtime.Shared.Scripts;

namespace WelwiseClothesSharedModule.Runtime.Client.Scripts.Example
{
    public class ClothesEntryPoint
    {
        private ItemsConfigsProviderService _itemsConfigsProviderService = new();
        private ClothesFactory _clothesFactory = new();

        public async void OnCreatePlayerAsync(
            PlayerColorableClothesViewSerializableComponents playerColorableClothesViewSerializableComponents,
            ClientEquippedItemsData clientEquippedItemsData)
        {
            ClothesSharedTools.GetPlayerColorableClothesViewController(
                await _itemsConfigsProviderService.GetItemsConfigAsync(), _clothesFactory,
                playerColorableClothesViewSerializableComponents, clientEquippedItemsData);
        }
    }
}