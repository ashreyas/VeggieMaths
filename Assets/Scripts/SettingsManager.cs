using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    Toggle musicOnToggle, gSoundOnToggle, languageEnToggle;
    bool isMusic, isGSound, isVegetables, isFruits, changeLangReq = false;
    string language;

    void Awake()
    {
        if (PlayerPrefs.GetString("LanguageOption", "EN").Equals("EN"))
        {
            language = "EN";
            GameObject.Find("ToggleGroup_languages").transform.GetChild(0).GetComponent<Toggle>().isOn = true; language = "EN";
            GameObject.Find("ToggleGroup_languages").transform.GetChild(1).GetComponent<Toggle>().isOn = false;
        }
        else
        {
            language = "ES";
            GameObject.Find("ToggleGroup_languages").transform.GetChild(0).GetComponent<Toggle>().isOn = false; language = "ES";
            GameObject.Find("ToggleGroup_languages").transform.GetChild(1).GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("MusicOption") == 1)
        {
            GameObject.Find("ToggleGroup_Music").transform.GetChild(0).GetComponent<Toggle>().isOn = true; isMusic = true;
            GameObject.Find("ToggleGroup_Music").transform.GetChild(1).GetComponent<Toggle>().isOn = false;
        }
        else
        {
            GameObject.Find("ToggleGroup_Music").transform.GetChild(0).GetComponent<Toggle>().isOn = false; isMusic = false;
            GameObject.Find("ToggleGroup_Music").transform.GetChild(1).GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("GSoundOption") == 1)
        {
            GameObject.Find("ToggleGroup_Sound").transform.GetChild(0).GetComponent<Toggle>().isOn = true; isGSound = true;
            GameObject.Find("ToggleGroup_Sound").transform.GetChild(1).GetComponent<Toggle>().isOn = false;
        }
        else
        {
            GameObject.Find("ToggleGroup_Sound").transform.GetChild(0).GetComponent<Toggle>().isOn = false; isGSound = false;
            GameObject.Find("ToggleGroup_Sound").transform.GetChild(1).GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("EdVeg") == 1)
        {
            GameObject.Find("ToggleGroup_Ed").transform.GetChild(0).GetComponent<Toggle>().isOn = true; isVegetables = true;
        }
        if (PlayerPrefs.GetInt("EdFruit") == 1)
        {
            GameObject.Find("ToggleGroup_Ed").transform.GetChild(1).GetComponent<Toggle>().isOn = true; isFruits = true;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveSettings();
        }
    }
    public void ChangeEd()
    {
        isVegetables = GameObject.Find("ToggleGroup_Ed").transform.GetChild(0).GetComponent<Toggle>().isOn ? true : false;
        isFruits = GameObject.Find("ToggleGroup_Ed").transform.GetChild(1).GetComponent<Toggle>().isOn ? true : false;
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.CheckSound);
    }
    public void ChangeMusic()
    {
        musicOnToggle = GameObject.Find("ToggleGroup_Music").transform.GetChild(0).GetComponent<Toggle>();
        isMusic = musicOnToggle.isOn ? true : false;
        SoundManager.instance.ToggleMusicAudio(isMusic);
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.CheckSound);
    }
    public void ChangeGSound()
    {
        gSoundOnToggle = GameObject.Find("ToggleGroup_Sound").transform.GetChild(0).GetComponent<Toggle>();
        isGSound = gSoundOnToggle.isOn ? true : false;
        SoundManager.instance.ToggleGameAudio(isGSound);
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.CheckSound);
    }
    public void ChangeLang()
    {
        changeLangReq = true;
           languageEnToggle = GameObject.Find("ToggleGroup_languages").transform.GetChild(0).GetComponent<Toggle>();
        language = languageEnToggle.isOn ? "EN" : "ES";
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.CheckSound);
    }

    public void SaveSettings()
    {
        if (changeLangReq)
        {
            LocalizationManager.instance.isReady = false;
            StartCoroutine(StartLocalization());
        }
        PlayerPrefs.SetInt("EdVeg", isVegetables ? 1 : 0);
        PlayerPrefs.SetInt("EdFruit", isFruits ? 1 : 0);
        PlayerPrefs.SetInt("MusicOption", isMusic ? 1 : 0);
        PlayerPrefs.SetInt("GSoundOption", isGSound ? 1 : 0);
        PlayerPrefs.SetString("LanguageOption", language);
        PlayerPrefs.Save();
    }

    public IEnumerator StartLocalization()
    {
        StartCoroutine(LocalizationManager.instance.LoadLocalizedText(language));
        while (!LocalizationManager.instance.isReady)
            yield return new WaitForSeconds(0.1f);
        changeLangReq = false;
    }
}