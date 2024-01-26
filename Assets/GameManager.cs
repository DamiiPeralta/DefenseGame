using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Asegurar que solo haya una instancia del GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public HealthController healthController;

    public EnemySpawner enemySpawner;
    public int actualHealth;
    public int maxHealth;

    public int money;

    public int damage;

    public int firstLevelEnemies;

    public int inicialDamage;

    public int inicialMaxHealth;

    public int wave;



}
