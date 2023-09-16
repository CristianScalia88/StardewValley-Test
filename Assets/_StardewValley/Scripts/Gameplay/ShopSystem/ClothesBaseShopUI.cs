using UnityEngine;

public class ClothesBaseShopUI : BaseShopUI
{
    private delegate void OnSell(int slotIndex);
    
    [SerializeField] private ShopOptionUI buyOption;
    [SerializeField] private ShopOptionUI sellOption;
    [SerializeField] private ShopOptionUI leaveOption;
    [SerializeField] private GameObject itemsPanel;
    
    [Header("Items")]
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private ShopItemUI shopItemUIPrefab;
    
    private void Start()
    {
        buyOption.Setup("Buy", OnBuyPressed);
        sellOption.Setup("Sell", OnSellPressed);
        leaveOption.Setup("Leave", OnLeavePressed);
    }

    private void SellItem(int slotIndex)
    {
        PlayerInventory.RemoveItem(PlayerInventory.itemAmount[slotIndex]);
    }

    private void CreateItems(ItemInInventory[] playerInventoryItemAmount, OnSell onClickAction)
    {
        for (int i = 0; i < playerInventoryItemAmount.Length; i++)
        {
            ItemInInventory item = playerInventoryItemAmount[i];
            if (ItemsManager.Instance.TryGetItemById(item.itemID, out ItemDefinition itemDefinition))
            {
                ShopItemUI itemButton = Instantiate(shopItemUIPrefab, itemsContainer);
                int slotIndex = i;
                itemButton.Setup(itemDefinition.name, ()=> onClickAction?.Invoke(slotIndex));
                itemButton.SetupItem(itemDefinition);
            }
        }
    }

    private void ClearItems()
    {
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnLeavePressed()
    {
        Close();
    }

    private void OnSellPressed()
    {
        itemsPanel.gameObject.SetActive(true);
        ClearItems();
        CreateItems(PlayerInventory.itemAmount, SellItem);
        PlayerInventory.OnItemRemoved -= OnItemRemoved;
        PlayerInventory.OnItemRemoved += OnItemRemoved;
    }

    private void OnItemRemoved(ItemInInventory itemInInventory, int slotIndex)
    {
        OnSellPressed();
    }

    private void OnBuyPressed()
    {
        itemsPanel.gameObject.SetActive(true);
        ClearItems();
        CreateItems(ItemShopDefinition.items, BuyItem);
    }

    private void BuyItem(int slotIndex)
    {
        ItemInInventory itemToBuy = ItemShopDefinition.items[slotIndex];
        if (ItemsManager.Instance.TryGetItemById(itemToBuy.itemID, out var itemDef))
        {
            if (PlayerInventory.AddItem(new ItemInInventory(itemToBuy.itemID, itemToBuy.amount)))
            {
                Debug.Log("Item Added");
            }
        }
    }
    
    ItemsShopDefinition ItemShopDefinition => (ItemsShopDefinition) shopDefinition;
    Inventory PlayerInventory => GameplayManager.Instance.playerInventory;

}
