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

        print(rb.velocity.magnitude);
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
        if (inputBrake)
        {

           /* if (rb.velocity.magnitude > 0)
            {
                Vector3 brakeForceMovement = rb.velocity.normalized * -brakeForce * Time.deltaTime;
                rb.velocity += brakeForceMovement;

            }*/

            rb.velocity = transform.InverseTransformDirection(Vector3.back);
            rb.velocity = rb.velocity * speed * Time.deltaTime;


            /*
             * si pulso abajo:
             *      si estoy yendo palante:
             *          reduzco el velocity hacia 0 0 0
             *      si no:
             *          acelero patrás
             *          
             */
        }
    }
}
