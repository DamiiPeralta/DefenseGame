using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using TMPro;

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

    public GameObject popUpDamagePrefab;

    public float velocity = 3f;
    

    public int enemyDamage = 1;

    public int killReward = 1;

    public bool enemyInFront;

    public GameObject enemyInFrontObj;

    

    void Start()
    {
        actualHealth = health;
        enemyInFront = false;
    }

    void Update()
    {
        if(merc != null)
        {
            if(!merc.stats.alive)
            {
                merc = null;
            }
        }
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
        if(enemyInFrontObj == null)
        {
            enemyInFront = false;
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


            merc.TakeDamage(mgkAttack, atk);
            animator.SetBool("Attack", true);
            //if(merc != null)
            //{
            //    
            //    if(!merc.stats.alive)
            //    {
            //        merc = null;
            //    }
            //}
    }
    public void TakeDamage(int mgk, int atk)
    {
        int magicDamageText;
        int attackDamageText;
        actualHealth -= Mathf.Max(0, mgk - mgkdefense);
        actualHealth -= Mathf.Max(0, atk - defense);
        magicDamageText = mgk - mgkdefense;
        attackDamageText = atk - defense;

       // popUpDamagePrefab.GetComponent<PopUpText>().damageTextSpawnPoint = this.transform;
        ///popUpDamagePrefab.GetComponent<PopUpText>().ShowDamageText(magicDamageText += attackDamageText);
        ShowDamageText(magicDamageText += attackDamageText);

        if(actualHealth <= 0)
        {
            GameManager.Instance.money += killReward;
            Death();
               
        }
        
    }
    public void ShowDamageText(int damageAmount)
    {
        // Instanciar el Prefab del texto de daño
        GameObject damageTextObject = Instantiate(popUpDamagePrefab.GetComponent<PopUpText>().damageTextPrefab, transform.position, Quaternion.identity);
        Debug.Log(transform.position);
        // Establecer el objeto de texto como hijo del objeto al que le estás aplicando daño
        damageTextObject.transform.parent = transform;
    
        // Ajustar la posición relativa del objeto de texto
        damageTextObject.transform.localPosition = new Vector3(0f, 2f, 0f); // Ajusta las coordenadas según tus necesidades

        TextMeshPro damageText = damageTextObject.GetComponent<TextMeshPro>();
        damageText.text = damageAmount.ToString();
        // Destruir el objeto de texto después de un tiempo
        Destroy(damageTextObject, 4f);
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
            enemyInFrontObj = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            enemyInFront = false;
            enemyInFrontObj = null;
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
