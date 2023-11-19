using Zenject;

public class SelectedItemChecker : IIntVisitor
{
    private IPersistentData _persistentData;

    [Inject]
    public SelectedItemChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public int ID { get; private set; }
    public bool IsSelected { get; private set; }

    public void Visit(int id)
    {
        ID = _persistentData.PlayerData.IDdSelectedClickableItem;

        if (_persistentData.PlayerData.IDdSelectedClickableItem == id)
            IsSelected = true;
        else
            IsSelected = false;
    }

     public void Visit() =>
        ID = _persistentData.PlayerData.IDdSelectedClickableItem;
}
