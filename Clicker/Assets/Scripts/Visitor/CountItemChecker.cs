using System.Linq;
using Zenject;

public class CountItemChecker : IVisitor<ShopItem>
{
    private IPersistentData _persistentData;

    public int Count { get; private set; }

    [Inject]
    public CountItemChecker(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void Visit(ShopItem shopItem) =>
        Count = _persistentData.PlayerData.QuantityItem(shopItem.TypeItem);

    public void Visit(ItemType itemType) =>
       Count = _persistentData.PlayerData.QuantityItem(itemType);
}