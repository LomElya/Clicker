using UnityEngine;

[System.Serializable]
public class ShopItemConfig
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int OpenAtLevel { get; private set; }
}
