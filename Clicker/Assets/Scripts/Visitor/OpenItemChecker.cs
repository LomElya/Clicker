using Zenject;

public class OpenItemChecker : IVisitor<ShopItem>
{
    private IPersistentData _persistentData;

    public bool IsOpened { get; private set; }

    [Inject]
    public OpenItemChecker(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void Visit(ShopItem shopItem) =>
        IsOpened = _persistentData.PlayerData.IsOpenItem(shopItem.TypeItem);

}