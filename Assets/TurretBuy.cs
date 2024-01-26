using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretBuy : MonoBehaviour
{
    public GameObject turretPrefab;
    public GameObject plot;
    public Vector3 set1;
    public Color hoverColor;
    public Color originalColor;
    private void OnMouseEnter()
    {
        // Cambiar el color al color de resaltado cuando el mouse entra
        plot.GetComponent<SpriteRenderer>().color = hoverColor;
        Debug.Log("enter");
    }

    private void OnMouseExit()
    {
        // Volver al color original cuando el mouse sale
        plot.GetComponent<SpriteRenderer>().color = originalColor;
        Debug.Log("exit");
    }

    void OnMouseDown()
    {
        // Acción que se ejecuta cuando se hace clic en el objeto
        Debug.Log("Clic en el objeto");

        // Puedes agregar aquí la lógica que deseas ejecutar al hacer clic
        // Por ejemplo, instanciar un objeto, activar/desactivar algo, etc.
    }
    void Update()
    {
        
    }

    
}
