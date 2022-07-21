using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class InGameHintScript : MonoBehaviour
{
    [SerializeField]
    private GameObject popUpPhrase;
    [SerializeField]
    private string hintText;
    [SerializeField]
    private TextMeshProUGUI popUpText;
    [SerializeField]
    private BoxCollider boxTrigger;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Text());
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {
        boxTrigger.enabled = false;
    }
    void HidePopUp()
    {
        popUpPhrase.SetActive(false);
        popUpText.text = "";
    }
    void ShowPopUp()
    {
        popUpPhrase.SetActive(true);
        popUpText.text = hintText;
       
    }

    IEnumerator Text()
    {
        ShowPopUp();
        yield return new WaitForSeconds(5);
        HidePopUp();
    }
}

