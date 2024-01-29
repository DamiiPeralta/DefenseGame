using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public int maxAttack;
    public int minAttack;
    public int maxMgkattack;
    public int minMgkattack;
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
                Attack(minAttack, maxAttack, minMgkattack, maxMgkattack);
            }
        }
    }

    public void Attack(int minAtk, int maxAtk, int minMgk, int maxMgk)
    {
        System.Random randAtk = new System.Random();
        System.Random randMgk = new System.Random();

        int mgkAttack = randAtk.Next(minMgk, maxMgk + 1);
        int atk = randMgk.Next(minAtk, maxAtk + 1);
        int totalDamage = atk - enemyTarget.defense + mgkAttack - enemyTarget.mgkdefense;
        Debug.Log("the mgj atk is " + mgkAttack + " the attack is " + atk + "total damage =" + totalDamage + " left life = " + enemyTarget.actualHealth);


        enemyTarget.TakeDamage(mgkAttack, atk);
        Destroy(gameObject);
    }
}