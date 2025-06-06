using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WelwiseClothesSharedModule.Runtime.Shared.Scripts;
using Object = UnityEngine.Object;

namespace WelwiseClothesSharedModule.Runtime.Client.Scripts
{
    public class ClothesSkinnedMeshRendererController
    {
        public event Action<ItemConfig, ItemConfig, ColorableClothesSerializableComponents> UpdatedInstance;
        public event Action<ItemConfig> RemovedInstance;

        private ItemConfig _targetItemConfig;
        private bool _shouldEnableInstances = true;

        private readonly List<GameObject> _defaultClothesInstance;
        private readonly List<GameObject> _prefabInstances = new List<GameObject>();
        private readonly Transform _modelTransform;
        private readonly SkinnedMeshRenderer _modelSkinnedMeshRenderer;
        private readonly ClothesFactory _clothesFactory;


        public ClothesSkinnedMeshRendererController(SkinnedMeshRenderer modelSkinnedMeshRenderer,
            Transform modelTransform, ClothesFactory clothesFactory, List<GameObject> defaultClothesInstance)
        {
            _modelSkinnedMeshRenderer = modelSkinnedMeshRenderer;
            _modelTransform = modelTransform;
            _clothesFactory = clothesFactory;
            _defaultClothesInstance = defaultClothesInstance;
        }

        public void SetShouldEnableInstances(bool shouldEnableInstances) => _shouldEnableInstances = shouldEnableInstances;

        public void SetActivePrefabInstances() => _prefabInstances.ForEach(instance => instance.gameObject.SetActive(_shouldEnableInstances));

        public async void UpdateInstanceAsync(ItemConfig itemConfig, bool shouldTakeOff)
        {
            shouldTakeOff = shouldTakeOff || itemConfig == null;
            
            if (shouldTakeOff)
            {
                if (_targetItemConfig != null)
                    RemovedInstance?.Invoke(_targetItemConfig);
             
                _prefabInstances.ForEach(Object.Destroy);
                _prefabInstances.Clear();
                _targetItemConfig = null;
            }
            else
            {
                var instance = await GetInstantiatedPrefabsAsync(_modelSkinnedMeshRenderer, itemConfig, _modelTransform);
                _prefabInstances.ForEach(Object.Destroy);
                _prefabInstances.Clear();
                _prefabInstances.Add(instance);
                SetActivePrefabInstances();
            }
            
            _defaultClothesInstance?.ForEach(instance => instance.SetActive(shouldTakeOff));
        }

        private async UniTask<GameObject> GetInstantiatedPrefabsAsync(SkinnedMeshRenderer prefabSkinnedMeshRenderer, ItemConfig itemConfig,
            Transform parent)
        {
            var instance = await _clothesFactory.GetClothesInstanceAsync(itemConfig, parent);

            var skinnedMeshRenderers = instance.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var skinnedRenderer in skinnedMeshRenderers)
            {
                skinnedRenderer.bones = prefabSkinnedMeshRenderer.bones;
                skinnedRenderer.rootBone = prefabSkinnedMeshRenderer.rootBone;
            }

            UpdatedInstance?.Invoke(_targetItemConfig, itemConfig, instance);

            _targetItemConfig = itemConfig;

            return instance.gameObject;
        }
    }
}