using Cysharp.Threading.Tasks;
using WelwiseSharedModule.Runtime.Shared.Scripts;

namespace WelwiseClothesSharedModule.Runtime.Shared.Scripts
{
    public class ItemsConfigsProviderService
    {
        public const string ItemsConfigsAssetId = "ItemsConfig";
        
        private readonly Container _container = new Container();
        
        public async UniTask<ItemsConfig> GetItemsConfigAsync() =>
            await _container.GetOrLoadAndRegisterObjectAsync<ItemsConfig>(
                ItemsConfigsAssetId);
    }
}