using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] int slotIndex;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text amountText;

    public void Setup(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    public void SetItem(ItemInInventory itemInInventory)
    {
        if (itemInInventory == null)
        {
            ClearSlot();
            return;
        }

        itemImage.enabled = true;
        if (ItemsManager.Instance.TryGetItemById(itemInInventory.itemID, out ItemDefinition itemDefinition))
        {
            itemImage.sprite = itemDefinition.sprite;
        }

        string amountString = itemInInventory.amount > 1 ? itemInInventory.amount.ToString() : string.Empty;
        amountText.text = amountString;
    }

    public void ClearSlot()
    {
        itemImage.sprite = null;
        itemImage.enabled = false;
        amountText.text = string.Empty;
    }
}
