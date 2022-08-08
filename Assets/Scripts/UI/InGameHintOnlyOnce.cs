using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameHintOnlyOnce : MonoBehaviour
{
    [SerializeField]
    private GameObject _popUpPhrase;
    [SerializeField]
    private string _hintText;
    [SerializeField]
    private TextMeshProUGUI _popUpText;
    [SerializeField]
    private BoxCollider _boxTrigger;
    [SerializeField]
    private string _key;

    void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefs.HasKey(_key))
        {
            return;
        }
        else
        {
            StartCoroutine(Text());
            PlayerPrefs.SetString(_key, _key);
        }
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {
        _boxTrigger.enabled = false;
    }
    void HidePopUp()
    {
        _popUpPhrase.SetActive(false);
        _popUpText.text = "";
    }
    void ShowPopUp()
    {
        _popUpPhrase.SetActive(true);
        //_popUpText.text = _hintText;
        _popUpText.text = LocalizationManager.GetTranslate(_hintText);
    }

    IEnumerator Text()
    {
        ShowPopUp();
        yield return new WaitForSeconds(5);
        HidePopUp();
    }
}
