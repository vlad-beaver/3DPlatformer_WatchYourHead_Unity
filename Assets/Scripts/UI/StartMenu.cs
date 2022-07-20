using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject nextButton;
    private Image comics;

    private int keyPressCounter=0;
    void Update()
    {
        if (Input.anyKeyDown && mainMenu.activeSelf)
        {
            comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
            mainMenu.SetActive(false);
            comics.enabled = true;
            StartCoroutine(ShowNextButton());
        }
    }
    public void NextComics()
    {
        nextButton.SetActive(false);
        comics.enabled = false;
        if (keyPressCounter >= 4)
        {
            PlayGame();
            return;
        }
        comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
        comics.enabled = true;
        StartCoroutine(ShowNextButton());
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
    IEnumerator ShowNextButton()
    {
        yield return new WaitForSeconds(3);
        nextButton.SetActive(true);
    }
}
