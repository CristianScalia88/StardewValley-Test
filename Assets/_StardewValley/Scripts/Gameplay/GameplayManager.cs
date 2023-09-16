using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    
    [SerializeField] ItemsManager itemsManager;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] GameObject equipItemTutorial;

    public Inventory playerInventory;
    
    private GameplayEvents GameplayEvents;
    private CurrencyManager currencyManager;
    
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