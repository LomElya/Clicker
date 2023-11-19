using System;
using Zenject;

public class Income
{
    public event Action<int> IncomeChanged;

    private IPersistentData _persistentData;

    [Inject]
    public Income(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void ChangeIncome() =>
        IncomeChanged.Invoke(GetCurrentIncome());

    public int GetCurrentIncome() => _persistentData.PlayerData.GetCurrentIncome();
}
