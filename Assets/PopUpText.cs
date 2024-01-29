using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PopUpText : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public Transform damageTextSpawnPoint;

    //public void ShowDamageText(int damageAmount)
    //{
    //    // Instanciar el Prefab del texto de daño
    //    GameObject damageTextObject = Instantiate(damageTextPrefab, damageTextSpawnPoint.position, Quaternion.identity);
    //    Debug.Log(damageTextSpawnPoint.position);
    //    // Configurar el texto con el daño recibido
    //    TextMeshPro damageText = damageTextObject.GetComponent<TextMeshPro>();
    //    damageText.text = damageAmount.ToString();
    //    // Destruir el objeto de texto después de un tiempo
    //    Destroy(damageTextObject, 4f);
    //}
}
