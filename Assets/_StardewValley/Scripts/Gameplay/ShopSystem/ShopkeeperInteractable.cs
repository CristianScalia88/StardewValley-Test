using UnityEngine;

public class ShopkeeperInteractable : Interactable
{
    [SerializeField] private BaseShopUI baseShopUI;
    
    protected override void OnInteract()
    {
        baseShopUI.Open();
    }

    public override string Message => "Open Shop";
}
