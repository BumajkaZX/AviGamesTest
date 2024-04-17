namespace AviGamesTest.Services.Shop
{
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class BuyButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private ShopItem _shopItem;

        [Inject]
        private IShop _shop;

        private void Awake()
        {
            _button.onClick.AddListener(BuyItem);
        }
        private void BuyItem()
        {
            _shop.Buy(_shopItem);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(BuyItem);
        }
    }
}
