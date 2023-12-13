using Zenject;

public class Bootstrap
{
    [Inject]
    public Bootstrap(IPersistentData persistentData, IDataProvider dataProvider)
    {
        if (dataProvider.TryLoad() == false)
            persistentData.PlayerData = new PlayerData();
    }
}
