using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private Animator animator;

    const string COINS_ADDED_STATE = "CoinsAdded";
    const string COINS_REMOVED_STATE = "CoinsRemoved";

    private void Start()
    {
        UpdateCoinsText();
        CurrencyManager.Instance.OnCoinsAdded += OnCoinsAddedHandler;
        CurrencyManager.Instance.OnCoinsRemoved += OnCoinsRemovedHandler;
    }

    private void OnCoinsRemovedHandler(int amount)
    {
        UpdateCoinsText();
        animator.Play(COINS_REMOVED_STATE);
    }

    private void OnCoinsAddedHandler(int amount)
    {
        UpdateCoinsText();
        animator.Play(COINS_ADDED_STATE);
    }

    private void UpdateCoinsText()
    {
        coinsText.text = CurrencyManager.Instance.TotalCoins.ToString();
    }
}