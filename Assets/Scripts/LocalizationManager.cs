using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText = new Dictionary<string, string>();
    public bool isReady = false;
    private string missingTextString = "Localized text not found";
    public Text debugText;
    string fileName;
    public string currentLanguage = "EN";

    // Use this for initialization
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("LanguageOption")) PlayerPrefs.SetString("LanguageOption", "EN");
        if (!PlayerPrefs.HasKey("EdVeg")) PlayerPrefs.SetInt("EdVeg", 1);
        if (!PlayerPrefs.HasKey("EdFruit")) PlayerPrefs.SetInt("EdFruit", 1);
        if (!PlayerPrefs.HasKey("MusicOption")) PlayerPrefs.SetInt("MusicOption", 1);
        if (!PlayerPrefs.HasKey("GSoundOption")) PlayerPrefs.SetInt("GSoundOption", 1);
        if (!PlayerPrefs.HasKey("PlayerMoney")) PlayerPrefs.SetInt("PlayerMoney", 0); 
        if (!PlayerPrefs.HasKey("Character")) PlayerPrefs.SetInt("Character", 0);
        if (!PlayerPrefs.HasKey("Jacket1")) PlayerPrefs.SetInt("Jacket1", 0);
        if (!PlayerPrefs.HasKey("Jacket2")) PlayerPrefs.SetInt("Jacket2", 0);
        if (!PlayerPrefs.HasKey("Jacket3")) PlayerPrefs.SetInt("Jacket3", 0);
        if (!PlayerPrefs.HasKey("Cap1")) PlayerPrefs.SetInt("Cap1", 0);
        if (!PlayerPrefs.HasKey("Cap2")) PlayerPrefs.SetInt("Cap2", 0);
        if (!PlayerPrefs.HasKey("Cap3")) PlayerPrefs.SetInt("Cap3", 0);
        if (!PlayerPrefs.HasKey("Cap4")) PlayerPrefs.SetInt("Cap4", 0);
        if (!PlayerPrefs.HasKey("Mask1")) PlayerPrefs.SetInt("Mask1", 0);
        if (!PlayerPrefs.HasKey("Mask2")) PlayerPrefs.SetInt("Mask2", 0);
        if (!PlayerPrefs.HasKey("Mask3")) PlayerPrefs.SetInt("Mask3", 0);
        if (!PlayerPrefs.HasKey("Mask4")) PlayerPrefs.SetInt("Mask4", 0);
        if (!PlayerPrefs.HasKey("Mask5")) PlayerPrefs.SetInt("Mask5", 0);
        if (!PlayerPrefs.HasKey("Jacket")) PlayerPrefs.SetString("Jacket", "0");
        if (!PlayerPrefs.HasKey("Cap")) PlayerPrefs.SetString("Cap", "0");
        if (!PlayerPrefs.HasKey("Mask")) PlayerPrefs.SetString("Mask", "0");
        PlayerPrefs.Save();

        currentLanguage = PlayerPrefs.GetString("LanguageOption", "EN");
            StartCoroutine(LoadLocalizedText(currentLanguage));


        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    public IEnumerator LoadLocalizedText(string lang)
    {
        isReady = false;
                  fileName = "localizedText_en.json";
            if (lang.Equals("EN"))
                fileName = "localizedText_en.json";
            else
            {
                fileName = "localizedText_es.json";
            }
            // Debug.Log(fileName);
            localizedText.Clear();
            string filePath;
            filePath = Path.Combine(Application.streamingAssetsPath + "/", fileName);
            string dataAsJson = "";
            if (filePath.Contains("://") || filePath.Contains(":///"))
            {
                UnityWebRequest www = UnityWebRequest.Get(filePath);
                yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
                Debug.LogError(System.Environment.NewLine + www.error);
            else
                dataAsJson = www.downloadHandler.text;
            }
            else
            {
                dataAsJson = File.ReadAllText(filePath);
            }
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            isReady = true;
    }
   
    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result; 
    }
}
