using System;
using UnityEngine;
using Zenject;

public class ClickableItem
{
    public event Action<int> ItemChanged;

    private IPersistentData _persistentData;

    [Inject]
    public ClickableItem(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void ChangedItem() =>
        ItemChanged?.Invoke(GetCurrentItem());

    public int GetCurrentItem() => _persistentData.PlayerData.GetCurrentClickableItem();
}
