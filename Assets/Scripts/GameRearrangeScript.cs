using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

class GameRearrangeScript : MonoBehaviour
{
    public static GameRearrangeScript instance;

    System.Random rnd = new System.Random();
    EdibleCreator edibleCreator = new EdibleCreator();
    
    Sprite correctAnsSpr, incorrectAnsSpr;
    Transform[] progressholder;
    Transform[] answerPositions;
    public Font gameFont;
    List<IEdible> answerEdible = new List<IEdible>();
    List<int> numberValue = new List<int>();
    List<string> edibleName = new List<string>();
    int Edible = 1;
    int progress = 1;
    int orderSet = 0;
    int tasksWon = 0;
    Transform[] Foodspawner;
    void Start()
    {
        ShopManager sm = new ShopManager();
        sm.SetGifts();
        Foodspawner = GameObject.Find("Foodholder").transform.GetComponentsInChildren<Transform>();
        correctAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Confirm");
        incorrectAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Close");
        progressholder = GameObject.Find("Progressholder_Progresses").transform.GetComponentsInChildren<Transform>();
        answerPositions = GameObject.Find("Answerholder").transform.GetComponentsInChildren<Transform>();
        instance = this;
        if (PlayerPrefs.GetInt("EdFruit") == 1)
        {
            Edible = 2;
        }
        StartCoroutine(StartRearrangeScene(0));
    }
    IEnumerator StartRearrangeScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        // clear previous data
        answerEdible.Clear();
        numberValue.Clear();
        edibleName.Clear();
        GameObject[] previousObjects = GameObject.FindGameObjectsWithTag("Edible");

        for (int i = 0; i < previousObjects.Length; i++)
        {
            Destroy(previousObjects[i]);
        }




