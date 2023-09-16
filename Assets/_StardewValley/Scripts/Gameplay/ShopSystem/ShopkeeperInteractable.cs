using UnityEngine;

public class ShopkeeperInteractable : Interactable
{
    [SerializeField] private BaseShopUI baseShopUI;
    const string OPEN_SHOP = "Open Shop";

    protected override void OnInteract()
    {
        baseShopUI.Open();
    }

    public override string Message
    {
        get
        {
            return OPEN_SHOP;
        }
    }
}
