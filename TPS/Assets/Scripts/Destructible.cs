using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IDamageable
{
    public float maxHealth = 1.0f;
    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    void Update()
    {
        if (currentHealth <= 0.0f)
        {
            if (gameObject.CompareTag("Enemy"))
                ServiceLocator.Get<GameManager>().IncrementEnemyKilled();
            else if (gameObject.CompareTag("Player"))
                ServiceLocator.Get<GameManager>().LoseGame();
            Destroy(gameObject);
        }
    }
}
