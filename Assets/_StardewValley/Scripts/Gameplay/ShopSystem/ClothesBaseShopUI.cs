using System.Collections;
using TMPro;
using UnityEngine;

public class ClothesBaseShopUI : BaseShopUI
{
    private delegate void OnSell(int slotIndex);
    
    [SerializeField] private ShopOptionUI buyOption;
    [SerializeField] private ShopOptionUI sellOption;
    [SerializeField] private ShopOptionUI leaveOption;
    [SerializeField] private GameObject itemsPanel;
    [SerializeField] private TMP_Text panelTitle;

    [Header("Items")]
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private ShopItemUI shopItemUIPrefab;

    private Coroutine temporalMessageCoroutine;
    private Color startMessageColor;

    const string BUY = "Buy";
    const string SELL = "Sell";
    const string LEAVE = "Leave";
    const string NO_COINS = "Insufficient funds, try again later.";
    const string NO_SPACE = "Inventory full, make some room.";

    private void Start()
    {
        startMessageColor = messageText.color;
        buyOption.Setup(BUY, OmBuyPanelOptionPressed);
        sellOption.Setup(SELL, OnSellPanelOptionPressed);
        leaveOption.Setup(LEAVE, OnLeavePressed);
    }

    private void SellItem(int slotIndex)
    {
        ItemInInventory item = PlayerInventory.itemAmount[slotIndex];
        if (ItemsManager.Instance.TryGetItemById(item.itemID, out ItemDefinition itemDef))
        {
            CurrencyManager.Instance.AddCoins(itemDef.price);
        }
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
        itemsPanel.gameObject.SetActive(false);
        Close();
        buyOption.OnPointerExit(null);
        sellOption.OnPointerExit(null);
        leaveOption.OnPointerExit(null);
    }

    private void OnSellPanelOptionPressed()
    {
        panelTitle.text = SELL;
        itemsPanel.gameObject.SetActive(true);
        ClearItems();
        CreateItems(PlayerInventory.itemAmount, SellItem);
        PlayerInventory.OnItemRemoved -= OnItemRemoved;
        PlayerInventory.OnItemRemoved += OnItemRemoved;
    }

    private void OnItemRemoved(ItemInInventory itemInInventory, int slotIndex)
    {
        OnSellPanelOptionPressed();
    }

    private void OmBuyPanelOptionPressed()
    {
        panelTitle.text = BUY;
        itemsPanel.gameObject.SetActive(true);
        ClearItems();
        CreateItems(ItemShopDefinition.items, BuyItem);
    }

    private void BuyItem(int slotIndex)
    {
        ItemInInventory itemToBuy = ItemShopDefinition.items[slotIndex];
        if (ItemsManager.Instance.TryGetItemById(itemToBuy.itemID, out var itemDef))
        {
            if (!CurrencyManager.Instance.CanAfford(itemDef.price))
            {
                ShowTemporalMessage(NO_COINS);
                return;
            }
            if (!PlayerInventory.AddItem(new ItemInInventory(itemToBuy.itemID, itemToBuy.amount)))
            {
                ShowTemporalMessage(NO_SPACE);
                return;
            }
            CurrencyManager.Instance.RemoveCoins(itemDef.price);
        }
    }

    private void ShowTemporalMessage(string message)
    {
        if(temporalMessageCoroutine != null)
            StopCoroutine(temporalMessageCoroutine);
        
        temporalMessageCoroutine = StartCoroutine(ThrowTemporalMessageCoroutine(message));
    }

    private IEnumerator ThrowTemporalMessageCoroutine(string message)
    {
        messageText.color = Color.red;
        messageText.text = message;
        yield return new WaitForSeconds(5);
        messageText.text = ItemShopDefinition.shopkeeperMessage;
        messageText.color = startMessageColor;
    }

    ItemsShopDefinition ItemShopDefinition => (ItemsShopDefinition) shopDefinition;
    Inventory PlayerInventory => GameplayManager.Instance.playerInventory;

}
