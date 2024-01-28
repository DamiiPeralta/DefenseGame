using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "MercenaryStats", menuName = "Data/MercenaryStats")]
public class MercenaryStats : ScriptableObject
{
    public string mercName;
    public Sprite sprite;
    public bool alive;
    public int maxAttack;
    public int minAttack;
    public int maxMgkattack;
    public int minMgkattack;
    public int defense;
    public int mgkdefense;
    public float attackSpeed;
    public int strenght;
    public int dexterity;
    public int inteligence;
    public int vigor;
    public int mind;
    public int health;
    public int mana;
    public int level;
    public int exp;
    public int nLExp;
    public GameObject mercenaryObj;
}
