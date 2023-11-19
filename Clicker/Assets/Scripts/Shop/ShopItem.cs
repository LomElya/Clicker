using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Item/ShopItem")]
public class ShopItem : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public ItemType TypeItem { get; private set; }

    [SerializeField] private List<ShopItemConfig> _shopItemConfig;

    public IEnumerable<ShopItemConfig> ShopItemConfig => _shopItemConfig;

    public int MaxCount => _shopItemConfig.Count;

    public ShopItemConfig Get(int id) => _shopItemConfig.FirstOrDefault(x => x.ID == id);
}
