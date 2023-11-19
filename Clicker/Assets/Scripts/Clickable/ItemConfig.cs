using UnityEngine;

[System.Serializable]
public class ItemConfig
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, Range(0, 100000)] public int Price { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Multiplier { get; private set; }

}
