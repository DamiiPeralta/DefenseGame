using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public float fuerzaImpulso = 500f;
    void Update()
    {
         if (Input.GetMouseButtonDown(0))
        {
            // Disparar cuando se hace clic izquierdo
            if(!GameManager.Instance.healthController.centerText.gameObject.activeInHierarchy && !GameManager.Instance.healthController.shopPanel.gameObject.activeInHierarchy)
            {
                
                Shoot();
            }
        }
        // Obtener la posición del mouse en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Asegurarse de que la Z sea cero para un juego en 2D

        // Apuntar la torreta hacia la posición del mouse
        transform.up = mousePosition - transform.position;
    }

    void Shoot()
    {
        // Crear un nuevo proyectil en la posición de la torreta
        GameObject proyectil = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Obtener la posición del mouse en el mundo
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionMouse.z = 0f; // Asegurarse de que la Z sea cero para un juego en 2D

        // Calcular la dirección hacia la posición del mouse
        Vector2 direccion = (posicionMouse - transform.position).normalized;

        // Aplicar fuerza al proyectil
        proyectil.GetComponent<Rigidbody2D>().AddForce(direccion * fuerzaImpulso);
    }
}
