using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UpdatesShopItem", menuName = "Shop/UpdatesShopItem")]
public class UpdatesShopItem : ShopItem
{
    [field: SerializeField] private string _name;

    [SerializeField] private List<UpdatesShopItemConfig> _shopItemConfig;

    private int _addCount;

    public override string Name => _name;
    public override IEnumerable<ShopItemConfig> ShopItemConfig => _shopItemConfig;

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

        _addCount = config.AddCount;

        return config;
    }
}
