using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    GameObject popup;
    void Awake()
    {
        GameObject.Find("Text_Coins").GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerMoney", 0).ToString();
        instance = this;

    }
    void Start()
    {
        if(GameObject.Find("PopUp_Exit") != null)
        { 
            popup = GameObject.Find("PopUp_Exit");
            popup.SetActive(false);
        }
        if (GameObject.Find("Character_Posi") != null)
        {
            CharacterManager CM = new CharacterManager();
            GameObject.Find("Character_Posi").GetComponent<Image>().sprite = CM.GetCharacterAvatar();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "LandingScene")
            {
                Application.Quit();
            }
            if (SceneManager.GetActiveScene().name == "GameListScene")
            {
                SceneManager.LoadScene("LandingScene");
            }
            if (SceneManager.GetActiveScene().name == "SettingsScene")
            {
                SceneManager.LoadScene("LandingScene");
            }
            if (SceneManager.GetActiveScene().name == "CharacterScene")
            {
                SceneManager.LoadScene("LandingScene");
            }
            if (SceneManager.GetActiveScene().name == "ShopScene")
            {
                SceneManager.LoadScene("LandingScene");
            }
            if (SceneManager.GetActiveScene().name == "GameMathScene")
            {
                SceneManager.LoadScene("GameListScene");
            }
            if (SceneManager.GetActiveScene().name == "GameRecipeScene")
            {
                SceneManager.LoadScene("GameListScene");
            }
            if (SceneManager.GetActiveScene().name == "GameOdd1OutScene")
            {
                SceneManager.LoadScene("GameListScene");
            }
            if (SceneManager.GetActiveScene().name == "GameCountingScene")
            {
                SceneManager.LoadScene("GameListScene");
            }
            if (SceneManager.GetActiveScene().name == "GameRearrangeScene")
            {
                SceneManager.LoadScene("GameListScene");
            }
            if (SceneManager.GetActiveScene().name == "GameMemoryScene")
            {
                SceneManager.LoadScene("GameListScene");
            }
        }
    }

    public void ShowGameExitWarning()
    {
        popup.SetActive(true);
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BackSound);
    }
    public void HideGameExitWarning()
    {
        popup.SetActive(false);
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BackSound);
    }

    public void LoadSceneGameList()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("GameListScene");
        //GameObject.Find("PopUp").SetActive(false);
    }
    public void LoadSceneLanding()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BackSound);
        SceneManager.LoadScene("LandingScene");
    }
    public void LoadSceneSettings()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("SettingsScene");
    }
    public void LoadSceneCharacter()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("CharacterScene");
    }
    public void LoadSceneShop()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("ShopScene");
    }
    public void LoadSceneGameMath()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("GameMathScene");
    }
    public void LoadSceneGameRecipe()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("GameRecipeScene");
    }
    public void LoadSceneOdd1Out()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("GameOdd1OutScene");
    }
    public void LoadSceneCounting()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("GameCountingScene");
    }
    public void LoadSceneRearrange()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("GameRearrangeScene");
    }
    public void LoadSceneMemory()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("GameMemoryScene");
    }

}