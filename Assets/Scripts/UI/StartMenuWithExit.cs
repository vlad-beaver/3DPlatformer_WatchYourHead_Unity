using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuWithExit : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject nextButton;

    [SerializeField]
    private AudioSource _slideSound;

    public void BackToMainMenu1()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame1()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
