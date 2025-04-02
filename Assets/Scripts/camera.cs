using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] Transform player;
    public float minDistance = 10;
    public Vector3 offset = new Vector3(0f, 2f, -5f);

    private void FixedUpdate()
    {
        Vector3 targetPosition = player.position + player.rotation * offset;

        Vector3 v = transform.position - targetPosition;


        if (v.magnitude < minDistance)
        {
            v.Normalize();
            v = v * minDistance;
            targetPosition = targetPosition + v;
        }

        /*
         * longitud = sqrt(v.x^2 + v.y^2 + v.z^2)
         * 
         * v: vector que va del target a la cámara
         * si |v| es mu chica:          |v| = longitud
         *      alargo v -> v'
         *      coloco la cámara en v' respecto del target
         */

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(player);
    }

}
