using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ItemsManager : ScriptableObject
{
    public static ItemsManager Instance;
    public List<ItemDefinition> allItems;

    private Dictionary<int, ItemDefinition> itemMap;

    public void Initialize()
    {
        Instance = this;
        itemMap = allItems.ToDictionary(item => item.id);
    }
    
    public bool TryGetItemById(int id, out ItemDefinition itemDefinition)
    {
        return itemMap.TryGetValue(id, out itemDefinition);
    }
}