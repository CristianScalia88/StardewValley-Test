using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    
    [SerializeField] ItemsManager itemsManager;
    [SerializeField] InventoryUI inventoryUI;

    public Inventory playerInventory;
    
    private GameplayEvents GameplayEvents;
    private CurrencyManager currencyManager;
    
    const string INVENTORY_KEY = "Inventory";

    private void Awake()
    {
        Instance = this;
        itemsManager.Initialize();
        GameplayEvents = new GameplayEvents();
        currencyManager = new CurrencyManager();
        
        playerInventory = GetInventory();
        inventoryUI.Initialize(playerInventory);
        playerInventory.OnItemChanged += SaveInventoryData;
    }

    private void SaveInventoryData(ItemInInventory itemInInventory, int slotIndex)
    {
        PlayerPrefs.SetString(INVENTORY_KEY, JsonUtility.ToJson(playerInventory));
    }

    private Inventory GetInventory()
    {
        string json = PlayerPrefs.GetString(INVENTORY_KEY, "");
        if (string.IsNullOrEmpty(json))
        {
            Inventory newInventory = new Inventory();
            newInventory.FillWithNullElements();
            return newInventory;
        }

        return JsonUtility.FromJson<Inventory>(json);
    }

}