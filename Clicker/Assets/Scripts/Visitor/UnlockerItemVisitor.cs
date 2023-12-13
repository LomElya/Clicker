using Zenject;

public class UnlockerItemVisitor : IVisitor<ShopItem>
{
    private IPersistentData _persistentData;

    [Inject]
    public UnlockerItemVisitor(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void Visit(ShopItem shopItem) =>
        _persistentData.PlayerData.OpenItem(shopItem.TypeItem);

}