using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _nextButton;
    private Image _comics;

    [SerializeField]
    private AudioSource _slideSound;

    private int keyPressCounter=0;
    private int slidePressCounter = 1;

    void Update()
    {
        if (Input.anyKeyDown && _mainMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Debug.Log("QUIT");
                Application.Quit();
            }
            else {
                _comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
                _mainMenu.SetActive(false);
                _comics.enabled = true;
                StartCoroutine(ShowNextButton());
            }
        }
    }
    public void NextComics()
    {
        _nextButton.SetActive(false);
        _comics.enabled = false;
        if (keyPressCounter >= 4)
        {
            PlayGame();
            return;
        }
        _slideSound = GameObject.Find("SlideMusic" + (++slidePressCounter)).GetComponent<AudioSource>();
        _comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
        _comics.enabled = true;
        StartCoroutine(ShowNextButton());
    }
    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainLevel");
    }

    IEnumerator ShowNextButton()
    {
        _slideSound.Play();
        yield return new WaitForSeconds(2);
        var myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        _nextButton.SetActive(true);
    }
}
