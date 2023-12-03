using Zenject;

public class ChangeItemVisitor
{
    private IPersistentData _persistentData;

    public int ID { get; private set; }

    [Inject]
    public ChangeItemVisitor(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void Visit(ShopItem shopItem) =>
        ID = _persistentData.PlayerData.ChangeItemID(shopItem.TypeItem);
}