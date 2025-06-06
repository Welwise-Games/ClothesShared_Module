using System.Collections.Generic;
using System.Linq;
using WelwiseClothesSharedModule.Runtime.Shared.Scripts;
using WelwiseSharedModule.Runtime.Shared.Scripts;
using WelwiseSharedModule.Runtime.Shared.Scripts.Tools;

namespace WelwiseClothesSharedModule.Runtime.Client.Scripts
{
    public static class ClothesSharedTools
    {
        public const string EquippedItemsDataFieldNameForMetaverseSavings = "EquippedItemsData";

        public static PlayerColorableClothesViewController GetPlayerColorableClothesViewController(
            ItemsConfig itemsConfig, ClothesFactory clothesFactory,
            PlayerColorableClothesViewSerializableComponents serializableComponents,
            ClientEquippedItemsData equippedItemsData) =>
            new PlayerColorableClothesViewController(
                equippedItemsData,
                itemsConfig,
                serializableComponents, clothesFactory);
    }
}