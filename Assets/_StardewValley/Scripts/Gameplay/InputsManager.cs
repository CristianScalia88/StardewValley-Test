using System;
using UnityEngine;

public class InputsManager : MonoBehaviour
{
    private GameplayInputMap gameplayInputMap;
    
    public Vector2 Direction => gameplayInputMap.Player.Movement.ReadValue<Vector2>();
    public event Action OnInteractPressed;
    
    private void Awake()
    {
        gameplayInputMap = new GameplayInputMap();
        gameplayInputMap.Enable();
        gameplayInputMap.Player.Interact.performed += (c)=> OnInteractPressed?.Invoke();
    }
   
}
