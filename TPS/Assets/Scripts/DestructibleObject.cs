using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructibleObject : MonoBehaviour, IDamageable
{
    public Image healthBar;

    public float maxHealth = 100.0f;
    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
    }
}
