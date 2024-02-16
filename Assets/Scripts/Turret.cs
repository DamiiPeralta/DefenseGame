using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public enum FireMode { Rapid, Medium, Slow }

    public float rotationSpeed = 10.0f;
    public float projectileSpeed = 10.0f;
    public float range = 100.0f;

    public GameObject projectilePrefab;
    public Transform target;
    public Transform firePoint;
    public Transform cannonTransform;

    private FireMode fireMode = FireMode.Rapid;
    private float nextFireTime = 0.0f;
    private bool autoShoot = true;

    private void Start()
    {
        target = null;
    }

    private void Update()
    {
        // Rotación de la torreta
        if (target != null)
        {
            Vector3 targetDirection = target.position - transform.position;
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, targetAngle), rotationSpeed * Time.deltaTime);
        }
        else if (autoShoot)
        {
            // Buscar enemigos
            GameObject nearestEnemy = FindNearestEnemyInRange();
            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                // Apuntar hacia la posición del mouse
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = transform.position.z;
                Vector3 targetDirection = mousePosition - transform.position;
                float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, targetAngle), rotationSpeed * Time.deltaTime);
            }
        }

        // Seguimiento del cañón
        if (target != null)
        {
            Vector3 targetDirection = target.position - cannonTransform.position;
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            cannonTransform.rotation = Quaternion.Slerp(cannonTransform.rotation, Quaternion.Euler(0f, 0f, targetAngle), rotationSpeed * Time.deltaTime);
        }

        // Disparo
        if (Input.GetMouseButton(0) || (autoShoot && target != null))
        {
            if (Time.time > nextFireTime)
            {
                FireProjectile();
                nextFireTime = Time.time + GetFireRate();
            }
        }

        // Cambio de modo de fuego
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetFireMode(FireMode.Rapid);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetFireMode(FireMode.Medium);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetFireMode(FireMode.Slow);
        }

        // Cambio de estado
        if (Input.GetKeyDown(KeyCode.Q))
        {
            autoShoot = !autoShoot;
            target = null;
        }
    }

    private GameObject FindNearestEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && distance <= range)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D projectileRigidbody2d = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody2d.velocity = firePoint.forward * projectileSpeed;
        Debug.Log("Firing projectile");
    }

    private float GetFireRate()
    {
        switch (fireMode)
        {
            case FireMode.Rapid:
                return 0.1f;
            case FireMode.Medium:
                return 0.5f;
            case FireMode.Slow:
                return 1.0f;
            default:
                return 0.0f;
        }
    }

    public void SetFireMode(FireMode mode)
    {
        fireMode = mode;
    }
}
