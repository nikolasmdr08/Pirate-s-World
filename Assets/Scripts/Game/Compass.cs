using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public string objTarget1;
    public string objTarget2;
    GameObject ship;
    public GameObject[] objs;
    public SpriteRenderer render;

    private void Start()
    {
        ship = GameObject.Find("ShipPrefab");
    }

    private void Update()
    {
        GameObject[] obj1 = GameObject.FindGameObjectsWithTag(objTarget1);
        GameObject[] obj2 = GameObject.FindGameObjectsWithTag(objTarget2);

        
        if (obj1.Length > 0)
        {
            objs = obj1;
        }
        else
        {
            objs = obj2;
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

        if (targetNearest != null)
        {
            render.enabled = true;
            Vector3 direction = targetNearest.transform.position - transform.position;
            float angulo = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angulo);
        }
        else
        {
            render.enabled = false;
        }
    }
}
