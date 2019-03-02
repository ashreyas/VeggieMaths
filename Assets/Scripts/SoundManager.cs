using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    private bool AudioToggle = true;  // toggle to indicate whether change is requested or not
    private bool BGMusicToggle = true;  // toggle to indicate whether music is playing or not
    private bool GSoundToggle = true;  // toggle to indicate whether music is playing or not
    public GameObject BGAudioSrcObject; //Background Music Audio object
    public GameObject GameAudioSrcObject; //Background Music Audio object
    public Sprite AudioOnSprite;
    public Sprite AudioOffSprite;
    public AudioClip CorrectAns;
    public AudioClip IncorrectAns;
    public AudioClip CheckSound;
    public AudioClip BackSound;
    public AudioClip BtnClick;
    public bool AudioIconEnabled = true;
    private bool InternalReq = true;
    // Use this for initialization
    void Awake ()
    {
        BGAudioSrcObject = GameObject.Find("BGAudio");
        GameAudioSrcObject = GameObject.Find("GameAudio");


        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);

    }
    private void Start()
    {
        BGMusicToggle = (PlayerPrefs.GetInt("MusicOption", 1) > 0) ? true : false;
        GSoundToggle = (PlayerPrefs.GetInt("GSoundOption", 1) > 0) ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioToggle)
        {


            if (BGMusicToggle && AudioIconEnabled)
            {
                BGAudioSrcObject.GetComponent<AudioSource>().mute = false;
            }
            else if (!BGMusicToggle || !AudioIconEnabled)
            {
                BGAudioSrcObject.GetComponent<AudioSource>().mute = true;
            }

            if (GSoundToggle && AudioIconEnabled)
            {
                if(!GameAudioSrcObject || GameAudioSrcObject == null) GameAudioSrcObject = GameObject.Find("GameAudio");
                GameAudioSrcObject.GetComponent<AudioSource>().mute = false;
            }
            else if(!GSoundToggle || !AudioIconEnabled)
            {
                if (!GameAudioSrcObject || GameAudioSrcObject == null) GameAudioSrcObject = GameObject.Find("GameAudio");
                GameAudioSrcObject.GetComponent<AudioSource>().mute = true;
            }

            if (AudioIconEnabled && !InternalReq)
            {
                GameObject.Find("Icon_Volume").GetComponent<Image>().sprite = AudioOnSprite;
                GameObject.Find("Icon_Volume").GetComponent<Image>().color = Color.white;
            }
            else if (!AudioIconEnabled && !InternalReq)
            {
                GameObject.Find("Icon_Volume").GetComponent<Image>().sprite = AudioOffSprite;
                GameObject.Find("Icon_Volume").GetComponent<Image>().color = Color.black;
            }
            AudioToggle = false;
        }
    }

    public void ToggleAudio() {
        AudioToggle = true;
        InternalReq = false;
        AudioIconEnabled = !AudioIconEnabled;
    }

    public void ToggleMusicAudio(bool bgmusic)
    {
        BGMusicToggle = bgmusic;
        AudioToggle = true;
        InternalReq = true;

    }
    public void ToggleGameAudio(bool isGSound)
    {
        GSoundToggle = isGSound;
        AudioToggle = true;
        InternalReq = true;
    }
}
