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
    public int defense;
    public float attackSpeed;
    public int level;
    public Animator animator;
    public GameObject merc;

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
    if (enemyInFrontObj == null)
    {
        enemyInFront = false;
    }
    if (enemyInFrontObj != null)
    {
        if (!enemyInFrontObj.GetComponent<Enemy>().alive)
        {
            enemyInFront = false;
            enemyInFrontObj = null;
        }
    }
    if (merc != null)
    {
        if (!merc.GetComponent<MercenaryController>().stats.alive)
        {
            merc = null;
        }
    }
    if (alive && merc == null && !enemyInFront)
    {
        walk = true;
    }
    else
    {
        walk = false;
    }
    BattleLogic();
    if (walk)
    {
        transform.Translate(Vector2.left * velocity * Time.deltaTime);
    }
    if (enemyInFrontObj == null)
    {
        enemyInFront = false;
    }

}

public void Attack(int minAtk, int maxAtk)
{
    System.Random randAtk = new System.Random();

    int atk = randAtk.Next(minAtk, maxAtk + 1);
    int totalDamage = atk;
    Debug.Log(" the attack is " + atk + "total damage =" + totalDamage + " left life = " + merc.GetComponent<Stats>().health);


    if (merc.GetComponent<MercenaryController>() != null)
    {
        merc.GetComponent<MercenaryController>().TakeDamage(atk);
    }

    animator.SetBool("Attack", true);
}

public void TakeDamage(int atk)
{
    int attackDamageText;
    actualHealth -= Mathf.Max(0, atk - defense);
    attackDamageText = atk - defense;

    ShowDamageText(attackDamageText);

    if (actualHealth <= 0)
    {
        GameManager.Instance.money += killReward;
        Death();
    }
}

public void ShowDamageText(int damageAmount)
{
    GameObject damageTextObject = Instantiate(popUpDamagePrefab.GetComponent<PopUpText>().damageTextPrefab, transform.position, Quaternion.identity);
    damageTextObject.transform.parent = transform;
    damageTextObject.transform.localPosition = new Vector3(0f, 2f, 0f);

    TextMeshPro damageText = damageTextObject.GetComponent<TextMeshPro>();
    damageText.text = damageAmount.ToString();
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
    if (other.tag == "Merc")
    {
        merc = other.gameObject;
    }
    else if (other.tag == "EnemyBack")
    {
        enemyInFront = true;
        Transform parentTransform = other.gameObject.transform.parent;
        if (parentTransform != null)
        {
            GameObject parentObject = parentTransform.gameObject;
            enemyInFrontObj = parentObject;
        }
    }
}

private void OnTriggerExit2D(Collider2D other)
{
    if (other.tag == "EnemyBack")
    {
        enemyInFront = false;
        enemyInFrontObj = null;
    }
}

public void BattleLogic()
{
    if (merc != null)
    {
        walk = false;
        if (merc.GetComponent<MercenaryController>() != null)
        {
            if (merc.GetComponent<MercenaryController>().stats.alive)
            {
                if (!cooling)
                {
                    Attack(minAttack, maxAttack);
                    cooling = true;
                }
                else
                {
                    Cooldown();
                }
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