using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class EnemySpawner : MonoBehaviour
{
    public bool isSpawning = false;
    public GameObject enemyPrefab;
    public float cooldownSpawn = 1f;
    public Transform[] spawnPositions;

    private float timeSinceLastSpawn = 0f;

    public int enemiesLeftToSpawn;

    public bool lastEnemySpawned;

    public int enemieInLevel;

    public bool levelStart;
    void Start()
    {
        enemieInLevel = GameManager.Instance.firstLevelEnemies;
    }

    void Update()
    {
        if(levelStart)
        {

        if(isSpawning && enemiesLeftToSpawn != 0)
        {
            // Actualizar el tiempo pasado desde el último spawn
            timeSinceLastSpawn += Time.deltaTime;

            // Verificar si ha pasado el tiempo de cooldown
            if (timeSinceLastSpawn >= cooldownSpawn)
            {
                // Elegir aleatoriamente una posición de spawn
                Transform choosedPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

                // Crear un nuevo enemigo en la posición elegida
                Instantiate(enemyPrefab, choosedPosition.position, Quaternion.identity);
                --enemiesLeftToSpawn;
                // Reiniciar el tiempo pasado desde el último spawn
                timeSinceLastSpawn = 0f;
            }

        }
        if(enemiesLeftToSpawn == 0)
        {
            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
            bool enemigosEncontrados;
            enemigosEncontrados = enemigos.Length > 0;
            if(!enemigosEncontrados)
            {
                WinWave();
            }
            
        }
        }
        
    }
    public void StartWave()
    {
        ++GameManager.Instance.wave;
        enemiesLeftToSpawn = enemieInLevel;
        GameManager.Instance.healthController.centerText.gameObject.SetActive(false);
        levelStart = true;
        isSpawning = true;
    }

    public void WinWave()
    {
        GameManager.Instance.healthController.centerText.gameObject.SetActive(true);
        GameManager.Instance.healthController.centerText.text = "Wave " + GameManager.Instance.wave + "!";
        enemieInLevel = enemieInLevel * 2;
        levelStart = false;

    }
}
