using TMPro;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] Interacter interacter;
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text interactableMessageText;
    
    const string IS_OPEN = "IsOpen";

    void Start()
    {
        interacter.OnInteractableChanged += OnInteractChanged;
    }

    private void OnInteractChanged(Interactable obj)
    {
        if (obj == null)
        {
            animator.SetBool(IS_OPEN, false);
            return;
        }
        animator.SetBool(IS_OPEN, true);
        interactableMessageText.text = obj.Message;
    }

}
