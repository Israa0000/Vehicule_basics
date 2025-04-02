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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputAccelerate = Input.GetKey(KeyCode.W);
        inputTurnLeft = Input.GetKey(KeyCode.A);
        inputTurnRight = Input.GetKey(KeyCode.D);
        inputBrake = Input.GetKey(KeyCode.S);
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        bool isMovingForward = Vector3.Dot(rb.velocity, transform.forward) > 0;

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

        if (inputBrake && !isMovingForward)
        {
            if (rb.velocity.magnitude > 0)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeForce * Time.deltaTime);
            }
        }

        if (inputBrake && isMovingForward)
        {
            Vector3 backward_movement = -transform.forward * speed * Time.deltaTime;
            rb.velocity += backward_movement;

            if (rb.velocity.magnitude >= maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }
}