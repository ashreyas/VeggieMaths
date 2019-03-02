using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class GameMathsScript : MonoBehaviour {


    int answer, lvl, operationSet, progress=1;
    System.Random rnd = new System.Random();
    Sprite correctAnsSpr, incorrectAnsSpr;
    Transform[] progressholder;
    bool answerClicked = false;
    int tasksWon = 0;


    // Use this for initialization
    void Start() {
        ShopManager sm = new ShopManager();
        sm.SetGifts();
        lvl = PlayerPrefs.GetInt("GameComplexity", 1);

        operationSet = PlayerPrefs.GetInt("NextGame", 1);


        StartCoroutine(StartGameScene1Settings(lvl, operationSet, 0));

        correctAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Confirm");
        incorrectAnsSpr = Resources.Load<Sprite>("Sprites/Media/UI_Icon_Close");
        progressholder = GameObject.Find("Progressholder_Progresses").transform.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
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
                        ////GameObject.Find("TrueAnswer").GetComponent<Text>().text = "Correct ANS";
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
                        StartCoroutine(StartGameScene1Settings(lvl, operationSet, 0.5f));
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

    IEnumerator StartGameScene1Settings(int lvl, int operationSet, float delay)
    {
        yield return new WaitForSeconds(delay);
        //delete previous data
        this.lvl = lvl;
        this.operationSet = operationSet;
        GameObject[] previousObjects = GameObject.FindGameObjectsWithTag("Edible");
        if (previousObjects.Length > 0)
        for (int i = 0; i < previousObjects.Length; i++)
        {
                Destroy(previousObjects[i]);
        }



        int Edible = 1;
        if (PlayerPrefs.GetInt("EdFruit") == 1)
        {
            Edible = rnd.Next(1, 3);
        }
        int number1, number2, operation;
        string operationString;

        operation = rnd.Next(1, 3);  //decide addition or subtraction / multiplication or division

        if (operationSet > 1)
        {
            operationString = operation < 2 ? "x" : "÷";   //check if multiplication or division and set the string of that operation
        }
        else
        {
            operationString = operation < 2 ? "+" : "-";   //check if addition or subtraction and set the string of that operation

        }

        GameObject.Find("Operation").GetComponent<Text>().text = operationString;


        switch (lvl)
        {
            case 1:
                number1 = rnd.Next(1, 3);

                if (operation > 1) { 
                    if (operationSet > 1) { 
                        List<int> factors = GetFactors(number1);
                        number2 = factors[rnd.Next(factors.Count)]; 
                    } 
                    else number2 = rnd.Next(1, number1);
                }
                else
                    number2 = rnd.Next(1, 3);
                break;

            case 2:
                number1 = rnd.Next(3, 6);

                if (operation > 1)
                {
                    if (operationSet > 1)
                    {
                        List<int> factors = GetFactors(number1);
                        number2 = factors[rnd.Next(factors.Count)];
                    }
                    else number2 = rnd.Next(1, number1);
                }
                else
                    number2 = rnd.Next(3, 6);
                break;

            case 3:
                number1 = rnd.Next(6, 10);

                if (operation > 1)
                {
                    if (operationSet > 1)
                    {
                        List<int> factors = GetFactors(number1);
                        number2 = factors[rnd.Next(factors.Count)];
                    }
                    else number2 = rnd.Next(1, number1);
                }
                else
                    number2 = rnd.Next(6, 10);
                break;

            default:
                number1 = rnd.Next(1, 5);
                number2 = rnd.Next(1, 5);
                break;
        }

        //calculating the actual answer
        if (operationSet > 1) 
            answer = operation < 2 ? number1 * number2 : number1 / number2;      
        else
            answer = operation < 2 ? number1 + number2 : number1 - number2;
        //Debug.Log(number1+ operationString + number2 + " = " + answer);


        // fill the dummy and correct option for user
        GameObject[] userOptions = GameObject.FindGameObjectsWithTag("Options");
        int correctOption = rnd.Next(0, 4);
        int dummyNumber =0;
        List<int> dummyNumberList = new List<int>();
        for (int i=0; i < userOptions.Length; i++)
        {
            if (i != correctOption)
            {
                while(dummyNumberList.Count <= i)
                {
                    dummyNumber = rnd.Next(answer -5, answer + 5);
                    if (dummyNumber < 0) dummyNumber = -dummyNumber;
                    if (!dummyNumberList.Contains(dummyNumber) && dummyNumber!= answer)
                    {
                        dummyNumberList.Add(dummyNumber);
                    }
                }
                //switch (i)
                //{
                //    case 0:
                //        dummyNumber = answer + rnd.Next(1, number1 + 1);
                //        break;
                //    case 1:
                //        dummyNumber = answer - rnd.Next(2, number1 + 3);
                //        break;
                //    case 2:
                //        dummyNumber = answer + rnd.Next(3, number1 + 5);
                //        break;
                //    default:
                //        dummyNumber = answer - rnd.Next(4, number1 + 7);
                //        break;
                //}
                //dummyNumberList.Add(dummyNumber);
                userOptions[i].GetComponent<Text>().text = dummyNumber.ToString();
            }
            else userOptions[i].GetComponent<Text>().text = answer.ToString();
        }        



        //  fill the number of Edible objects on screen for user
        List<IEdible> leftObjects = new List<IEdible>(), rightObjects = new List<IEdible>();
        EdibleCreator edibleCreator = new EdibleCreator();
        IEdible RandomEdible = Edible>1 ? edibleCreator.GetRandomFruit(false) : edibleCreator.GetRandomVeggie(false);
        RandomEdible.EdObject.tag = "Edible";
        leftObjects.Add(RandomEdible);
        for (int i = 0; i < number1 - 1; i++)  //-1 to avoid extra due to adding RandomEdible to list
        {
            IEdible CloneEdible = new Vegetable
            {
                EdAudio = RandomEdible.EdAudio,
                EdSprite = RandomEdible.EdSprite,
                EdName = RandomEdible.EdName,
                EdSize = RandomEdible.EdSize,
                EdObject = Instantiate(RandomEdible.EdObject, GameObject.Find("Foodholder_L").transform)
            };
            leftObjects.Add(CloneEdible);
        }
        for (int i = 0; i < number2; i++)
        {
            IEdible CloneEdible = new Vegetable
            {
                EdAudio = RandomEdible.EdAudio,
                EdSprite = RandomEdible.EdSprite,
                EdName = RandomEdible.EdName,
                EdSize = RandomEdible.EdSize,
                EdObject = Instantiate(RandomEdible.EdObject, GameObject.Find("Foodholder_R").transform)
            };
            rightObjects.Add(CloneEdible);
        }
        Transform[] leftSpawns = GameObject.Find("Foodholder_L").transform.GetComponentsInChildren<Transform>();
        Transform[] rightSpawns = GameObject.Find("Foodholder_R").transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < leftObjects.Count; i++)
        {
            leftObjects[i].EdObject.GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 150f);
            leftObjects[i].EdObject.transform.SetParent(GameObject.Find("Foodholder_L").transform, false);
            leftObjects[i].EdObject.transform.position = new Vector3(leftSpawns[i + 1].position.x, leftSpawns[i + 1].position.y, 1);
        }
        for (int i = 0; i < rightObjects.Count; i++)
        {
            rightObjects[i].EdObject.GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 150f);
            rightObjects[i].EdObject.transform.SetParent(GameObject.Find("Foodholder_R").transform, false);
            rightObjects[i].EdObject.transform.position = new Vector3(rightSpawns[i + 1].position.x, rightSpawns[i + 1].position.y, 1);
        }
        answerClicked = false;
    }
    List<int> GetFactors(int x)
    {
        List<int> factors = new List<int>();
        for (int i = 1; i * i <= x; i++)
        {
            if (0 == (x % i))
            {
                factors.Add(i);
                if (i != (x / i))
                {
                    factors.Add(x / i);
                }
            }
        }
        return factors;
    }
}