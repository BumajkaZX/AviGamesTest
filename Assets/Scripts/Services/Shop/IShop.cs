namespace AviGamesTest.Services.Shop
{
    using System;
    
    public interface IShop
    {
        public event Action<string> OnSuccessPurchase;
        
        public event Action<string> OnFailedPurchase;
        
        public event Action OnStartPurchase;

        public void Buy(ShopItem item);
    }
}
