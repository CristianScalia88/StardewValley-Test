using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemSlotUI slotPrefab;
    [SerializeField] private Transform slotsContainer;
    
    private List<ItemSlotUI> slots;
    private Inventory inventory;
    
    public void Initialize(Inventory inventory)
    {
        this.inventory = inventory;
        slots = new List<ItemSlotUI>();
        InitializeSlots(inventory);
        UpdateItems(inventory);
        inventory.OnItemChanged += SetupItem;
    }

    private void InitializeSlots(Inventory inventory)
    {
        for (var i = 0; i < inventory.itemAmount.Length; i++)
        {
            ItemSlotUI itemSlot = Instantiate(slotPrefab, slotsContainer);
            ItemInInventory item = inventory.itemAmount[i];
            itemSlot.Setup(i);
            slots.Add(itemSlot);
        }
    }

    private void UpdateItems(Inventory inventory)
    {
        for (var i = 0; i < inventory.itemAmount.Length; i++)
        {
            ItemSlotUI itemSlot = slots[i];
            if (inventory.HasItemAt(i))
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
        UpdateItems(inventory);
    }

}