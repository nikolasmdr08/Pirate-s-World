using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public int maxHealth = 10;
    [SerializeField] private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ReceiveDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage); // cuando toma valores negativos siempre devolvera cero
    }

    public void ReceiveHealing(int healing)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healing); // cuando supere maxHealth devolvera maxHealth
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public int ActualLifePoints()
    {
        return currentHealth;
    }
}
