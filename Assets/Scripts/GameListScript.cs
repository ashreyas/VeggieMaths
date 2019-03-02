using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameListScript : MonoBehaviour
{
    GameObject popup;
    int operationSet;
    int fruitEnabled = 0;
    public void Awake()
    {
        popup = GameObject.Find("PopUp");
        popup.SetActive(false);
        if (PlayerPrefs.GetInt("EdFruit") != 1)
        {
            GameObject.Find("Panel_GameCard5").GetComponent<Button>().interactable = false;
        }
    }
    public void ShowPopUp()
    {
        popup.SetActive(true);
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
    }

    public void HidePopUp()
    {
        popup.SetActive(false);
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BackSound);
    }
    public void SetSceneSettings(int operationSet)
    {
        this.operationSet = operationSet;
        PlayerPrefs.SetInt("NextGame", operationSet);
        ShowPopUp();
    }
    public void SetComplexityLevel(int complexity)
    {
        PlayerPrefs.SetInt("GameComplexity", complexity);
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("GameMathScene");
    }
}
