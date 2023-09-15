using TMPro;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] Interacter interacter;
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text interactableMessageText;
    
    void Start()
    {
        interacter.OnInteractableChanged += OnInteractChanged;
    }

    private void OnInteractChanged(Interactable obj)
    {
        if (obj == null)
        {
            animator.SetBool("IsOpen", false);
            return;
        }
        animator.SetBool("IsOpen", true);
        interactableMessageText.text = obj.Message;
    }

}
