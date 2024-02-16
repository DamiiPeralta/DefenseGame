using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public int maxAttack;
    public int minAttack;
    public Enemy enemyTarget;
    void Start()
    {
        Destroy(gameObject, 5f);
    }  
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto con el que colisionó tiene el tag "Enemigo"
        if (other.CompareTag("Enemy"))
        {
            // Obtener el componente de vida del enemigo
            Enemy enemy = other.GetComponent<Enemy>();
            enemyTarget = enemy;
            
            // Verificar si el componente de vida del enemigo es válido
            if (enemy != null)
            {
                Attack(minAttack, maxAttack);
            }
        }
    }

    public void Attack(int minAtk, int maxAtk)
    {
        System.Random randAtk = new System.Random();
        int atk = randAtk.Next(minAtk, maxAtk + 1);
        int totalDamage = atk - enemyTarget.defense;
        Debug.Log(" the attack is " + atk + "total damage =" + totalDamage + " left life = " + enemyTarget.actualHealth);


        enemyTarget.TakeDamage(atk);
        Destroy(gameObject);
    }
}