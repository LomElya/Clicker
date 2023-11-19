using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerData
{
    private int _idSelectedClickableItem;

    private List<ItemData> _itemData;

    private int _money;

    private LevelData _level;

    public PlayerData()
    {
        _money = 10000;

        IDdSelectedClickableItem = 0;

        _level = new LevelData();

        ItemType type = ItemType.Cursor;
        _itemData = new List<ItemData>() { new ItemData(type) };
        //_itemData = new List<ItemData>() { };

        type = ItemType.AutoClick;
        _itemData.Add(new ItemData(type));
    }

    [JsonConstructor]
    public PlayerData(int money, int idSelectedClickableItem,
       List<ItemData> itemsData, LevelData level)
    {
        Money = money;

        IDdSelectedClickableItem = idSelectedClickableItem;

        _level = level;

        _itemData = itemsData;
    }

    public int Money
    {
        get => _money;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _money = value;
        }
    }

    public int IDdSelectedClickableItem
    {
        get => _idSelectedClickableItem;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _idSelectedClickableItem = value;
        }
    }

    public IEnumerable<ItemData> ItemData => _itemData;

    public int GetCurrentLevel() =>
        _level.CurrentLevel;

    public float GetCurrentExperience() =>
        _level.Experience;

    public void LevelUp() =>
        _level.LevelUP();

    public void AddExperience(float experience) =>
        _level.AddExperience(experience);

    public int GetCurrentIncome()
    {
        ItemData item = _itemData.Find(type => type.TypeItem == ItemType.AutoClick);

        if (item == null)
            return 0;

        return item.Count;
    }

    public int CountItem(ItemType itemType)
    {
        var item = _itemData.Find(type => type.TypeItem == itemType);

        if (!_itemData.Contains(item))
            throw new ArgumentException(nameof(item));

        return item.AddItem();
    }

    public int QuantityItem(ItemType itemType)
    {
        var item = _itemData.Find(type => type.TypeItem == itemType);

        if (!_itemData.Contains(item))
            return 0;

        return item.Count;
    }

    public void UnlockItem(ItemType itemType)
    {
        var item = _itemData.Find(type => type.TypeItem == itemType);

        if (_itemData.Contains(item))
            throw new ArgumentException(nameof(itemType));

        _itemData.Add(new ItemData(itemType));
        //item.AddItem();
    }

    public bool OppenedItem(ItemType itemType)
    {
        var item = _itemData.Find(type => type.TypeItem == itemType);

        if (!_itemData.Contains(item))
            return false;

        return true;
    }
}



