using UnityEngine;

[System.Serializable]
public class ShopItemConfig
{
    private int _id;

    public int ID
    {
        get => _id;
        set
        {
            _id = value;
        }
    }

    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int OpenAtLevel { get; private set; }
}
