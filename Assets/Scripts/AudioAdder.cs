using UnityEngine;
using UnityEngine.UI;

public class AudioAdder : MonoBehaviour {

    // Use this for initialization
    void Start() {
        GameObject.Find("Button_Volume").GetComponent<Button>().onClick.AddListener(SoundManager.instance.ToggleAudio);
        if (!SoundManager.instance.AudioIconEnabled)
        {
            GameObject.Find("Icon_Volume").GetComponent<Image>().sprite = SoundManager.instance.AudioOffSprite;
            GameObject.Find("Icon_Volume").GetComponent<Image>().color = Color.black;
        }
        else if (SoundManager.instance.AudioIconEnabled)
        {
            GameObject.Find("Icon_Volume").GetComponent<Image>().sprite = SoundManager.instance.AudioOnSprite;
            GameObject.Find("Icon_Volume").GetComponent<Image>().color = Color.white;

        }
    }
	
}
