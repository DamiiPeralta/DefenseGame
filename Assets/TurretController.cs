using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public enum FireMode { Rapid, Medium, Slow }
    private FireMode fireMode = FireMode.Rapid;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f; // Velocidad del proyectil, modificable desde el inspector
    private float nextFireTime = 0.0f;
    private bool autoShoot = true;
    public float range = 100.0f;
    public Transform target;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            autoShoot = !autoShoot;
            target = null;
        }
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + GetFireRate();
        }
        if (autoShoot)
        {
            if(target != null)
            {
                if (target.GetComponent<Enemy>().actualHealth <= 0)
                {
                    target = null;
                    GameObject nearestEnemy = FindNearestEnemyInRange();
                    if (nearestEnemy != null && nearestEnemy.GetComponent<Enemy>().actualHealth > 0)
                    {
                        target = nearestEnemy.transform;
                    }
                }
            }
            if (target == null)
            {
                target = null;
                GameObject nearestEnemy = FindNearestEnemyInRange();
                if (nearestEnemy != null)
                {
                    target = nearestEnemy.transform;
                }
            }

            if (target != null)
            {
                AimAtTarget(target.position);
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + GetFireRate();
                }
            }
            else
            {
                AimAtMousePosition();
            }
        }
        else
        {
            AimAtMousePosition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetFireMode(FireMode.Rapid);
            Debug.Log("Rapid fire mode");
            bulletPrefab.GetComponent<Bullet>().maxAttack = 3;
            bulletPrefab.GetComponent<Bullet>().minAttack = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetFireMode(FireMode.Medium);
            Debug.Log("Medium fire mode");
            bulletPrefab.GetComponent<Bullet>().maxAttack = 6;
            bulletPrefab.GetComponent<Bullet>().minAttack = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetFireMode(FireMode.Slow);
            Debug.Log("Slow fire mode");
            bulletPrefab.GetComponent<Bullet>().maxAttack = 10;
            bulletPrefab.GetComponent<Bullet>().minAttack = 5;
        }
    }

    void Shoot()
    {
        Vector3 shootDirection;
        if(target == null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            shootDirection = (mousePosition - transform.position).normalized;
        }
        else
        {   
            shootDirection = (target.position - transform.position).normalized;
        } 

        GameObject proyectil = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        proyectil.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        proyectil.transform.up = shootDirection;
        proyectil.transform.Rotate(0, 0, 90);
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

    private GameObject FindNearestEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && distance <= range && enemy.GetComponent<Enemy>().actualHealth > 0)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    void AimAtTarget(Vector3 targetPosition)
    {
        Vector3 targetDirection = targetPosition - transform.position;
        transform.up = targetDirection;
    }

    void AimAtMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 directionToMouse = mousePosition - transform.position;
        transform.up = directionToMouse;
    }
}