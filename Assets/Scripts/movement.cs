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
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        // local.velocity.z = movimiento frontal o trasero local de objeto 

        if (inputBrake)
        {
            // HACIA DELANTE-> FRENAR
            if (localVelocity.z > 0)
            {
                Vector3 brakeForceMovement = -transform.forward * brakeForce * Time.deltaTime;
                rb.velocity += brakeForceMovement;

                // LA FUERZA DE FRENADO ES SUFICIENTE PARA EMPEZAR A IR HACIA ATRAS
                // SI LA SUMA DEL EJE DE VELOCIDAD QUE VA HACIA ATRAS/DELANTE MAS LA FUERZA DE FRENADO = A <0 SE PUEDE ECHAR MARCHA ATRAS
                if (localVelocity.z + brakeForceMovement.z < 0)
                {
                    rb.velocity = transform.TransformDirection(new Vector3(localVelocity.x, localVelocity.y, 0));
                }
            }
            else
            {
                // SI SE ESTA LLENDO HACIA ATRAS SE ACELERA HACIA ATRA
                Vector3 reverseMovement = -transform.forward * speed * Time.deltaTime;
                rb.velocity += reverseMovement;

                // LIMITAR VELOCIADAD HACIA ATRAS
                if (localVelocity.z < -maxReverseSpeed)
                {
                    rb.velocity = transform.TransformDirection(new Vector3(localVelocity.x, localVelocity.y, -maxReverseSpeed));
                }
            }
        }
    }
}
