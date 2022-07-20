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
    private bool gameEnd = false;

    private Image comics;

    private int keyPressCounter = 0;
    void Update()
    {
        if (Input.anyKeyDown && gameEnd)
        {
            if (keyPressCounter >= 3)
            {
                comics.enabled = false;
                endGameMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                comics.enabled = false;
                comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
                comics.enabled = true;
            }
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        gameEnd = true;
        comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
        comics.enabled = true;
    }
    void OnTriggerStay(Collider other)
    {
        Time.timeScale = 0f;
        inGameUI.SetActive(false);
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object Exited the trigger");
    }
    

}
