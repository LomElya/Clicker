using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ItemConfig
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

    [field: SerializeField] public int Multiplier { get; private set; }
}
