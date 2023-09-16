using UnityEngine;

public class KeyboardCheats : MonoBehaviour
{
    #if UNITY_EDITOR
    
    [SerializeField] ItemDefinition itemDef;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CurrencyManager.Instance.AddCoins(100);
        }
    }
    #endif
}