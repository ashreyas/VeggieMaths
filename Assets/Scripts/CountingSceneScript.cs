using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

class CountingSceneScript : MonoBehaviour
{
    public static CountingSceneScript instance;

    System.Random rnd = new System.Random();
    EdibleCreator edibleCreator = new EdibleCreator();

    Sprite correctAnsSpr, incorrectAnsSpr;
    Transform[] progressholder;
    int tasksWon = 0;
    int answer = 0;
    int Edible = 1;
    int progress = 1;
    List<IEdible> foodObjects = new List<IEdible>();
    Transform[] Foodspawner;
    bool answerClicked = false;
    void Start()
    {
        ShopManager sm = new ShopManager();
        sm.SetGifts();
        Foodspawner = GameObject.Find("Foodholder").transform.GetComponentsInChildren<Transform>();

        correctAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Confirm");
        incorrectAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Close");
        progressholder = GameObject.Find("Progressholder_Progresses").transform.GetComponentsInChildren<Transform>();

        if (PlayerPrefs.GetInt("EdFruit") == 1)
        {
            Edible = 2;
        }
        instance = this;


        StartCoroutine(StartCountingScene(0));
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && progress < 12 && !answerClicked)
        {
            try
            {
                if (EventSystem.current.currentSelectedGameObject.GetComponent<Text>())
                {
                    answerClicked = true;
                    if (EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text == answer.ToString())
                    {
                        //GameObject.Find("TrueAnswer").GetComponent<Text>().text = "Correct ANS";
                        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.CorrectAns);
                        progressholder[progress].GetComponent<Image>().color = Color.green;
                        progressholder[progress + 1].GetComponent<Image>().sprite = correctAnsSpr;
                        tasksWon++;
                    }
                    else
                    {
                        //GameObject.Find("TrueAnswer").GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text;
                        SoundManager.instance.GameAudioSrcObject.GetComponent<AudioSource>().PlayOneShot(SoundManager.instance.IncorrectAns);
                        progressholder[progress].GetComponent<Image>().color = Color.red;
                        progressholder[progress + 1].GetComponent<Image>().sprite = incorrectAnsSpr;
                    }
                    progressholder[progress + 1].GetComponent<Image>().color = Color.white;
                    progress += 2;
                    if (progress < 12)
                    {
                        StartCoroutine(StartCountingScene(0.3f));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney", 0) + tasksWon);
                        SceneController.instance.LoadSceneGameList();
                    }
                }
            }
            catch (NullReferenceException)
            {
            }
        }
    }

    IEnumerator StartCountingScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        // clear previous data
        foodObjects.Clear();
        GameObject[] previousObjects = GameObject.FindGameObjectsWithTag("Edible");
        List<GameObject> previousObjectsParent = new List<GameObject>();
        if (previousObjects.Length > 0)
            for (int i = 0; i < previousObjects.Length; i++)
            {
                previousObjectsParent.Add(previousObjects[i].transform.parent.gameObject);
            }

        for (int i = 0; i < previousObjectsParent.Count; i++)
        {
            Destroy(previousObjectsParent[i]);
        }




        // generate counting data
        IEdible answerEdible;
        answer = rnd.Next(1, 15);
        if (Edible > 1)
        {
            answerEdible = edibleCreator.GetRandomEdible(false);
        }
        else
        {
            answerEdible = edibleCreator.GetRandomVeggie(false);
        }
        foodObjects.Add(answerEdible);
        string edibleName = LocalizationManager.instance.GetLocalizedValue("Counting-instructions");
        edibleName = String.Format(edibleName, LocalizationManager.instance.GetLocalizedValue(answerEdible.EdName));

        GameObject.Find("Instructions").GetComponent<Text>().text = edibleName;
        for (int i = 0; i < answer-1; i++)
        {
            IEdible answerEdibleCopy = edibleCreator.GetEdible(answerEdible.EdName, false);
            foodObjects.Add(answerEdibleCopy);
        }




        // fill the random extra objects in list
        int dummyItemCount = 0;
        dummyItemCount = Foodspawner.Length - 1 - foodObjects.Count;
        for (int i = 0; i < dummyItemCount; i++)
        {
            IEdible dummyEdible;
            if (Edible > 1)
            {
                dummyEdible = edibleCreator.GetRandomEdible(false);
            }
            else
            {
                dummyEdible = edibleCreator.GetRandomVeggie(false);
            }
            if (dummyEdible.EdName != answerEdible.EdName)
            {
                foodObjects.Add(dummyEdible);
            }
            else i -= 1;
        }

        foodObjects = foodObjects.OrderBy(a => rnd.Next()).ToList();



        // fill the dummy and correct option for user
        GameObject[] userOptions = GameObject.FindGameObjectsWithTag("Options");
        int correctOption = rnd.Next(0, 4);
        int dummyNumber = 0;
        List<int> dummyNumberList = new List<int>();
        for (int i = 0; i < userOptions.Length; i++)
        {
            if (i != correctOption)
            {
                while (dummyNumberList.Count <= i)
                {
                    dummyNumber = rnd.Next(answer - 5, answer + 5);
                    if (dummyNumber < 0) dummyNumber = -dummyNumber;
                    if (!dummyNumberList.Contains(dummyNumber) && dummyNumber != answer)
                    {
                        dummyNumberList.Add(dummyNumber);
                    }
                }
                userOptions[i].GetComponent<Text>().text = dummyNumber.ToString();
            }
            else userOptions[i].GetComponent<Text>().text = answer.ToString();
        }

        // Set visible edible positions 
        for (int i = 0; i < foodObjects.Count; i++)
        {
            foodObjects[i].EdObject.GetComponent<RectTransform>().sizeDelta = new Vector2(145f, 145f);
            foodObjects[i].EdObject.transform.SetParent(GameObject.Find("Foodholder").transform, false);
            foodObjects[i].EdObject.transform.position = new Vector3(Foodspawner[i + 1].position.x, Foodspawner[i + 1].position.y, 1);

            GameObject fakeChild = new GameObject();
            fakeChild.tag = "Edible";
            fakeChild.transform.SetParent(foodObjects[i].EdObject.transform);
        }
        answerClicked = false;
    }
}