using System;
using Newtonsoft.Json;

public class ItemData
{
    public ItemType TypeItem { get; private set; }

    private int _currentID = 0;
    private int _count = 1;

    public ItemData(ItemType typeItem) => TypeItem = typeItem;

    [JsonConstructor]
    public ItemData(ItemType typeItem, int currentID, int count)
    {
        CurrentID = currentID;
        Count = count;
        TypeItem = typeItem;
    }

    public int CurrentID
    {
        get => _currentID;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _currentID = value;
        }
    }

    public int Count
    {
        get => _count;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _count = value;
        }
    }

    public int AddItem(int addCount) => Count += addCount;
    public int ChangeItem() => ++CurrentID;
}