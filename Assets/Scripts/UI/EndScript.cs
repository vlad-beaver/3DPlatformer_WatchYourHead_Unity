using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [SerializeField]
    private GameObject endGameMenu;
    [SerializeField]
    private GameObject nextButton;

    private Image comics;

    private int keyPressCounter = 0;

    public void NextComics()
    {
        nextButton.SetActive(false);
        comics.enabled = false;
        if (keyPressCounter >= 2)
        {
            endGameMenu.SetActive(true);
        }
        else
        {
            comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
            comics.enabled = true;
            StartCoroutine(ShowNextButton());
        }
    }

    private void Start()
    {
        comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
        comics.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(ShowNextButton());
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    IEnumerator ShowNextButton()
    {
        yield return new WaitForSeconds(3);
        nextButton.SetActive(true);
    }

}
