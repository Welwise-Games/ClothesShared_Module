using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WelwiseClothesSharedModule.Runtime.Shared.Scripts;
using WelwiseSharedModule.Runtime.Shared.Scripts.AddressablesPart;

namespace WelwiseClothesSharedModule.Runtime.Client.Scripts
{
    public class ClothesFactory
    {
        public async UniTask<ColorableClothesSerializableComponents> GetClothesInstanceAsync(
            ItemConfig itemConfig, Transform parent)
        {
            var prefab = await AssetProvider.LoadAsync<ColorableClothesSerializableComponents>(
                await itemConfig.PrefabReference.GetAssetIdAsync());
            
            var instance = Object.Instantiate(prefab, parent);
            SetPrefabInstanceLayer(instance.gameObject, parent.gameObject.layer);
            return instance;
        }
        
        private void SetPrefabInstanceLayer(GameObject instance, int targetLayer)
        {
            instance.GetComponentsInChildren<Transform>().ToList()
                .ForEach(transform => transform.gameObject.layer = targetLayer);
        }
    }
}