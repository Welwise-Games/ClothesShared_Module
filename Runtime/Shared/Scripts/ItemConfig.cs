using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WelwiseClothesSharedModule.Runtime.Shared.Scripts
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "ItemConfig", order = 2)]
    public class ItemConfig : ScriptableObject, IIndexableItemConfig
    {
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        [field: SerializeField] public string ItemIndex { get; private set; }
        [field: SerializeField] public string ItemName { get; private set; }
        [field: SerializeField] public ItemCategory ItemCategory { get; private set; }
        [field: SerializeField] public AssetReference PrefabReference { get; private set; }
    }
}