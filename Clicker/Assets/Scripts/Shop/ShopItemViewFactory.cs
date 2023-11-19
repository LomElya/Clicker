using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemtView _cursorItemPrefab;
    [SerializeField] private ShopItemtView _autoClickItemPrefab;

    public ShopItemtView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(_cursorItemPrefab, _autoClickItemPrefab);
        visitor.Visit(1);

        ShopItemtView instance = Instantiate(visitor.Prefab, parent);
        instance.Init(shopItem);

        return instance;
    }

    private class ShopItemVisitor : IIntVisitor
    {
        private ShopItemtView _cursorItemPrefab;
        private ShopItemtView _autoClickItemPrefab;

        public ShopItemVisitor(ShopItemtView cursorItemPrefab, ShopItemtView autoClickItemPrefab)
        {
            _cursorItemPrefab = cursorItemPrefab;
            _autoClickItemPrefab = autoClickItemPrefab;
        }

        public ShopItemtView Prefab { get; private set; }

        public void Visit(int id)
        {
            Prefab = _cursorItemPrefab;
        }
    }
}
