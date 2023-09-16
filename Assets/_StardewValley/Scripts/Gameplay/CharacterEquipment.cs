using System;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] CharacterView characterView;
    
    private int clothsEquipedSlotIndex = -1;

    private void Awake()
    {
        GameplayEvents.Instance.OnItemUsed += OnItemUsed;
        PlayerInventory.OnItemRemoved += OnItemRemoved;
    }

    private void OnItemRemoved(ItemInInventory item, int slotIndex)
    {
        if (clothsEquipedSlotIndex == slotIndex)
            characterView.SetDefaultEquipment(EquipmentType.Cloths);
    }

    private void OnItemUsed(int slotIndex)
    {
        ItemInInventory item = PlayerInventory.itemAmount[slotIndex];
        if (ItemsManager.Instance.TryGetItemById(item.itemID, out ItemDefinition itemDefinition))
        {
            if (itemDefinition is ItemEquipementDefinition itemEquipment)
            {
                characterView.SetEquipment(itemEquipment.equipementView);
                switch (itemEquipment.equipementView.type)
                {
                    case EquipmentType.Cloths:
                        clothsEquipedSlotIndex = slotIndex;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    private Inventory PlayerInventory => GameplayManager.Instance.playerInventory;
}