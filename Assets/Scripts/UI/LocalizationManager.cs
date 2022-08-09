using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UnityEditor;

public class LocalizationManager : MonoBehaviour
{
    public static int SelectedLanguage { get; private set; }

    public delegate void LanguageChangeHandler();
    public static event LanguageChangeHandler OnLanguageChange;

    private static Dictionary<string, List<string>> localization;
    [SerializeField]
    private TextAsset textFile;

    private void Awake()
    {
        if (localization == null)
        {
            LoadLocalization();
        }
    }
    public void SetLanguage(int id)
    {
        SelectedLanguage = id;

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textFile.text);
        xmlDocument["Keys"].Attributes["CurrentLanguage"].Value = id.ToString();
        xmlDocument.Save(textFile.name);

        OnLanguageChange?.Invoke();
    }
    private void LoadLocalization()
    {
        localization = new Dictionary<string, List<string>>();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textFile.text);
        int currentLanguage = int.Parse(xmlDocument["Keys"].Attributes["CurrentLanguage"].Value); 

        foreach (XmlNode key in xmlDocument["Keys"].ChildNodes)
        {
            string keyStr = key.Attributes["Name"].Value;

            var values = new List<string>();
            foreach (XmlNode translate in key["Translates"].ChildNodes)
            {
                values.Add(translate.InnerText);
            }
            localization[keyStr] = values;
        }
        SetLanguage(currentLanguage);
    }
    public static string GetTranslate(string key, int languageId = -1)
    {
        if(languageId == -1)
        {
            languageId = SelectedLanguage;
        }
        if (localization.ContainsKey(key))
        {
            return localization[key][languageId];
        }
        return key;
    }
}
