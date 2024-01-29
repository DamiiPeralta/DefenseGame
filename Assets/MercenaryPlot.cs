using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryPlot : MonoBehaviour
{
    public GameObject mercObj;
    public GameObject positionObj;
    public MercenaryPlot plot;
    public Color hoverColor;
    public Color originalColor;
    public bool isMouseover;
    public bool isMercInPlot;

    public void Start()
    {

    }
    public void Update()
    {
        if(mercObj == null)
        {
            UnSetMerc();
        }
    }
    private void OnMouseEnter()
    {
        isMouseover = true;
        Debug.Log("drag and drop tiene un plot");
        // Cambiar el color al color de resaltado cuando el mouse entra
        plot.GetComponent<SpriteRenderer>().color = hoverColor;
        GameManager.Instance.dragAndDrop.mercPlot = plot;
    }
    private void OnMouseUp() 
    {
        if(!isMercInPlot)
        {
            if(GameManager.Instance.dragAndDrop.merc != null)
            {
                isMercInPlot = true;
                SetMerc(GameManager.Instance.dragAndDrop.merc);
                GameManager.Instance.dragAndDrop.merc = null;
            }
        }
        
    }

    private void OnMouseExit()
    {
        isMouseover = false;
        // Volver al color original cuando el mouse sale
        plot.GetComponent<SpriteRenderer>().color = originalColor;
        GameManager.Instance.dragAndDrop.mercPlot = null;
    }

    public void SetMerc(GameObject mercInMouse)
    {
        mercObj = mercInMouse;
        Vector3 posicionOriginal = positionObj.transform.position;

        // Instanciar un nuevo objeto en la misma posición que el original
        positionObj = Instantiate(mercObj, posicionOriginal, Quaternion.identity);
        
        positionObj.SetActive(true);

    }
    public void UnSetMerc()
    {
        positionObj.SetActive(false);
    }
}
