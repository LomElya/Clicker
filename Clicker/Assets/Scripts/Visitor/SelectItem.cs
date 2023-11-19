using Zenject;

public class SelectItem : IIntVisitor
{
    private IPersistentData _persistentData;

    [Inject]
    public SelectItem(IPersistentData persistentData) => _persistentData = persistentData;

    public int ID { get; private set; }

    public void Visit(int id)
    {
        _persistentData.PlayerData.IDdSelectedClickableItem = id;
        ID = id;
    }
}
