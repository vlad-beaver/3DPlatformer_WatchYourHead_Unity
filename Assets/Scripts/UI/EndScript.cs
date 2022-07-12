using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    [SerializeField]
    private GameObject endGameMenu;
    [SerializeField]
    private GameObject inGameUI;
    void OnTriggerEnter(Collider other)
    {

    }
    void OnTriggerStay(Collider other)
    {
        //PuzzlePlayer.Instance.Kill();
        Time.timeScale = 0f;
        endGameMenu.SetActive(true);
        inGameUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object Exited the trigger");
    }
    

}
