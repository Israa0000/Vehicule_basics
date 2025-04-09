using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset = new Vector3(0f, 2f, -3f);
    public float smoothTime = 0.3f;
    public float minDistance = 10;

    private void FixedUpdate()
    {
        Vector3 cameraToPlayer = transform.position - player.position;
        Vector3 cameraToPlayerNormalized = cameraToPlayer.normalized;

        Vector3 targetPosition = player.position + cameraToPlayerNormalized * minDistance + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(player);
    }
}
