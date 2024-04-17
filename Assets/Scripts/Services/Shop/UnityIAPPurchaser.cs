namespace AviGamesTest.Services.Shop
{
    using System;
    using System.Threading.Tasks;
    using UnityEngine.Purchasing;
    using UnityEngine.Purchasing.Extension;
    using Unity.Services.Core;
    using Unity.Services.Core.Environments;
    using UnityEngine;
    using Zenject;

    public class UnityIAPPurchaser : IDetailedStoreListener, IShop
    {
        public event Action<string> OnSuccessPurchase = delegate { };
        
        public event Action<string> OnFailedPurchase = delegate { };
        
        public event Action OnStartPurchase;

        private IStoreController _storeController;
        
        [Inject]
        public async Task Init()
        {
            await UnityServices.InitializeAsync();
            
            SetupBuilder();
        }
        

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
        }

        public void Buy(ShopItem item)
        {
            if (_storeController == null || UnityServices.State != ServicesInitializationState.Initialized)
            {
                return;
            }

            OnStartPurchase();
            
            _storeController.InitiatePurchase(_storeController.products.WithID(item.Id));
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            OnSuccessPurchase(purchaseEvent.purchasedProduct.definition.id);
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            OnFailedPurchase(product.definition.id);
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            OnFailedPurchase(product.definition.id);
        }
        
        private void SetupBuilder()
        {
            StandardPurchasingModule.Instance().useFakeStoreAlways = true;
            
            StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
            
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            ResourceRequest assetLoad = Resources.LoadAsync<TextAsset>("IAPProductCatalog");

            
            ProductCatalog catalog = JsonUtility.FromJson<ProductCatalog>((assetLoad.asset as TextAsset).text);
            
            foreach (var item in catalog.allProducts)
            {
                builder.AddProduct(item.id, item.type);
            }
            
            UnityPurchasing.Initialize(this, builder);
        }
        
    }
}