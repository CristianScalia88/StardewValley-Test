using System;

public class GameplayEvents
{
    public static GameplayEvents Instance;
    
    public delegate void OnItemUsedEvent(int slotIndex);
    public OnItemUsedEvent OnItemUsed;


    public GameplayEvents()
    {
        Instance = this;
    }
}