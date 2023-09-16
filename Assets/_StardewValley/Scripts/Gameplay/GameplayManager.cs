using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    
    [SerializeField] ItemsManager itemsManager;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] CharacterView characterView;

    public Inventory playerInventory;
    public GameplayEvents GameplayEvents;
    
    private void Awake()
    {
        Instance = this;
        itemsManager.Initialize();
        GameplayEvents = new GameplayEvents();
        
        playerInventory = GetInventory();
        inventoryUI.Initialize(playerInventory);
        playerInventory.OnItemChanged += SaveInventoryData;
        GameplayEvents.Instance.OnItemUsed += OnItemUsed;
    }

    private void OnItemUsed(int slotIndex)
    {
        ItemInInventory item = playerInventory.itemAmount[slotIndex];
        if (itemsManager.TryGetItemById(item.itemID, out ItemDefinition itemDefinition))
        {
            if (itemDefinition is ItemEquipementDefinition itemEquipment)
            {
                characterView.SetEquipment(itemEquipment.equipementView);
            }
        }
    }

    private void SaveInventoryData(ItemInInventory itemInInventory, int slotIndex)
    {
        PlayerPrefs.SetString("Inventory", JsonUtility.ToJson(playerInventory));
    }

    private Inventory GetInventory()
    {
        string json = PlayerPrefs.GetString("Inventory", "");
        if (string.IsNullOrEmpty(json))
        {
            Inventory newInventory = new Inventory();
            newInventory.FillWithNullElements();
            return newInventory;
        }

        return JsonUtility.FromJson<Inventory>(json);
    }

}