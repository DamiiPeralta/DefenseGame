using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class HealthController : MonoBehaviour
{
    public TextMeshProUGUI actualHealthText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI paneldamageText;
    public TextMeshProUGUI panelMaxHealthText;
    public TextMeshProUGUI panelActualHealthText;
    public TextMeshProUGUI panelHealMoneyText;
    public TextMeshProUGUI panelMaxHealMoneyText;
    public TextMeshProUGUI panelAddDamageMoneyText;

    public TextMeshProUGUI centerText;

    public GameObject shopPanel;

    public int maxHealthCost = 1;
    public int healCost = 1;
    public int addDamageCost = 1;
    
    void Start()
    {
        Heal();
    }

    void Update()
    {
        actualHealthText.text = GameManager.Instance.actualHealth.ToString();
        moneyText.text = GameManager.Instance.money.ToString();

        if(GameManager.Instance.actualHealth <= 0)
        {
            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemigos.Length; i++)
            {
                Destroy(enemigos[i]);
            }
            Restart();
        }

    }

    public void Restart()
    {
        GameManager.Instance.enemySpawner.isSpawning = false;
        GameManager.Instance.enemySpawner.enemiesLeftToSpawn = 0;
        GameManager.Instance.wave = 1;
        GameManager.Instance.money = 0;
        GameManager.Instance.damage = 1;
        GameManager.Instance.actualHealth = GameManager.Instance.inicialMaxHealth;
        GameManager.Instance.maxHealth = GameManager.Instance.inicialMaxHealth;
        GameManager.Instance.enemySpawner.enemieInLevel = GameManager.Instance.firstLevelEnemies;
    }   
    public void Heal()
    {
        if(GameManager.Instance.money >= healCost)
        {
            
            if(GameManager.Instance.actualHealth < GameManager.Instance.maxHealth)
            {
                GameManager.Instance.money -= healCost;
                ++GameManager.Instance.actualHealth;
                PanelSet();
            }
        }
    }

    public void AddMaxHealth()
    {
        if(GameManager.Instance.money >= maxHealthCost)
        {
            GameManager.Instance.money -= maxHealthCost;
        GameManager.Instance.maxHealth += 1;
        maxHealthCost = maxHealthCost * 2;
        PanelSet();
        }
    }

    public void ActivePanel()
    {
        if(!shopPanel.gameObject.activeInHierarchy)
        {
            shopPanel.SetActive(true);
            PanelSet();
        }
        else
        {
            shopPanel.SetActive(false);
        }
    }

    public void AddDamage()
    {
        if(GameManager.Instance.money >= addDamageCost)
        {
            GameManager.Instance.money -= addDamageCost;
            GameManager.Instance.damage += 1;
            addDamageCost = addDamageCost * 2;
            PanelSet();
        }
        
    }

    public void PanelSet()
    {
        actualHealthText.text = GameManager.Instance.actualHealth.ToString();
        moneyText.text = GameManager.Instance.money.ToString();
        panelActualHealthText.text = GameManager.Instance.actualHealth.ToString();
        panelMaxHealthText.text = GameManager.Instance.maxHealth.ToString();
        panelHealMoneyText.text = healCost.ToString();
        panelMaxHealMoneyText.text = maxHealthCost.ToString();
        panelAddDamageMoneyText.text = addDamageCost.ToString();
        paneldamageText.text = GameManager.Instance.damage.ToString();

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto con el que colisionó tiene el tag "Enemigo"
        if (other.CompareTag("Enemy"))
        {
            // Obtener el componente de vida del enemigo
            GameObject enemy = other.gameObject;
            GameManager.Instance.actualHealth -= enemy.GetComponent<Enemy>().enemyDamage;
            Destroy(enemy);
        }
    }
}
