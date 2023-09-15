using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        OnInteract();
    }

    protected abstract void OnInteract();
    public abstract string Message { get; }
}
