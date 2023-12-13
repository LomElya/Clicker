using UnityEngine;

[System.Serializable]
public class UpdatesShopItemConfig : ShopItemConfig
{
    [field: SerializeField] public int AddCount { get; private set; }
}
