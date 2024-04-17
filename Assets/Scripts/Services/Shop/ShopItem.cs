namespace AviGamesTest.Services.Shop
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Shop/Item", fileName = "ShopItem")]
    public class ShopItem : ScriptableObject
    {
        public string Id => _id;

        [SerializeField]
        private string _id;
    }
}
