using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public MercenaryType mercenaryType = MercenaryType.Archer; // Tipo de mercenario
    public GameObject arrowPrefab; // Prefab de la flecha o proyectil
    public Transform arrowSpawnPoint; // Punto de spawn de la flecha o proyectil
    public float arrowSpeed = 10f; // Velocidad de la flecha o proyectil

    public float meleeRange = 2f; // Rango de ataque cuerpo a cuerpo
    public float distanceRange = 10f; // Rango de ataque a distancia
    public float attackCooldown = 1.5f; // Tiempo de enfriamiento entre ataques
    public bool meleInRange = false; // Variable para rastrear si hay enemigos en el rango de ataque cuerpo a cuerpo
    public bool distanceInRange = false; // Variable para rastrear si hay enemigos en el rango de ataque a distancia
    public Animator anim;
    private GameObject currentTarget; // Objeto del enemigo actualmente seleccionado
    private bool canAttack = true; // Variable para controlar si el arquero puede atacar
    public Stats stats; // Referencia al script Stats

    private void Start() 
    {
       stats = GetComponent<Stats>();
       attackCooldown = stats.attackSpeed;

    }
    private void Update() 
    {
        if(stats.alive)
        {
            if (currentTarget != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (distanceToEnemy > distanceRange)
                {
                    distanceInRange = false;
                }
                if (distanceToEnemy > meleeRange)
                {
                    meleInRange = false;
                }
            }
            else
            {
                SetIdle();
            } 
        }
           
    }
    // Método para manejar el evento OnTriggerEnter cuando otros colliders ingresan en el rango del arquero
    private void OnTriggerEnter(Collider other)
    {
        
        // Verifica si el collider tocado es del tipo Enemy
        if (other.CompareTag("Enemy"))
        {
            // Determina la distancia entre el arquero y el enemigo
            float distanceToEnemy = Vector3.Distance(transform.position, other.transform.position);

            // Verifica si el enemigo está dentro del rango de ataque cuerpo a cuerpo
            if (distanceToEnemy <= meleeRange)
            {
                // Si el enemigo está dentro del rango de ataque cuerpo a cuerpo, establece la variable meleInRange en true
                meleInRange = true;
                anim.SetBool("Attack", true);
                anim.SetBool("DistanceAttack", false);
            }
            // Verifica si el enemigo está dentro del rango de ataque a distancia
            else if (distanceToEnemy <= distanceRange)
            {
                // Si el enemigo está dentro del rango de ataque a distancia, establece la variable distanceInRange en true
                distanceInRange = true;
                anim.SetBool("Attack", false);
                anim.SetBool("DistanceAttack", true);
            }

            // Si no hay un objetivo actual o el enemigo que ingresó está más cerca que el objetivo actual, actualiza el objetivo
            if (currentTarget == null || distanceToEnemy < Vector3.Distance(transform.position, currentTarget.transform.position))
            {
                currentTarget = other.gameObject;
            }

            // Si el arquero puede atacar, llama a la función AttackLogic
            if (canAttack)
            {
                AttackLogic();
            }
        }
    }

    // Método para manejar el evento OnTriggerExit cuando otros colliders salen del rango del arquero
    private void OnTriggerExit(Collider other)
    {
        // Verifica si el collider que ha dejado de tocar es del tipo Enemy
        if (other.CompareTag("Enemy"))
        {
            // Si el enemigo sale del rango de ataque cuerpo a cuerpo, establece la variable meleInRange en false
            meleInRange = false;

            // Si el enemigo sale del rango de ataque a distancia, establece la variable distanceInRange en false
            distanceInRange = false;

            // Si el enemigo que sale es el objetivo actual, busca un nuevo objetivo
            if (other.gameObject == currentTarget)
            {
                currentTarget = null;
            }
        }
    }

    public void TakeDamage(int mgk, int atk)
    {
        stats.health -= Mathf.Max(0, mgk - stats.mgkdefense);
        stats.health -= Mathf.Max(0, atk - stats.defense);
        
        if(stats.health <= 0)
        {
            Death();   
        }
        
    }

    private void Death()
    {
        anim.SetTrigger("Death");
        stats.alive = false;
    }

    // Método para que el arquero realice una acción en función de si hay enemigos en el rango de ataque
    private void AttackLogic()
    {
        if (currentTarget != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, currentTarget.transform.position);
            // Verifica si hay enemigos en el rango de ataque cuerpo a cuerpo
            if (meleInRange && distanceToEnemy <= meleeRange)
            {
                // Si hay enemigos en el rango de ataque cuerpo a cuerpo, el arquero realiza un ataque cuerpo a cuerpo
                MeleeAttack(stats.minAttack, stats.maxAttack, stats.minMgkattack, stats.maxMgkattack);
            }
            else if (distanceInRange && distanceToEnemy <= distanceRange)
            {
                // Si hay enemigos en el rango de ataque a distancia, el arquero dispara flechas
                ShootArrow();
            }
        }
    }

    // Método para simular el ataque cuerpo a cuerpo del arquero
    public void MeleeAttack(int minAtk, int maxAtk, int minMgk, int maxMgk)
    {
        System.Random randAtk = new System.Random();
        System.Random randMgk = new System.Random();

        Enemy enemy =  currentTarget.GetComponent<Enemy>();
        int mgkAttack = randAtk.Next(minMgk, maxMgk + 1);
        int atk = randMgk.Next(minAtk, maxAtk + 1);
        int totalDamage = atk - enemy.defense + mgkAttack - enemy.mgkdefense;
        Debug.Log("the mgj atk is " + mgkAttack + " the attack is " + atk + "total damage =" + totalDamage + " left life = " + enemy.actualHealth);
   

        // Comienza el cooldown del ataque
        StartCooldown();
    }

    // Método para simular el disparo de flechas del arquero
    private void ShootArrow()
    {
        Debug.Log("Archer shooting arrows!");
        // Aquí colocarías la lógica de tu disparo de flechas

        // Instancia la flecha o proyectil
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        // Aplica una velocidad a la flecha o proyectil en la dirección hacia adelante
        Rigidbody arrowRigidbody = arrow.GetComponent<Rigidbody>();
        arrowRigidbody.velocity = transform.forward * arrowSpeed;

        // Comienza el cooldown del ataque
        StartCooldown();
    }
    private void SetIdle()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("DistanceAttack", false);
    }

    // Método para iniciar el cooldown del ataque
    private void StartCooldown()
    {
        // Establece canAttack en false para que el arquero no pueda atacar durante el cooldown
        canAttack = false;
        // Llama a la función ResetAttack después de 'attackCooldown' segundos para restablecer canAttack
        Invoke("ResetAttack", attackCooldown);
    }

    // Método para restablecer el estado de ataque del arquero
    private void ResetAttack()
    {
        canAttack = true;
    }
}

