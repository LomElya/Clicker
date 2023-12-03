using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/ItemFactory")]
public class ItemFactory : ScriptableObject
{
    [SerializeField] private ItemsShopItem _shopItemConfig;
    [SerializeField] private List<ItemConfig> _itemConfigs;

    public IEnumerable<ItemConfig> ItemConfigs => _itemConfigs;

    public ItemsShopItem ShopItemConfig => _shopItemConfig;

    public string Name { get; private set; }
    public Sprite Image { get; private set; }

    private void OnValidate()
    {
        int id = 0;

        foreach (var config in _itemConfigs)
        {
            config.ID = id;
            id++;
        }
    }

    public ItemConfig Get(int id)
    {
        ItemConfig itemConfig = _itemConfigs.FirstOrDefault(x => x.ID == id);
        ShopItemConfig shopConfig = _shopItemConfig.Get(id);

        Name = _shopItemConfig.Name;
        Image = shopConfig.Image;

        return itemConfig;
    }
}
