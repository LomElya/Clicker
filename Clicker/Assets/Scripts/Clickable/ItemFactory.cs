using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/ItemFactory")]
public class ItemFactory : ScriptableObject
{
    [field: SerializeField] public List<ItemConfig> ItemConfigs { get; private set; }

    public ItemConfig Get(int id) => ItemConfigs.FirstOrDefault(x => x.ID == id);
}
