using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public MercenaryType mercenaryType;
    public MercenaryStats stats;
    public Sprite sprite;
    public string mercName;
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

    public void SetStatus()
    {
    sprite = stats.sprite;
    mercName = stats.mercName;
    alive = stats.alive;
    maxAttack = stats.maxAttack;
    minAttack = stats.minAttack;
    maxMgkattack = stats.maxMgkattack;
    minMgkattack = stats.minMgkattack;
    defense = stats.defense;
    mgkdefense = stats.mgkdefense;
    attackSpeed = stats.attackSpeed;
    strenght = stats.strenght;
    dexterity = stats.dexterity;
    inteligence = stats.inteligence;
    vigor = stats.vigor;
    mind = stats.mind;
    health = stats.health;
    mana = stats.mana;
    level = stats.level;
    exp = stats.exp;
    nLExp = stats.nLExp;
    }
}
