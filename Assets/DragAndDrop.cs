using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DragAndDrop : MonoBehaviour
{
    public GameObject merc;
    public MercenaryPlot mercPlot;
    public SpriteRenderer objSelected;
    public MercenaryPlot mercPlotinWait;
    void Update()
    {
        if(merc != null)
        {
            
            objSelected.enabled = true;
            objSelected.sprite = merc.GetComponent<MercenaryController>().stats.stats.sprite;
        }
        else
        {
            objSelected.enabled = false;
        }
        FollowMouse();
    }

    private void OnMouseDown() 
    {
        merc = null;    
    }

    public void FollowMouse()
    {
        
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionMouse.z = transform.position.z;
        transform.position = posicionMouse;
    }
}
