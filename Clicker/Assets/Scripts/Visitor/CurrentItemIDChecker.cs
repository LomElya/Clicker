using System.Linq;
using Zenject;

public class CurrentItemIDChecker : IVisitor<ShopItem>
{
    private IPersistentData _persistentData;

    public int ID { get; private set; }

    [Inject]
    public CurrentItemIDChecker(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void Visit(ShopItem shopItem) =>
        ID = _persistentData.PlayerData.CurrentIDItem(shopItem.TypeItem);
}