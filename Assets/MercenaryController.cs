using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Timeline;

public class MercenaryController : MonoBehaviour
{
    public MercenaryStats mercenaryStats;

    public Stats stats;
    public Enemy enemy;
    public Animator animator;

    public BoxCollider2D coll;

    public bool cooling;
    public float timer;
    public int actualHealth;

    private void Start() 
    {
        
        actualHealth = stats.health;
        stats.stats = mercenaryStats;
        stats.SetStatus();
        stats.alive = true;
        actualHealth = stats.health;  
    }

    private void Update() 
    {
        BattleLogic();
    }

    public void Attack(int minAtk, int maxAtk, int minMgk, int maxMgk)
    {
        System.Random randAtk = new System.Random();
        System.Random randMgk = new System.Random();

        int mgkAttack = randAtk.Next(minMgk, maxMgk + 1);
        int atk = randMgk.Next(minAtk, maxAtk + 1);
        int totalDamage = atk - enemy.defense + mgkAttack - enemy.mgkdefense;
        Debug.Log("the mgj atk is " + mgkAttack + " the attack is " + atk + "total damage =" + totalDamage + " left life = " + enemy.actualHealth);

        enemy.TakeDamage(mgkAttack, atk);
        animator.SetBool("Attack", true);
        if(!enemy.alive)
        {
            enemy = null;
        }
    }
    public void TakeDamage(int mgk, int atk)
    {
        actualHealth -= Mathf.Max(0, mgk - stats.mgkdefense);
        actualHealth -= Mathf.Max(0, atk - stats.defense);
        
        if(actualHealth <= 0)
        {
            Death();   
        }
        
    }
    public void Death()
    {
        stats.alive = false;
        coll.enabled = false;
        animator.SetTrigger("Death");
    }
    public void LevelUp()
    {

    }
    public void TakeExp(int exp)
    {

    }
    public void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling)
        {
            cooling = false;
            timer = stats.attackSpeed;

        }
        
    }

    public void BackToIdle()
    {
        animator.SetBool("Attack", false);
    }
    public void BattleLogic()
    {
        if(stats.alive)
        {
            if(enemy != null)
            {
                if(enemy.alive)
                {
                    if(!cooling)
                    {
                        Attack(stats.minAttack, stats.maxAttack, stats.minMgkattack, stats.maxMgkattack);
                        cooling = true;
                    }
                    else
                    {
                        
                        Cooldown();
                    }
                }
            }
            else
            {
                BackToIdle();
                timer = stats.attackSpeed;
            }  
        }
             
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            enemy = other.gameObject.GetComponent<Enemy>();
        }
    }

}
