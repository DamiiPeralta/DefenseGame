using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    void Start()
    {
        damage = GameManager.Instance.damage;
        Destroy(gameObject, 5f);
    }  
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto con el que colisionó tiene el tag "Enemigo"
        if (other.CompareTag("Enemy"))
        {
            // Obtener el componente de vida del enemigo
            Enemy enemy = other.GetComponent<Enemy>();

            // Verificar si el componente de vida del enemigo es válido
            if (enemy != null)
            {
                // Aplicar daño al enemigo
                enemy.TakeDamage(damage);

                // Destruir el proyectil
                Destroy(gameObject);
            }
        }
    }
}