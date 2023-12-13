using System;
using Zenject;

public class CurrentLevelChecker : IIntVisitor
{
    private IPersistentData _persistentData;

    [Inject]
    public CurrentLevelChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public int Level { get; private set; }
    public float Experience { get; private set; }

    public void Visit(int id) =>
        throw new ArgumentException();

    public void Visit()
    {
        Level = _persistentData.PlayerData.GetCurrentLevel();

        Experience = _persistentData.PlayerData.GetCurrentExperience();
    }

}