        // generate  new data
        orderSet = rnd.Next(0, 2);
        for (int i = 0; i < 4; i++)
        {
            IEdible tempEdible;
            if (Edible > 1)
            {
                //dont repeat the same edible
                while (edibleName.Count <= i) {
                tempEdible = edibleCreator.GetRandomEdible(true);

                    if (!edibleName.Contains(tempEdible.EdName))
                    {
                        answerEdible.Add(tempEdible);
                        edibleName.Add(tempEdible.EdName);
                    }        
                }
            }
            else
            {
                while (edibleName.Count <= i)
                {
                    tempEdible = edibleCreator.GetRandomVeggie(true);

                    if (!edibleName.Contains(tempEdible.EdName))
                    {
                        answerEdible.Add(tempEdible);
                        edibleName.Add(tempEdible.EdName);
                    }
                }
            }
            while (numberValue.Count <= i)
            {
                int number = rnd.Next(1, 26);
                if (!numberValue.Contains(number))
                {
                    numberValue.Add(number);
                }
            }
        }
        if(orderSet < 1)
        {
            GameObject.Find("Instructions").GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("Rearrange-instructions-increase");
            foreach(Text t in GameObject.Find("Order").GetComponentsInChildren<Text>())
            {
                t.text = "<";
            }
        }
        else
        {
            GameObject.Find("Instructions").GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue("Rearrange-instructions-decrease");
            foreach (Text t in GameObject.Find("Order").GetComponentsInChildren<Text>())
            {
                t.text = ">";
            }
        }
        

        
        // Set visible edible positions with numbers attached
        for (int i = 0; i < answerEdible.Count; i++)
        {
            answerEdible[i].EdObject.GetComponent<RectTransform>().sizeDelta = new Vector2(145f, 145f);
            answerEdible[i].EdObject.transform.SetParent(GameObject.Find("Foodholder").transform, false);
            answerEdible[i].EdObject.transform.position = new Vector3(Foodspawner[i + 1].position.x, Foodspawner[i + 1].position.y, 1);
            answerEdible[i].EdObject.tag = "Edible";
            GameObject numberText = new GameObject();
            numberText.AddComponent<Text>();
            numberText.AddComponent<Outline>();
            numberText.GetComponent<Text>().text = numberValue[i].ToString();
            numberText.GetComponent<Text>().color = Color.white;
            numberText.GetComponent<Outline>().effectColor = Color.black;
            numberText.GetComponent<Outline>().effectDistance = new Vector2(2, 2);
            numberText.GetComponent<Text>().fontSize = 70;
            numberText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            numberText.GetComponent<Text>().font = gameFont;
            numberText.transform.SetParent(answerEdible[i].EdObject.transform);
            numberText.transform.localPosition = new Vector3(0, 0, 3);
            numberText.GetComponent<RectTransform>().sizeDelta = new Vector2(145f, 145f);
            numberText.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //handling drag end event to detect if all arranged numbers are correct
    public void OnDragEnd(GameObject draggedItem, Vector3 startPosi, Vector3 mousePosition)
    {
        if (Vector3.Distance(answerPositions[1].position, mousePosition) < 65 && answerPositions[1].childCount < 1)
        {
            draggedItem.transform.position = answerPositions[1].position;
            draggedItem.transform.parent = answerPositions[1];
            Destroy(draggedItem.GetComponent<Dragger>());
        }
        else if (Vector3.Distance(answerPositions[2].position, mousePosition) < 65 && answerPositions[2].childCount < 1)
        {
            draggedItem.transform.position = answerPositions[2].position;
            draggedItem.transform.parent = answerPositions[2];
            Destroy(draggedItem.GetComponent<Dragger>());
        }
        else if (Vector3.Distance(answerPositions[3].position, mousePosition) < 65 && answerPositions[3].childCount < 1)
        {
            draggedItem.transform.position = answerPositions[3].position;
            draggedItem.transform.parent = answerPositions[3];
            Destroy(draggedItem.GetComponent<Dragger>());
        }
        else if (Vector3.Distance(answerPositions[4].position, mousePosition) < 65 && answerPositions[4].childCount < 1)
        {
            draggedItem.transform.position = answerPositions[4].position;
            draggedItem.transform.parent = answerPositions[4];
            Destroy(draggedItem.GetComponent<Dragger>());
        }
        else
        {
            draggedItem.transform.position = startPosi;
        }

        Transform[] foods = GameObject.Find("Answerholder").transform.GetComponentsInChildren<Transform>();
        if(foods.Length > 12)
        {
            bool correct = false;
            int value1 = Int32.Parse(GameObject.Find("Answerholder").transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text);
            int value2 = Int32.Parse(GameObject.Find("Answerholder").transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text);
            int value3 = Int32.Parse(GameObject.Find("Answerholder").transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text);
            int value4 = Int32.Parse(GameObject.Find("Answerholder").transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text);
            if (orderSet < 1)
            {
                if (value1 < value2 && value2 < value3 && value3 < value4)
                {
                    correct = true;
                }
                else
                {
                    correct = false;
                }
            }
            else
            {
                if (value1 > value2 && value2 > value3 && value3 > value4)
                {
                    correct = true;
                }
                else
                {
                    correct = false;
                }
            }

            if (correct)
            {
                //GameObject.Find("TrueAnswer").GetComponent<Text>().text = "Correct ANS";
                SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.CorrectAns);
                progressholder[progress].GetComponent<Image>().color = Color.green;
                progressholder[progress + 1].GetComponent<Image>().sprite = correctAnsSpr;
                tasksWon++;
            }
            else
            {
                //GameObject.Find("TrueAnswer").GetComponent<Text>().text = "Fail!";
                SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.IncorrectAns);
                progressholder[progress].GetComponent<Image>().color = Color.red;
                progressholder[progress + 1].GetComponent<Image>().sprite = incorrectAnsSpr;

            }
            progressholder[progress + 1].GetComponent<Image>().color = Color.white;
            progress += 2;
            if (progress < 12)
            {
                StartCoroutine(StartRearrangeScene(0.3f));
            }
            else
            {
                PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney", 0) + tasksWon);
                SceneController.instance.LoadSceneGameList();
            }
        }
    }
}