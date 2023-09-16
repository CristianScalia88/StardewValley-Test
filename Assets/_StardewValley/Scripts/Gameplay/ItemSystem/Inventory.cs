using System;

[System.Serializable]
public class Inventory
{
    public const int MAX_SLOTS = 10;
    public ItemInInventory[] itemAmount = new ItemInInventory[MAX_SLOTS];

    public event Action<ItemInInventory, int> OnItemAdded;
    public event Action<ItemInInventory, int> OnItemRemoved;


    public static readonly ItemInInventory NULL_ITEM = new ItemInInventory(-1, -1);

    public void FillWithNullElements()
    {
        for (int i = 0; i < itemAmount.Length; i++)
        {
            itemAmount[i] = NULL_ITEM;
        }
    }

    private bool GetEmptySlotIndex(out int emptySlotIndex)
    {
        emptySlotIndex = -1;
        for (int i = 0; i < MAX_SLOTS; i++)
        {
            if (!HasItem(i))
            {
                emptySlotIndex = i;
                return true;
            }
        }

        return false;
    }

    public bool AddItem(ItemInInventory item)
    {
        if (GetEmptySlotIndex(out int emptySlotIndex))
        {
            itemAmount[emptySlotIndex] = item;
            OnItemAdded?.Invoke(item, emptySlotIndex);
            return true;
        }

        return false;
    }

    public bool RemoveItem(ItemInInventory item)
    {
        for (int i = 0; i < MAX_SLOTS; i++)
        {
            if (itemAmount[i] == item)
            {
                ItemInInventory removedItem = itemAmount[i];
                itemAmount[i] = NULL_ITEM;
                OnItemRemoved?.Invoke(removedItem, i);
                return true;
            }
        }

        return false;
    }

    public bool HasItem(int slotIndex)
    {
        return itemAmount[slotIndex].itemID != NULL_ITEM.itemID;
    }
}