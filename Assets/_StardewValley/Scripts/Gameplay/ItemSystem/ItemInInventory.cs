[System.Serializable]
public class ItemInInventory
{
    public int itemID = -1;
    public int amount = -1;

    public ItemInInventory(int itemID, int amount)
    {
        this.itemID = itemID;
        this.amount = amount;
    }
}