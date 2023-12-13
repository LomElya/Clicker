using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public ItemType TypeItem { get; private set; }

    public abstract string Name { get; }

    public abstract IEnumerable<ShopItemConfig> ShopItemConfig { get; }

    public abstract int AddCount { get; }
    public abstract int MaxCount { get; }

    public abstract ShopItemConfig Get(int id);
}
