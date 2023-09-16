using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class InputsManager : MonoBehaviour
{
    public static InputsManager Instance;
    public Vector2 Direction => gameplayInputMap.Player.Movement.ReadValue<Vector2>();
    public event Action OnInteractPressed;

    private HashSet<Object> inputBlockers;
    private GameplayInputMap gameplayInputMap;
    
    private void Awake()
    {
        Instance = this;
        inputBlockers = new HashSet<object>();
        gameplayInputMap = new GameplayInputMap();
        gameplayInputMap.Enable();
        gameplayInputMap.Player.Interact.performed += (c)=> OnInteractPressed?.Invoke();
    }

    public void AddInputBlocker(Object blocker)
    {
        inputBlockers.Add(blocker);
        UpdateInputState();
    }
    
    public void RemoveInputBlocker(Object blocker)
    {
        inputBlockers.Remove(blocker);
        UpdateInputState();
    }

    private void UpdateInputState()
    {
        if(inputBlockers.Any())
            gameplayInputMap.Disable();
        else
            gameplayInputMap.Enable();
    }
}
