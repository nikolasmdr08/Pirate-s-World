using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float maxspeed = 10f;
    public float aceleracion = 3f;
    public float addSpeed = 0f;
    public float currentSpeed = 0f;
    public float rotation = 100f;

    public int levelATK = 1;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Movimiento hacia adelante
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += (aceleracion + addSpeed) * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxspeed);
        }
        else
        {
            currentSpeed -= (aceleracion/2) * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxspeed);
        }

        // Rotación
        float rotationZ = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            rotationZ = rotation;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationZ = -rotation;
        }

        // Aplicar movimiento y rotación al Rigidbody2d
        Vector2 move = transform.up * currentSpeed;
        float rotationEuler = rb.rotation + rotationZ * Time.deltaTime;
        rb.MoveRotation(rotationEuler);
        rb.velocity = move;
    }

    public int GetAtackPoints()
    {
        return levelATK;
    }

    public float GetSpeedPoints()
    {
        return addSpeed;
    }

    public void AddATK()
    {
        levelATK++;
    }
}
