using System;
using UnityEngine;

public class CurrencyManager
{
    const string COINS_KEY = "Coins";

    public int TotalCoins
    {
        get
        {
            return PlayerPrefs.GetInt(COINS_KEY, 0);
        }
        set => PlayerPrefs.SetInt(COINS_KEY, value);
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