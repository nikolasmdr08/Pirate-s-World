using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemyController : MonoBehaviour
{
    public float minDistance = 10f;
    public float velocity = 2f;
    private Rigidbody2D rb2D;
    private GameObject player;
    private LifeManager playerLifeManager;

    //movimientos aleatorios
    private float timeSinceLastChange;
    private float timebetweenChanges = 3f;
    public float obstacleDetectionDistance;

    public LayerMask layerHit;

    private bool onShooting = false;
    public float timeBetweenShoots;
    private float currentTimeShoot;
    public GameObject bulletPrefab; 
    public float bulletSpeed = 5f;
    public int levelATK;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("ShipPrefab");
        playerLifeManager = player.GetComponent<LifeManager>();
        levelATK = 1;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 enemyPosition = rb2D.position; //pos enemigo
            Vector2 playerPosition = player.transform.position; //Pos player

            float distance = Vector2.Distance(enemyPosition, playerPosition); //distancia actual

            if (distance < minDistance && playerLifeManager.IsAlive())
            {
                if(distance > 2f)
                {
                    Debug.LogWarning("Detecto player");
                    MoveTowardsTarget(playerPosition, obstacleDetectionDistance);
                }
                else
                {   
                    RotateTo(player.transform);
                    if (!onShooting)
                    {
                        EnemyShoot();
                        onShooting = true;
                    }
                    else
                        timeTonextShoot();
                }
            }
            else
            {
                if (SearchOthersTargets("PowerUp"))
                {
                    //codigo si encuentea un powerup
                    Debug.LogWarning("Detecto powerup");
                }
                else
                {
                    if(SearchOthersTargets("Isla"))
                    {
                        Debug.LogWarning("Detecto isla");
                        GameObject targetIsland = nextIsland();
                        MoveTowardsTarget(targetIsland.transform.position,0.1f);
                    }
                    else
                    {
                        Debug.LogWarning("patruyando...");
                        if (timebetweenChanges < timeSinceLastChange)
                        {
                            timeSinceLastChange = 0;
                            ChangeRotation();
                        }
                        else
                        {
                            timeSinceLastChange += Time.deltaTime;
                            MoveToForward(rb2D, velocity);
                        }
                    }

                }
            }
        }
    }

    public void AddATK()
    {
        levelATK++;
    }
    private void timeTonextShoot()
    {
        currentTimeShoot += Time.deltaTime;
        if (currentTimeShoot > timeBetweenShoots)
        {
            onShooting = false;
            currentTimeShoot = 0f;

        }
    }

    private void EnemyShoot()
    {
        Debug.LogWarning("disparando a player");
        InstanciateShoot(player.transform.position);
    }

    private void InstanciateShoot(Vector3 position)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.LookAt(position, -Vector3.forward);
        bullet.transform.rotation = new Quaternion(0, 0, bullet.transform.rotation.z,0);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }

    private bool SearchOthersTargets(string tag)
    {
        if (tag == "Isla")
        {
            GameObject targetIsland = nextIsland();
            if (targetIsland != null)
            {
                return true;
            }
        }
        return false;

    }

    private GameObject nextIsland()
    {
        GameObject[] objs;
        GameObject[] objIslaEmpty = GameObject.FindGameObjectsWithTag("IslaEmpty");
        GameObject[] objIslaPlayer = GameObject.FindGameObjectsWithTag("IslaPlayer");

        if (objIslaEmpty.Length > 0)
        {
            objs = objIslaEmpty;
        }
        else
        {
            objs = objIslaPlayer;
        }

        GameObject targetNearest = null;
        float distanceNearest = Mathf.Infinity;

        foreach (GameObject obj in objs)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < distanceNearest)
            {
                distanceNearest = distance;
                targetNearest = obj;
            }
        }

        return targetNearest;
    }

    public void ChangeRotation()
    {
        float ramdomAngle = Random.Range(0f, 360f);
        Quaternion newRotation = Quaternion.Euler(0, 0, ramdomAngle);
        StartCoroutine(GradualRotation(transform.rotation, newRotation, 1f));
    }

    IEnumerator GradualRotation(Quaternion inicioRotacion, Quaternion finRotacion, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            transform.rotation = Quaternion.RotateTowards(inicioRotacion, finRotacion, t * 360f);
            yield return null;
        }
    }

    public void MoveToForward(Rigidbody2D rb2D, float speed)
    {
        Vector2 forward = transform.rotation * Vector2.up;
        rb2D.velocity = forward * speed;
    }

    private void MoveTowardsTarget(Vector2 target, float contactDistance)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, contactDistance, layerHit);
        Vector2 actualDirection;
        float angulo;

        if (hit.collider != null)
        {
            Vector2 newDirection = Vector2.Reflect(direction, hit.normal);
            Vector2 pointC = hit.point + newDirection.normalized * contactDistance;
            Debug.DrawRay(hit.point, hit.normal * 2f, Color.blue); // Dibuja la normal del obstáculo.
            Debug.DrawRay(transform.position, newDirection * contactDistance, Color.yellow); // Dibuja la nueva dirección.
            actualDirection = newDirection * contactDistance;
            angulo = Mathf.Atan2(actualDirection.y, actualDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angulo);
            MoveTowardsTarget(pointC, contactDistance);
        }
        else
        {
            Debug.DrawRay(transform.position, direction * contactDistance, Color.green); //dibuja direccion hacia el objetivo
            actualDirection = direction * contactDistance;
            angulo = Mathf.Atan2(actualDirection.y, actualDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angulo);
            Vector2 newPosition = Vector2.MoveTowards(transform.position, target, velocity * Time.fixedDeltaTime);
            rb2D.MovePosition(newPosition);
        }

    }

    private void RotateTo(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
