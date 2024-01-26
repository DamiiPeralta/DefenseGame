using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocity = 3f;
    public int maxHealth = 2;

    public int enemyDamage = 1;

    public int killReward = 1;

    private int actualHealth;

    void Start()
    {
        actualHealth = maxHealth;
    }

    void Update()
    {
        transform.Translate(Vector2.left * velocity * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        actualHealth -= damage;

        if (actualHealth <= 0)
        {
            // Destruir el enemigo cuando se queda sin vida
            GameManager.Instance.money += killReward;
            Destroy(gameObject);
        }
    }

    
    
}
