using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.U2D;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public float maxSpeed = 180;
    public float turnSpeed = 200;
    public float brakeForce;
    public float maxReverseSpeed = 30;
    public float handbrakeForce;
    public float rotationInput = 2;

    public bool inputAccelerate;
    public bool inputTurnLeft;
    public bool inputTurnRight;
    public bool inputBrake;
    public bool inputHandbrake;

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
        inputHandbrake = Input.GetKey(KeyCode.Space);
        if (inputTurnLeft) rotationInput = -1;
        else if (inputTurnRight) rotationInput = 1;
        else rotationInput = 0;
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

        //FRENO Y MARCHA ATRAS
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        // local.velocity.z = movimiento frontal o trasero local de objeto 

        if (inputBrake)
        {
            // HACIA DELANTE -> FRENAR
            if (localVelocity.z > 0)
            {
                rb.velocity = rb.velocity / brakeForce;

                if (localVelocity.z < 0)
                {
                    rb.velocity = transform.TransformDirection(new Vector3(localVelocity.x, localVelocity.y, 0));
                }
            }
            else
            {
                // SI SE ESTA YENDO HACIA ATRÁS, ACELERAR HACIA ATRÁS
                Vector3 reverseMovement = -transform.forward * speed * Time.deltaTime;
                rb.velocity += reverseMovement;

                // MAX SPEED (HACIA ATRAS)
                if (localVelocity.z < -maxReverseSpeed)
                {
                    rb.velocity = transform.TransformDirection(new Vector3(localVelocity.x, localVelocity.y, -maxReverseSpeed));
                }
            }
        }
    }
}


