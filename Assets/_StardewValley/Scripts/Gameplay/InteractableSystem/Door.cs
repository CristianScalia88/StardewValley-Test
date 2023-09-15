using UnityEngine;

public class Door : Interactable
{
    [SerializeField] Collider2D collider;
    [SerializeField] Animator animator;

    private bool isOpen;
    
    protected override void OnInteract()
    {
        isOpen = !isOpen;
        SetOpenState(isOpen);
    }

    public override string Message => isOpen ? "Close" : "Open";

    private void SetOpenState(bool isOpen)
    {
        collider.enabled = !isOpen;
        animator.SetBool("IsOpen", isOpen);
    }
}
