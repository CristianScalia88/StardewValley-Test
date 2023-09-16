using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : ShopOptionUI
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text priceText;

    public void SetupItem(ItemDefinition itemDefinition)
    {
        itemImage.sprite = itemDefinition.sprite;
        priceText.text = itemDefinition.price.ToString();
    }
}
