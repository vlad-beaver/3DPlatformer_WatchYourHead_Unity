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

    [SerializeField]
    private AudioSource _slideSound;

    private int keyPressCounter=0;
    private int slidePressCounter = 1;

    void Update()
    {
        if (Input.anyKeyDown && mainMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Debug.Log("QUIT");
                Application.Quit();
            }
            else {
                comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
                mainMenu.SetActive(false);
                comics.enabled = true;
                StartCoroutine(ShowNextButton());
            }
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
        _slideSound = GameObject.Find("SlideMusic" + (++slidePressCounter)).GetComponent<AudioSource>();
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
        _slideSound.Play();
        yield return new WaitForSeconds(3);
        var myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        nextButton.SetActive(true);
    }
}
