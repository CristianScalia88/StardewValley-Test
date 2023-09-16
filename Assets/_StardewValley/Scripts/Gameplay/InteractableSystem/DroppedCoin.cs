using UnityEngine;

public class DroppedCoin : Interactable
{
    public int amount;
    
    const string PICK_UP = "Pick Up";

    protected override void OnInteract()
    {
        CurrencyManager.Instance.AddCoins(amount);
        Destroy(gameObject);
    }

    public override string Message => PICK_UP;
}
