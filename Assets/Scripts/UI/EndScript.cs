using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [SerializeField]
    private GameObject endGameMenu;
    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject nextButton;

    private Image comics;

    private int keyPressCounter = 0;

    public void NextComics()
    {
        nextButton.SetActive(false);
        comics.enabled = false;
        if (keyPressCounter >= 3)
        {
            Time.timeScale = 0f;
            endGameMenu.SetActive(true);
        }
        else
        {
            comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
            comics.enabled = true;
            StartCoroutine(ShowNextButton());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        inGameUI.SetActive(false);
        comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
        comics.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(ShowNextButton());
    }
    void OnTriggerStay(Collider other)
    {
        
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object Exited the trigger");
    }
    IEnumerator ShowNextButton()
    {
        yield return new WaitForSeconds(3);
        nextButton.SetActive(true);
    }

}
