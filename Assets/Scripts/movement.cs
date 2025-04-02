using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public float maxSpeed = 180;
    public float turnSpeed = 200;
    public float brakeForce = 10;

    public bool inputAccelerate;
    public bool inputTurnLeft;
    public bool inputTurnRight;
    public bool inputBrake;
    public bool inputReverse;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputAccelerate = Input.GetKey(KeyCode.W);
        inputTurnLeft = Input.GetKey(KeyCode.A);
        inputTurnRight = Input.GetKey(KeyCode.D);
}
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        //ACELERAR
        if (inputAccelerate)
        {
            Vector3 forward_movement = transform.forward * speed * Time.deltaTime;
            rb.velocity += forward_movement;

            if (rb.velocity.magnitude >= maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }

        //GIRAR

        if (inputTurnLeft)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, -turnSpeed * Time.deltaTime, 0));
        }

        if (inputTurnRight)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turnSpeed * Time.deltaTime, 0));
        }

        //
    }

}

