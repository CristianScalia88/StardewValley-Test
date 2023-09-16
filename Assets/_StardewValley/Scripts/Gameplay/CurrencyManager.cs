using System;
using UnityEngine;

public class CurrencyManager
{
    public int TotalCoins
    {
        get => PlayerPrefs.GetInt("Coins", 20);
        set => PlayerPrefs.SetInt("Coins", value);
    }

    public event Action<int> OnCoinsAdded;
    public event Action<int> OnCoinsRemoved;

    public static CurrencyManager Instance;

    public CurrencyManager()
    {
        Instance = this;
    }
    
    public void AddCoins(int coins)
    {
        TotalCoins += coins;
        OnCoinsAdded?.Invoke(coins);
    }

    public void RemoveCoins(int coins)
    {
        if (!CanAfford(coins))
        {
            Debug.LogError("Attempted to remove more coins than available!");
            coins = TotalCoins;
        }
        TotalCoins -= coins;
        OnCoinsRemoved?.Invoke(coins);
    }

    public bool CanAfford(int coins)
    {
        return TotalCoins >= coins;
    }
    
    
}