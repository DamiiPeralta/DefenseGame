using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarrackBtn : MonoBehaviour
{
    public int indexbtn;

    public List<MercenaryStats> mercStat = new List<MercenaryStats>();
    public List<GameObject> mercs = new List<GameObject>();

    private void Start() 
    {
        for (int i = 0; i < mercs.Count; i++)
        {
            mercs[i].GetComponent<MercenaryController>().stats = mercStat[i];
        }    
    }
    public void SetMercInMouse(int index)
    {
        GameManager.Instance.dragAndDrop.merc = mercs[index];
    }
}
