using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject equipTutorial;
    [SerializeField] private GameObject movementTutorial;

    private void Start()
    {
        movementTutorial.SetActive(true);
        GameplayManager.Instance.playerInventory.OnItemAdded += OnItemAddedHandler;
    }

    private void OnItemAddedHandler(ItemInInventory item, int slot)
    {
        StartCoroutine(WaitForPlayerInputControlIsEnabled());
    }

    private IEnumerator WaitForPlayerInputControlIsEnabled()
    {
        yield return new WaitUntil(() => InputsManager.Instance.IsEnabled);
        equipTutorial.SetActive(true);
    }
}