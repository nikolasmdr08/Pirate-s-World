using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    public float minHeight;
    public float maxHeight;
    public float minWidth;
    public float maxWidth;

    public GameObject[] PU;

    private void Start()
    {
        Invoke("spawnPU", 30f);
    }

    void spawnPU()
    {
        int index = Random.Range(0, PU.Length);
        GameObject instancia = Instantiate(PU[index], transform.position,Quaternion.identity);
        instancia.transform.position = new Vector2(Random.Range(minWidth, maxWidth), Random.Range(minHeight, maxHeight));
        Invoke("spawnPU", 30f);
    }
}
