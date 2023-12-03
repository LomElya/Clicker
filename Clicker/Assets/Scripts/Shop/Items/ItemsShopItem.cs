using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

[CreateAssetMenu(fileName = "ItemsShopItem", menuName = "Shop/ItemsShopItem")]
public class ItemsShopItem : ShopItem
{
    [SerializeField] private List<ItemsShopItemConfig> _shopItemConfig;

    private string _name;
    private int _addCount = 1;

    public override IEnumerable<ShopItemConfig> ShopItemConfig => _shopItemConfig;

    public override string Name => _name;

    public override int MaxCount => _shopItemConfig.Count;

    public override int AddCount => _addCount;

    private void OnValidate()
    {
        int id = 0;

        foreach (var config in _shopItemConfig)
        {
            config.ID = id;
            id++;
        }
    }

    public override ShopItemConfig Get(int id)
    {
        var config = _shopItemConfig.FirstOrDefault(x => x.ID == id);

        _name = config.Name;

        return config;
    }

}
