using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("Camera Settings")]
    public float distanceBehind = 6f;
    public float heightAbove = 3f;
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {

        // CALCULAR POSICION
        Vector3 targetPosition = player.position
                               - player.forward * distanceBehind
                               + Vector3.up * heightAbove;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
