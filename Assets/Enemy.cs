using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{
    public bool alive = true;
    public BoxCollider2D coll;
    public bool cooling;
    public float timer;
    public bool walk;
    public int health = 2;
    public int actualHealth;
    public int maxAttack;
    public int minAttack;
    public int maxMgkattack;
    public int minMgkattack;
    public int defense;
    public int mgkdefense;
    public float attackSpeed;
    public int level;
    public Animator animator;
    public MercenaryController merc;


    public float velocity = 3f;
    

    public int enemyDamage = 1;

    public int killReward = 1;

    public bool enemyInFront;

    

    void Start()
    {
        actualHealth = health;
        enemyInFront = false;
    }

    void Update()
    {
        if(alive && merc == null && !enemyInFront)
            {
                walk = true;
            }
        else
        {
            walk = false;
        }
        BattleLogic();
        if(walk)
        {
            transform.Translate(Vector2.left * velocity * Time.deltaTime);
        }
        
    }

    

    public void Attack(int minAtk, int maxAtk, int minMgk, int maxMgk)
    {
        System.Random randAtk = new System.Random();
        System.Random randMgk = new System.Random();

        int mgkAttack = randAtk.Next(minMgk, maxMgk + 1);
        int atk = randMgk.Next(minAtk, maxAtk + 1);
        int totalDamage = atk + mgkAttack;
        Debug.Log("the mgj atk is " + mgkAttack + " the attack is " + atk + "total damage =" + totalDamage + " left life = " + merc.actualHealth);


        if(merc.stats.alive)
        {
            merc.TakeDamage(mgkAttack, atk);
            animator.SetBool("Attack", true);
            if(!merc.stats.alive)
            {
                merc = null;
            }
        }
    }
    public void TakeDamage(int mgk, int atk)
    {
        actualHealth -= Mathf.Max(0, mgk - mgkdefense);
        actualHealth -= Mathf.Max(0, atk - defense);
        if(actualHealth <= 0)
        {
            GameManager.Instance.money += killReward;
            Death();
               
        }
        
    }
    public void Death()
    {
        alive = false;
        coll.enabled = false;
        animator.SetTrigger("Death");
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Merc")
        {
            merc = other.gameObject.GetComponent<MercenaryController>();
            if(!merc.stats.alive)
            {
                merc = null;
            }
        }
        if(other.tag == "Enemy")
        {
            enemyInFront = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            enemyInFront = false;
        }
    }
    public void BattleLogic()
    {
        
        if(merc != null)
        {
            walk = false;
            if(merc.stats.alive)
            {
                if(!cooling)
                {
                    Attack(minAttack, maxAttack, minMgkattack, maxMgkattack);
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
            timer = attackSpeed;
            
        }       
    }
    public void BackToIdle()
    {
        animator.SetBool("Attack", false);
    }
    void Cooldown()
    {
        
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling)
        {
            cooling = false;
            timer = attackSpeed;

        }
    }
    
}
