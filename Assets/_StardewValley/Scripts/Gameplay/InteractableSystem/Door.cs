using UnityEngine;

public class Door : Interactable
{
    [SerializeField] Collider2D collider;
    [SerializeField] Animator animator;

    private bool isOpen;
    
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    private const string CLOSE = "Close";
    private const string OPEN = "Open";
    
    protected override void OnInteract()
    {
        isOpen = !isOpen;
        SetOpenState(isOpen);
    }

    public override string Message
    {
        get
        {
            
            return isOpen ? CLOSE : OPEN;
        }
    }

    private void SetOpenState(bool isOpen)
    {
        collider.enabled = !isOpen;
        animator.SetBool(IsOpen, isOpen);
    }
}
