using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerData
{
    private List<ItemData> _itemData;

    private int _money;

    private LevelData _level;

    public PlayerData()
    {
        _money = 3000;

        _level = new LevelData();

        ItemType type = ItemType.Cursor;
        _itemData = new List<ItemData>() { new ItemData(type) };
        //_itemData = new List<ItemData>() { };

        /*  type = ItemType.AutoClick;
         _itemData.Add(new ItemData(type)); */

        type = ItemType.Clickable;
        _itemData.Add(new ItemData(type));
    }

    [JsonConstructor]
    public PlayerData(int money,
       List<ItemData> itemData, LevelData level)
    {
        Money = money;

        _level = level;

        _itemData = itemData;
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

    public IEnumerable<ItemData> ItemData => _itemData;

    public LevelData Level => _level;

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

    public int GetCurrentClickableItem()
    {

        ItemData item = _itemData.Find(type => type.TypeItem == ItemType.Clickable);

        if (item == null)
            return 0;

        return item.CurrentID;
    }

    public int ChangeItemID(ItemType itemType)
    {
        ItemData item = _itemData.Find(type => type.TypeItem == itemType);

        if (!_itemData.Contains(item))
            return 0;

        return item.ChangeItem();
    }

    public int CountItem(ItemType itemType, int addCount)
    {
        ItemData item = _itemData.Find(type => type.TypeItem == itemType);

        if (!_itemData.Contains(item))
        {
            _itemData.Add(new ItemData(itemType));
            int count = CountItem(itemType, 1);
            return count;
        }

        return item.AddItem(addCount);
    }

    public int CurrentIDItem(ItemType itemType)
    {
        ItemData item = _itemData.Find(type => type.TypeItem == itemType);

        if (!_itemData.Contains(item))
            return -1;

        return item.CurrentID;
    }

    public int QuantityItem(ItemType itemType)
    {
        ItemData item = _itemData.Find(type => type.TypeItem == itemType);

        if (!_itemData.Contains(item))
            return 0;

        return item.Count;
    }

    public bool IsOpenItem(ItemType itemType)
    {
        ItemData item = _itemData.Find(type => type.TypeItem == itemType);

        if (!_itemData.Contains(item))
            return false;

        return true;
    }

    public void OpenItem(ItemType itemType) =>
        _itemData.Add(new ItemData(itemType));
}



