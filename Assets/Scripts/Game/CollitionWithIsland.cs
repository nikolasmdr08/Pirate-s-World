using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollitionWithIsland : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IslandController isla = collision.gameObject.GetComponent<IslandController>();
        if(isla != null)
        {
            isla.OnInvation(tag);
        }
    }

}
