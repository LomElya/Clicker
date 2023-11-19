using System;
using Newtonsoft.Json;

public class ItemData
{
    public ItemType TypeItem { get; private set; }
    private int _count = 0;

    public ItemData(ItemType typeItem) => TypeItem = typeItem;

    [JsonConstructor]
    public ItemData(ItemType typeItem, int count)
    {
        Count = count;
        TypeItem = typeItem;
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

    public int AddItem() => ++Count;

    public int RemoveItem()
    {
        if (Count - 1 < 0)
            return 0;

        return --Count;
    }
}