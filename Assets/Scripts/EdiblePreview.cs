using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdiblePreview : MonoBehaviour
{
    //public Sprite[] icons;
    GameObject iconText;  
    public Font fft;


    // Use this for initialization
    void Awake()
    {
        EdibleCreator edibleCreator = new EdibleCreator();
        List<IEdible> allEdibles = edibleCreator.GetAllEdible();
        foreach (IEdible edible in allEdibles)
        {
            edible.EdObject.transform.SetParent(this.gameObject.transform);
            edible.EdObject.GetComponent<RectTransform>().sizeDelta = new Vector2(400f, 400f);
            //edible.EdObject.GetComponent<Image>.pre
            iconText = new GameObject(edible.EdName+"_Text");
            iconText.transform.SetParent(edible.EdObject.transform);
            iconText.AddComponent<Text>();
            iconText.GetComponent<Text>().color = Color.white;
            iconText.GetComponent<Text>().font = fft;
            iconText.GetComponent<Text>().text = edible.EdName;
            //iconText.GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue(edible.EdName);
            iconText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            iconText.GetComponent<Text>().fontSize = 70;
            iconText.AddComponent<Shadow>().useGraphicAlpha = true;
            iconText.GetComponent<Shadow>().effectDistance = new Vector2(0, -4);
            iconText.GetComponent<Shadow>().effectColor = new Color(78f, 76f, 74f, 255f);
            iconText.AddComponent<Outline>().useGraphicAlpha = true;
            iconText.GetComponent<Outline>().effectDistance = new Vector2(2, 2);
            iconText.GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 128f);
            iconText.GetComponent<RectTransform>().sizeDelta = new Vector2(400f, 100f);
            iconText.transform.position = iconText.transform.position + new Vector3(0, -200, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}