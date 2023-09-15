using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputsManager inputsManager;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] float moveSpeed = 5.0f;
    
    void FixedUpdate()
    {
        Vector2 direction = inputsManager.Direction;
        if (direction == Vector2.zero)
            return;
        
        Vector3 movement = new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(transform.position + movement);
    }
}


