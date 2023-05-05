using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : MonoBehaviour
{
    public string actualLider;
    public bool inInvation = false;
    public float lifeTime = 3f;
    public float time = 0f;
    public string _attacker;

    Color colorPlayer = new Color(0.2870152f, 0.7098039f, .2392157f, .5f);
    Color colorEmpty = new Color(0.8313726f, 0.7169812f, .1386614f, .5f);
    Color colorEnemy = new Color(.7176471f, .1372549f, .1372549f, .5f);

    public SpriteRenderer islaColor;

    private void Start()
    {
        islaColor = GetComponent<SpriteRenderer>();
    }

    public void OnInvation(string attacker)
    {
        if(!inInvation && attacker != actualLider)
        {
            inInvation = true;
            _attacker = attacker;
        }
        else
        {
            inInvation = false;
            _attacker = "";
        }

    }

    private void Update()
    {
        AtackController();
        ColorController();
    }

    private void ColorController()
    {
        if(actualLider == "Player")
        {
            islaColor.color = colorPlayer;
        }
        else if (actualLider == "Enemy")
        {
            islaColor.color = colorEnemy;
        }
        else
        {
            islaColor.color = colorEmpty;
        }
    }

    private void AtackController()
    {
        if (inInvation && _attacker != actualLider)
        {
            time += Time.deltaTime;
            if (time >= lifeTime)
            {
                if (_attacker == "Player")
                    gameObject.tag = "IslaPlayer";
                else
                {
                    gameObject.tag = "IslaEnemy";
                }

                actualLider = _attacker;
                time = 0f;
                inInvation = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == _attacker)
        {
            time = 0f;
            _attacker = "";
            inInvation = false;
        }
    }
}
