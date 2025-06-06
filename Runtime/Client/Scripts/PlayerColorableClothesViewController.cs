using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WelwiseClothesSharedModule.Runtime.Shared.Scripts;
using WelwiseSharedModule.Runtime.Shared.Scripts.Tools;

namespace WelwiseClothesSharedModule.Runtime.Client.Scripts
{
    public class PlayerColorableClothesViewController
    {
        private readonly Dictionary<ItemCategory, ClothesSkinnedMeshRendererController>
            _clothesSkinnedMeshRendererControllerByItemCategory;

        private readonly Dictionary<ItemConfig, ColorableClothesSerializableComponents>
            _clothesInstancesComponentsByConfig =
                new Dictionary<ItemConfig, ColorableClothesSerializableComponents>();

        private readonly ItemsConfig _itemsConfig;

        public PlayerColorableClothesViewController(ClientEquippedItemsData equippedItemsData, ItemsConfig itemsConfig,
            PlayerColorableClothesViewSerializableComponents playerColorableClothesViewSerializableComponents,
            ClothesFactory clothesFactory)
        {
            _itemsConfig = itemsConfig;

            _clothesSkinnedMeshRendererControllerByItemCategory = CollectionTools.ToList<ItemCategory>()
                .Where(category => category is not ItemCategory.All and not ItemCategory.Color).ToDictionary(
                    category => category, category =>
                    {
                        var controller = new ClothesSkinnedMeshRendererController(
                            playerColorableClothesViewSerializableComponents.MainSkinnedMeshRenderer,
                            playerColorableClothesViewSerializableComponents.transform, clothesFactory,
                            (playerColorableClothesViewSerializableComponents.DefaultClothesInstances
                                .Find(instance => instance.ItemCategory == category)?.Instances)?.ToList());
                        controller.UpdatedInstance += UpdateInstancesComponentsByConfigDictionary;
                        controller.RemovedInstance += config => _clothesInstancesComponentsByConfig.Remove(config);
                        return controller;
                    });

            SetClothesInstancesByData(equippedItemsData);
        }

        public void TrySettingClothesInstance(ItemConfig itemConfig, ItemCategory itemCategory, bool shouldTakeOff)
        {
            _clothesSkinnedMeshRendererControllerByItemCategory.GetValueOrDefault(itemCategory)
                ?.UpdateInstanceAsync(itemConfig, shouldTakeOff);
        }

        public void TrySettingItemCategoryInstancesActiveState(ItemCategory itemCategory, bool shouldEnableInstances)
        {
            var controller = _clothesSkinnedMeshRendererControllerByItemCategory.GetValueOrDefault(itemCategory);
            
            controller?.SetShouldEnableInstances(shouldEnableInstances);
            controller?.SetActivePrefabInstances();
        }

        public Material[] GetClothesInstanceMaterials(ItemConfig itemConfig) =>
            _clothesInstancesComponentsByConfig.GetValueOrDefault(itemConfig)?.RendererWithMaterials.sharedMaterials;

        public void TrySettingClothesColor(ItemConfig itemConfig, int materialIndex, Color color)
        {
            var instance = _clothesInstancesComponentsByConfig.GetValueOrDefault(itemConfig);

            if (instance && materialIndex < instance.RendererWithMaterials.sharedMaterials.Length)
                instance.RendererWithMaterials.sharedMaterials[materialIndex].color = color;
        }

        private void UpdateInstancesComponentsByConfigDictionary(ItemConfig oldConfig, ItemConfig newConfig,
            ColorableClothesSerializableComponents colorableClothesSerializableComponents)
        {
            if (oldConfig != null)
                _clothesInstancesComponentsByConfig.Remove(oldConfig);

            _clothesInstancesComponentsByConfig.AddOrAppoint(newConfig, colorableClothesSerializableComponents);
        }

        public void SetClothesInstancesByData(ClientEquippedItemsData clientEquippedItemsData)
        {
            foreach (var itemData in clientEquippedItemsData.ItemsData)
            {
                var itemConfig = itemData.ItemIndex == null ? null : _itemsConfig.TryGettingConfig(itemData.ItemIndex);
                var category = itemData.ItemCategory;
                TrySettingClothesInstance(itemConfig, category, false);
            }
        }
    }
}