using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [SerializeField] InputsManager inputsManager;
    [SerializeField] Collider2D interactCollider;
    [SerializeField] PlayerDirection playerDirection;

    private List<Interactable> interactables;

    public event Action<Interactable> OnInteractableChanged;

    private void Awake()
    {
        interactables = new List<Interactable>();
    }

    private void Start()
    {
        inputsManager.OnInteractPressed += OnInteractPressedHandler;
        playerDirection.OnDirectionChanged += OnDirectionChangedHandler;
    }

    private void OnDirectionChangedHandler(DirectionType newDirection)
    {
        switch (newDirection)
        {
            case DirectionType.Up:
                interactCollider.transform.localPosition = Vector3.up * .5f;
                break;
            case DirectionType.Right:
                interactCollider.transform.localPosition = Vector3.right * .5f;
                break;
            case DirectionType.Down:
                interactCollider.transform.localPosition = Vector3.down * .5f;
                break;
            case DirectionType.Left:
                interactCollider.transform.localPosition = Vector3.left * .5f;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            interactables.Add(interactable);
            OnInteractableChanged?.Invoke(interactables.FirstOrDefault());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            interactables.Remove(interactable);
            OnInteractableChanged?.Invoke(interactables.FirstOrDefault());
        }
    }

    private void OnInteractPressedHandler()
    {
        Interactable interactable = interactables.FirstOrDefault();
        if (interactable != null)
        {
            interactable.Interact();
            OnInteractableChanged?.Invoke(interactable);
        }
    }
}
