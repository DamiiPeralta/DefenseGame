using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarrackBtn : MonoBehaviour
{
    public int indexbtn;

    public List<GameObject> mercs = new List<GameObject>();
    public void SetMercInMouse(int index)
    {
        GameManager.Instance.dragAndDrop.merc = mercs[index];
    }
}
