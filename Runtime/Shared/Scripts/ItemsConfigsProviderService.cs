using Cysharp.Threading.Tasks;
using WelwiseSharedModule.Runtime.Shared.Scripts;

namespace WelwiseClothesSharedModule.Runtime.Shared.Scripts
{
    public class ItemsConfigsProviderService
    {
        private readonly Container _container = new Container();
        private const string ItemsConfigsAssetId = "ItemsConfig";

        public async UniTask<ItemsConfig> GetItemsConfigAsync() =>
            await _container.GetOrLoadAndRegisterObjectAsync<ItemsConfig>(
                ItemsConfigsAssetId);
    }
}