using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseShopUI : MonoBehaviour
{
    [SerializeField] protected ShopDefinition shopDefinition;
    
    [SerializeField] Image portraitImage;
    [SerializeField] TMP_Text messageText;

    public void Setup(ShopDefinition shopDefinition)
    {
        portraitImage.sprite = shopDefinition.shopkeeperImage;
        messageText.text = shopDefinition.shopkeeperMessage;
    }
    
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}