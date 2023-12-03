using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemtView _cursorShopItemPrefab;
    [SerializeField] private ShopItemtView _autoClickShopItemPrefab;
    [SerializeField] private ShopItemtView _itemShopItemPrefab;

    public ShopItemtView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(_cursorShopItemPrefab, _autoClickShopItemPrefab, _itemShopItemPrefab);
        visitor.Visit(shopItem.TypeItem);

        ShopItemtView instance = Instantiate(visitor.Prefab, parent);
        instance.Init(shopItem);

        return instance;
    }

    private class ShopItemVisitor : IVisitor<ItemType>
    {
        private ShopItemtView _cursorShopItemPrefab;
        private ShopItemtView _autoClickShopItemPrefab;
        private ShopItemtView _itemShopItemPrefab;

        public ShopItemVisitor(ShopItemtView cursorItemPrefab, ShopItemtView autoClickItemPrefab, ShopItemtView itemShopItemPrefab)
        {
            _cursorShopItemPrefab = cursorItemPrefab;
            _autoClickShopItemPrefab = autoClickItemPrefab;
            _itemShopItemPrefab = itemShopItemPrefab;
        }

        public ShopItemtView Prefab { get; private set; }

        public void Visit(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Cursor:
                    Prefab = _cursorShopItemPrefab;
                    break;

                case ItemType.AutoClick:
                    Prefab = _autoClickShopItemPrefab;
                    break;

                case ItemType.Clickable:
                    Prefab = _itemShopItemPrefab;
                    break;
            }

        }
    }
}
