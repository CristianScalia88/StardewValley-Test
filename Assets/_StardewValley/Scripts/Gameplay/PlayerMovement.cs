using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputsManager inputsManager;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] private CharacterView view;

    private void FixedUpdate()
    {
        Vector2 direction = inputsManager.Direction;

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

}


