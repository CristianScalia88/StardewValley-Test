using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float lerpTime = 10;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        transform.position = target.position + offset;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * lerpTime);
    }
}
