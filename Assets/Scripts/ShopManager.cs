using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    
    Sprite[] sprites;

    Dictionary<string, Sprite> SpritesList = new Dictionary<string, Sprite>();
    GameObject popup;
    string buyingItem;
    // Use this for initialization
    void Start () {
        popup = GameObject.Find("PopUp");
        popup.SetActive(false);

        if (PlayerPrefs.GetInt("Jacket1", 0) > 0) Destroy(GameObject.Find("Toggle_Jacket1").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Jacket2", 0) > 0) Destroy(GameObject.Find("Toggle_Jacket2").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Jacket3", 0) > 0) Destroy(GameObject.Find("Toggle_Jacket3").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Cap1", 0) > 0) Destroy(GameObject.Find("Toggle_Cap1").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Cap2", 0) > 0) Destroy(GameObject.Find("Toggle_Cap2").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Cap3", 0) > 0) Destroy(GameObject.Find("Toggle_Cap3").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Cap4", 0) > 0) Destroy(GameObject.Find("Toggle_Cap4").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Mask1", 0) > 0) Destroy(GameObject.Find("Toggle_Mask1").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Mask2", 0) > 0) Destroy(GameObject.Find("Toggle_Mask2").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Mask3", 0) > 0) Destroy(GameObject.Find("Toggle_Mask3").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Mask4", 0) > 0) Destroy(GameObject.Find("Toggle_Mask4").transform.GetChild(1).gameObject);
        if (PlayerPrefs.GetInt("Mask4", 0) > 0) Destroy(GameObject.Find("Toggle_Mask4").transform.GetChild(1).gameObject);


        if (PlayerPrefs.GetString("Jacket")!= "0") GameObject.Find("Toggle_" + PlayerPrefs.GetString("Jacket")).transform.GetComponent<Toggle>().isOn = true;
        if (PlayerPrefs.GetString("Cap") != "0") GameObject.Find("Toggle_" + PlayerPrefs.GetString("Cap")).transform.GetComponent<Toggle>().isOn = true;
        if (PlayerPrefs.GetString("Mask") != "0") GameObject.Find("Toggle_" + PlayerPrefs.GetString("Mask")).transform.GetComponent<Toggle>().isOn = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HidePopUp()
    {
        popup.SetActive(false);
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BackSound);
    }
    public void ShowPopUp()
    {
        popup.SetActive(true);
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BackSound);
    }
    public void ConfirmPurchase()
    {
        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.BtnClick);
        PlayerPrefs.SetInt(buyingItem, 1);
        PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney") - GetItemCost(buyingItem));
        PlayerPrefs.Save();
        HidePopUp();
        Destroy(GameObject.Find("Toggle_" + buyingItem).transform.GetChild(1).gameObject);
        GameObject.Find("Text_Coins").GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerMoney").ToString();
        GameObject.Find("Toggle_" + buyingItem).transform.GetComponent<Toggle>().isOn = true;
        PlayerPrefs.SetString(buyingItem.Remove(buyingItem.Length - 1), buyingItem);
    }
    public void InitiatePurchase(string item)
    {
        if (PlayerPrefs.GetInt(item, 0) > 0)
        {
            SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.CheckSound);
            PlayerPrefs.SetString(item.Remove(item.Length - 1), item);
            if(!GameObject.Find("ToggleGroup_" + (item.Remove(item.Length - 1))).transform.GetComponent<ToggleGroup>().AnyTogglesOn())
            {
                PlayerPrefs.SetString(item.Remove(item.Length - 1), "0");
            }
            return;
        }
        else
        {
            GameObject.Find("Toggle_"+ item).transform.GetComponent<Toggle>().isOn = false;
            if (PlayerPrefs.GetInt("PlayerMoney") < GetItemCost(item)) return;
            ShowPopUp();
            GameObject.Find("Purchase_Cost").GetComponent<Text>().text = GetItemCost(item).ToString();
            buyingItem = item;
        }
    }
    public void SetGifts()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Characters/Gifts");
        for (int i = 0; i < sprites.Length; i++)
        {
            SpritesList.Add(sprites[i].name, sprites[i]);
        }
        string jacket = PlayerPrefs.GetString("Jacket");
        string cap = PlayerPrefs.GetString("Cap");
        string mask = PlayerPrefs.GetString("Mask");
        Sprite tempSprite;
            if(jacket != "0")
            {
                tempSprite = GetItemSprite(jacket);
                if (tempSprite) {
                    GameObject.Find("Jacket").GetComponent<Image>().sprite = tempSprite;
                    GameObject.Find("Jacket").GetComponent<Image>().color = new Color(255, 255, 255, 255);
                }
            }
            if (cap != "0")
            {
                tempSprite = GetItemSprite(cap);
                if (tempSprite)
                {
                    GameObject.Find("Cap").GetComponent<Image>().sprite = tempSprite;
                    GameObject.Find("Cap").GetComponent<Image>().color = new Color(255, 255, 255, 255);
                }
            }
            if (mask != "0")
            {
                tempSprite = GetItemSprite(mask);
                if (tempSprite)
                {
                    GameObject.Find("Mask").GetComponent<Image>().sprite = tempSprite;
                    GameObject.Find("Mask").GetComponent<Image>().color = new Color(255, 255, 255, 255);
                }
            }
    }

    int GetItemCost(string item)
    {
        int cost;
        switch (item)
        {
            case "Jacket1":
                cost = 10;
                break;

            case "Jacket2":
                cost = 20;
                break;

            case "Jacket3":
                cost = 50;
                break;

            case "Cap1":
                cost = 20;
                break;

            case "Cap2":
                cost = 50;
                break;

            case "Cap3":
                cost = 70;
                break;

            case "Cap4":
                cost = 90;
                break;

            case "Mask1":
                cost = 20;
                break;

            case "Mask2":
                cost = 50;
                break;

            case "Mask3":
                cost = 70;
                break;

            case "Mask4":
                cost = 90;
                break;

            case "Mask5":
                cost = 110;
                break;

            default:
                cost = 2;
                break;
        }
        return cost;
    }
    Sprite GetItemSprite(string item)
    {
        Sprite gift;
        if(SpritesList.TryGetValue(item, out gift)) {
            return gift;
        }

        return null;
    }
}
