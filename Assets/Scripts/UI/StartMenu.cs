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
    private GameObject _nextText;
    private Image _comics;

    [SerializeField]
    private AudioSource _slideSound;
    [SerializeField]
    private GameObject _comics3Text;
    [SerializeField]
    private GameObject _comics4Text;
    private int keyPressCounter=0;
    private int slidePressCounter = 1;

    void Update()
    {
        if (Input.anyKeyDown && _mainMenu.activeSelf && !Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Debug.Log("QUIT");
                Application.Quit();
            }
            else {
                _comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
                _mainMenu.SetActive(false);
                _comics.enabled = true;
                StartCoroutine(ShowNextText());
            }
        }else if (Input.anyKeyDown && _nextText.activeSelf)
        {
            _comics3Text.SetActive(false);
            _comics4Text.SetActive(false);
            _nextText.SetActive(false);
            _comics.enabled = false;
            if (keyPressCounter >= 4)
            {
                PlayGame();
                return;
            }
            _slideSound = GameObject.Find("SlideMusic" + (++slidePressCounter)).GetComponent<AudioSource>();
            _comics = GameObject.Find("Comics" + (++keyPressCounter)).GetComponent<Image>();
            _comics.enabled = true;
            if (keyPressCounter == 3)
            {
                _comics3Text.SetActive(true);
            }
            if (keyPressCounter == 4)
            {
                _comics4Text.SetActive(true);
            }
            StartCoroutine(ShowNextText());
        }
    }
    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainLevel");
    }

    IEnumerator ShowNextText()
    {
        _slideSound.Play();
        yield return new WaitForSeconds(2);
        var myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        _nextText.SetActive(true);
    }
}
