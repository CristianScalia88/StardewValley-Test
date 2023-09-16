using UnityEngine;

public class KeyboardCheats : MonoBehaviour
{
    #if UNITY_EDITOR
    
    [SerializeField] ItemDefinition itemDef;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameplayManager.Instance.playerInventory.AddItem(new ItemInInventory(itemDef.id, 10));
        }
    }
    #endif
}