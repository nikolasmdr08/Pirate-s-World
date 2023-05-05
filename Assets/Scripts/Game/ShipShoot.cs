using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float timeBetweenShoot = 0.5f;
    public float bulletLifeTime = 0.5f;
    public float bulletDirecction = 1f; 
    private float timeSinceLastShoot = 0f;
    public float offsetShoot = 0.25f;
    ShipMove player;

    private void Start()
    {
        player = FindObjectOfType<ShipMove>(); // obtengo referencia al player para conocer el valor de ataque
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > timeSinceLastShoot + timeBetweenShoot)
        {
            timeSinceLastShoot = Time.time;

            if(player.levelATK >= 3)
            {
                // Crear proyectil a la derecha
                InstanciateShoot(bulletDirecction, new Vector2(transform.position.x - offsetShoot, transform.position.y + offsetShoot));
                InstanciateShoot(bulletDirecction, transform.position);
                InstanciateShoot(bulletDirecction, new Vector2(transform.position.x - offsetShoot, transform.position.y - offsetShoot));
                // Crear proyectil a la izquierda
                InstanciateShoot(-bulletDirecction, new Vector2(transform.position.x + offsetShoot, transform.position.y + offsetShoot));
                InstanciateShoot(-bulletDirecction, transform.position);
                InstanciateShoot(-bulletDirecction, new Vector2(transform.position.x + offsetShoot, transform.position.y - offsetShoot));
            }
            else if(player.levelATK == 2)
            {
                // Crear proyectil a la derecha
                InstanciateShoot(bulletDirecction, new Vector2(transform.position.x, transform.position.y + offsetShoot));
                InstanciateShoot(bulletDirecction, new Vector2(transform.position.x, transform.position.y - offsetShoot));
                // Crear proyectil a la izquierda
                InstanciateShoot(-bulletDirecction, new Vector2(transform.position.x, transform.position.y + offsetShoot));
                InstanciateShoot(-bulletDirecction, new Vector2(transform.position.x, transform.position.y - offsetShoot));
            }
            else
            {
                // Crear proyectil a la derecha
                InstanciateShoot(bulletDirecction, transform.position);
                // Crear proyectil a la izquierda
                InstanciateShoot(-bulletDirecction, transform.position);
            }
        }
    }

    void InstanciateShoot(float direction, Vector2 position)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.transform.rotation = transform.rotation;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * direction * bulletSpeed;
    }
}
