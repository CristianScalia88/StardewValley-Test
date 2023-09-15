using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputsManager inputsManager;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] private CharacterView view;

    private DirectionType directionType = DirectionType.Down;

    private void Start()
    {
        view.SetMovementDirection(directionType);
    }

    private void FixedUpdate()
    {
        Vector2 direction = inputsManager.Direction;

        UpdateDirection(direction);

        bool isMoving = IsMoving(direction);
        view.SetAnimationsEnabled(isMoving);
        if (!isMoving)
        {
            return;
        }

        Vector3 movement = new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(transform.position + movement);
    }

    private bool IsMoving(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            return true;
        }
        return false;
    }

    private void UpdateDirection(Vector2 direction)
    {
        DirectionType newDirectionType = GetDirectionType(direction, directionType);
        if (newDirectionType != directionType)
        {
            directionType = newDirectionType;
            view.SetMovementDirection(directionType);
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


