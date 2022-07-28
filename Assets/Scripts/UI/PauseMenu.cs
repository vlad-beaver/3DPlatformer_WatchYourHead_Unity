using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private static bool _gameIsPaused = false;
    [SerializeField]
    private GameObject _pauseMenuUI;
    [SerializeField]
    private GameObject _authorsMenuUI;
    [SerializeField]
    private GameObject _inGameUI;
    [SerializeField]
    public AudioMixerGroup mixer;
    [SerializeField]
    public GameObject _checkMark;
    private float _volume;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Awake()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;

        mixer.audioMixer.GetFloat("MasterVolume", out _volume);
        if (_volume != 0)
        {
            _checkMark.SetActive(false);
        }
    }

    public void Resume()
    {
        _inGameUI.SetActive(true);
        _pauseMenuUI.SetActive(false);
        _authorsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        var myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
    void Pause()
    {
        _inGameUI.SetActive(false);
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void BackToMainMenu()
    {
        _gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    // For music setting in UI
    public void ToggleMusic(bool enabled)
    {
        if (enabled)
        {
            mixer.audioMixer.SetFloat("MasterVolume", 0);
            _checkMark.SetActive(true);
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
            _checkMark.SetActive(false);
        }
    }
}
