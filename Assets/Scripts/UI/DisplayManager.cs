using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _display;
    [SerializeField]
    private string _changeImage;

    private readonly string _pathEng = "Display/Images/English/";
    private readonly string _pathRussian = "Display/Images/Russian/";
    private readonly string _pathBelarusian = "Display/Images/Belarusian/";

    private void Start()
    {
        Texture2D texture;
        switch (LocalizationManager.SelectedLanguage)
        {
            case 0:
                texture = (Texture2D)Resources.Load(_pathEng + _changeImage);
                _display.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                break;
            case 1:
                texture = (Texture2D)Resources.Load(_pathRussian + _changeImage);
                _display.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                break;
            case 2:
                texture = (Texture2D)Resources.Load(_pathBelarusian + _changeImage);
                _display.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                break;
        }


    }
}
