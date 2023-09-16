using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemSlotUI slotPrefab;
    [SerializeField] private Transform slotsContainer;
    
    private List<ItemSlotUI> slots;
    
    public void Initialize(Inventory inventory)
    {
        slots = new List<ItemSlotUI>();
        SetupInitialItems(inventory);
        inventory.OnItemAdded += SetupItem;
    }

    private void SetupInitialItems(Inventory inventory)
    {
        for (var i = 0; i < inventory.itemAmount.Length; i++)
        {
            ItemSlotUI itemSlot = Instantiate(slotPrefab, slotsContainer);
            slots.Add(itemSlot);
            itemSlot.Setup(i);
            if (inventory.HasItem(i))
            {
                itemSlot.SetItem(inventory.itemAmount[i]);
            }
            else
            {
                itemSlot.ClearSlot();
            }
        }
    }

    private void SetupItem(ItemInInventory item, int slotIndex)
    {
        slots[slotIndex].SetItem(item);
    }

}