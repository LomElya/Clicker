using Zenject;

public class CountItemVisitor
{
    private IPersistentData _persistentData;

    public int Count { get; private set; }

    [Inject]
    public CountItemVisitor(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void Visit(ShopItem shopItem) =>
        Count = _persistentData.PlayerData.CountItem(shopItem.TypeItem, shopItem.AddCount);
}