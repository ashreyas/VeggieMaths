using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {
    Sprite[] sprites;

    Dictionary<string, Sprite> SpritesList = new Dictionary<string, Sprite>();


// Use this for initialization
void Awake () {
        int pref = PlayerPrefs.GetInt("Character", 0);
        
        GameObject.Find("ToggleGroup_Character").transform.GetChild(pref).GetComponent<Toggle>().isOn = true;

    }

    public void SaveCharacterPref(int pref)
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.CheckSound);
        PlayerPrefs.SetInt("Character", pref);
        PlayerPrefs.Save();
    }
    public Sprite GetCharacterAvatar()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Characters/Characters");
        for (int i = 0; i < sprites.Length; i++)
        {
            SpritesList.Add(sprites[i].name, sprites[i]);
        }

        int pref = PlayerPrefs.GetInt("Character", 0);
        Sprite avatar;
        switch (pref)
        {
            case 0:
                avatar = SpritesList["Boy_hands"];
                break;

            case 1:
                avatar = SpritesList["Girl_hands"]; 
                break;

            case 2:
                avatar = SpritesList["Bull_hands"];
                break;

            case 3:
                avatar = SpritesList["Tiger_hands"];
                break;

            case 4:
                avatar = SpritesList["Koala_hands"];
                break;

            case 5:
                avatar = SpritesList["Pig_hands"];
                break;

            default:
                avatar = SpritesList["Boy_hands"];
                break;
        }
        return avatar;
    }
}
