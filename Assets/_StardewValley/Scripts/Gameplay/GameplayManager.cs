using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    
    [SerializeField] ItemsManager itemsManager;
    [SerializeField] InventoryUI inventoryUI;

    public Inventory playerInventory;
    
    private void Awake()
    {
        Instance = this;
        itemsManager.Initialize();
        
        playerInventory = GetInventory();
        inventoryUI.Initialize(playerInventory);
        playerInventory.OnItemChanged += SaveInventoryData;
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