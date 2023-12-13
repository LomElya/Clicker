using UnityEngine;

[System.Serializable]
public class ItemsShopItemConfig : ShopItemConfig
{
    [field: SerializeField] public string Name { get; private set; }
}
