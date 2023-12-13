using System;
using Zenject;

public class Wallet
{
    public event Action<int> CoinsChanged;
    public event Action<int> AddCoins;

    private IPersistentData _persistentData;

  /*   [Inject]
    private IncomeVisualizer _incomeVisualizer; */

    [Inject]
    public Wallet(IPersistentData persistentData ) =>
        _persistentData = persistentData;

    public void AddCoin(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        if (coins <= 0)
            return;

        _persistentData.PlayerData.Money += coins;

       // _incomeVisualizer.Visualize(coins.ToString());

        AddCoins?.Invoke(coins);
        CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
    }

    public int GetCurrentCoins() => _persistentData.PlayerData.Money;

    public bool IsEnough(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        return _persistentData.PlayerData.Money >= coins;
    }

    public void Spend(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _persistentData.PlayerData.Money -= coins;

        CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
    }
}
