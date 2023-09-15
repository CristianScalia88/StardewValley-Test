using System;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    [SerializeField] InputsManager inputsManager;
    [SerializeField] private CharacterView view;

    public event Action<DirectionType> OnDirectionChanged;
    
    private DirectionType directionType = DirectionType.Down;

    private void Start()
    {
        view.SetMovementDirection(directionType);
    }

    private void Update()
    {
        Vector2 direction = inputsManager.Direction;
        UpdateDirection(direction);
    }
    
    private void UpdateDirection(Vector2 direction)
    {
        DirectionType newDirectionType = GetDirectionType(direction, directionType);
        if (newDirectionType != directionType)
        {
            directionType = newDirectionType;
            view.SetMovementDirection(directionType);
            OnDirectionChanged?.Invoke(newDirectionType);
        }
    }

    private DirectionType GetDirectionType(Vector2 direction, DirectionType fallbackDirection)
    {
        if (direction.x != 0)
        {
            return direction.x > 0 ? DirectionType.Right : DirectionType.Left;
        }

        if (direction.y != 0)
        {
            return direction.y > 0 ? DirectionType.Up : DirectionType.Down;
        }

        return fallbackDirection;
    }
}